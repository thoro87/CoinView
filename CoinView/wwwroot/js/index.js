$(document).ready(init);

var mainModel;

function init() {
    mainModel = {};
    mainModel.isInitialized = ko.observable(false);
    mainModel.users = ko.observable([]);
    mainModel.userID = ko.observable(null);
    mainModel.user = ko.pureComputed(function () {
        return Enumerable.From(mainModel.users()).Where(function (u) { return u.UserId === mainModel.userID(); }).SingleOrDefault();
    });

    mainModel.wallets = ko.observable({});
    mainModel.coins = ko.observable({});

    mainModel.buysForTrades = ko.observableArray([]);
    mainModel.buysForInvests = ko.observableArray([]);
    mainModel.trades = ko.observable([]);
    mainModel.openTrades = ko.pureComputed(function () {
        return Enumerable.From(mainModel.trades()).Where(function (t) { return !t.isSold(); }).OrderByDescending(function (t) { return t.changeEUR(); }).ToArray();
    });
    mainModel.closedTrades = ko.pureComputed(function () {
        return Enumerable.From(mainModel.trades()).Where(function (t) { return t.isSold(); }).OrderByDescending(function (t) { return t.sellDate(); }).ToArray();
    });
    mainModel.creations = ko.observable([]);
    mainModel.openCreations = ko.pureComputed(function () {
        return Enumerable.From(mainModel.creations()).Where(function (c) { return !c.isSold() }).OrderByDescending(function (c) { return c.valueEUR(); }).ToArray();
    });
    mainModel.closedCreations = ko.pureComputed(function () {
        return Enumerable.From(mainModel.creations()).Where(function (c) { return c.isSold() }).OrderByDescending(function (c) { return c.sellDate(); }).ToArray();
    });
    mainModel.hasValues = ko.observable(false);
    mainModel.coinValues = ko.observable([]);

    // summary trading
    mainModel.tradingBuyBTCSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForTrades()).Select(function (b) { return b.AmountInWallet; }).Sum();
    });
    mainModel.tradingBuyEURSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForTrades()).Select(function (b) { return b.AmountBought * b.PriceEur; }).Sum();
    });
    mainModel.tradingOpenTradeCompareValueBTCSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.openTrades()).Select(function (t) { return t.compareValueBTC(); }).Sum();
    });
    mainModel.tradingOpenTradeCompareValueEURSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.openTrades()).Select(function (t) { return t.compareValueEUR(); }).Sum();
    });
    mainModel.tradingOpenCreationsBTCSum = ko.pureComputed(function () {
        return mainModel.hasValues() ? Enumerable.From(mainModel.openCreations()).Select(function (c) { return c.amount() * mainModel.coinValues()[c.coin().CoinId].PriceBtc; }).Sum() : 0;
    });
    mainModel.tradingOpenCreationsEURSum = ko.pureComputed(function () {
        return mainModel.hasValues() ? Enumerable.From(mainModel.openCreations()).Select(function (c) { return c.amount() * mainModel.coinValues()[c.coin().CoinId].PriceEur; }).Sum() : 0;
    });
    mainModel.holdingsBTCSum = ko.pureComputed(function () {
        return mainModel.tradingOpenTradeCompareValueBTCSum() + mainModel.tradingOpenCreationsBTCSum();
    });
    mainModel.holdingsEURSum = ko.pureComputed(function () {
        return mainModel.tradingOpenTradeCompareValueEURSum() + mainModel.tradingOpenCreationsEURSum();
    });
    mainModel.tradingTotalSummaryBTC = ko.pureComputed(function () {
        return mainModel.holdingsBTCSum() - mainModel.tradingBuyBTCSum();
    });
    mainModel.tradingTotalSummaryEUR = ko.pureComputed(function () {
        return mainModel.holdingsEURSum() - mainModel.tradingBuyEURSum();
    });
    mainModel.tradingTotalSummaryBTCPercent = ko.pureComputed(function () {
        return mainModel.tradingTotalSummaryBTC() / mainModel.tradingBuyBTCSum() * 100;
    });
    mainModel.tradingTotalSummaryEURPercent = ko.pureComputed(function () {
        return mainModel.tradingTotalSummaryEUR() / mainModel.tradingBuyEURSum() * 100;
    });


    // summary investment
    mainModel.investmentAmountInWalletSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForInvests()).Select(function (i) { return i.amountInWallet(); }).Sum();
    });
    mainModel.investmentBuyValueEURSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForInvests()).Select(function (i) { return i.buyValueEUR() }).Sum();
    });
    mainModel.investmentSellValueEURSum = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForInvests()).Select(function (i) { return i.sellValueEUR() }).Sum();
    });
    mainModel.investmentChangeEURSum = ko.pureComputed(function () {
        return mainModel.investmentSellValueEURSum() - mainModel.investmentBuyValueEURSum();
    });
    mainModel.investmentChangeEURPercentSum = ko.pureComputed(function () {
        return mainModel.investmentChangeEURSum() / mainModel.investmentBuyValueEURSum() * 100;
    });

    ko.applyBindings(mainModel);

    mainModel.userID.subscribe(function (newValue) {
        if (mainModel.isInitialized()) {
            beginGetData();
        }
    });

    beginGetData();
    //beginGetCoinValues();
}

function beginGetData() {
   xhr.callService(endGetData, "Home", "GetData", { userID: mainModel.userID() != null ? mainModel.userID() : 1 });
}

function endGetData(result) {
    mainModel.users(result.Users);
    mainModel.coins(result.Coins);
    mainModel.wallets(result.Wallets);
    mainModel.buysForTrades(Enumerable.From(result.Buys).Where(function (b) { return b.Purpose === 'Trade'; }).ToArray());
    mainModel.buysForInvests(Enumerable.From(result.Buys).Where(function (b) { return b.Purpose === 'Invest'; }).Select(function (b) { return createInvestment(b); }).ToArray());
    mainModel.creations(Enumerable.From(result.Creations).Select(function (c) { return createCreation(c); }).ToArray());
    mainModel.trades(Enumerable.From(result.Trades).Select(function (t) { return createTrade(t); }).ToArray());
    mainModel.coinValues(result.CoinValues);
    mainModel.isInitialized(true);
    mainModel.hasValues(true);
}

function createInvestment(investment) {
    var that = {};

    that.date = ko.observable(investment.Date);
    that.exchange = ko.observable(mainModel.wallets()[investment.ExchangeWalletId]);
    that.wallet = ko.observable(mainModel.wallets()[investment.WalletId]);
    that.coin = ko.observable(mainModel.coins()[investment.CoinId]);
    that.priceEUR = ko.observable(investment.PriceEur);
    that.amountBought = ko.observable(investment.AmountBought);
    that.amountInWallet = ko.observable(investment.AmountInWallet);
    that.buyValueEUR = ko.pureComputed(function () {
        return that.amountBought() * that.priceEUR();
    });
    that.sellValueEUR = ko.pureComputed(function () {
        if (mainModel.hasValues()) {
            return that.amountInWallet() * mainModel.coinValues()[1].PriceEur;
        } else {
            return 0;
        }
    });
    that.changeEUR = ko.pureComputed(function () {
        if (that.sellValueEUR() != null) {
            return that.sellValueEUR() - that.buyValueEUR();
        } else {
            return 0;
        }
    });
    that.changeEURPercent = ko.pureComputed(function () {
        if (that.sellValueEUR() != null) {
            return that.changeEUR() / that.buyValueEUR() * 100;
        } else {
            return 0;
        }
    });

    return that;
}

function createCreation(creation) {
    var that = {};

    that.wallet = ko.observable(mainModel.wallets()[creation.WalletId]);
    that.coin = ko.observable(mainModel.coins()[creation.CoinId]);
    that.amount = ko.observable(creation.Amount);

    that.isSold = ko.observable(creation.SellWalletId != null);
    that.sellWallet = ko.observable(mainModel.wallets()[creation.SellWalletId]);
    that.sellDate = ko.observable(creation.SellDate);
    that.sellPricePerShare = ko.observable(creation.SellPricePerShare);
    that.sellPriceBTC = ko.observable(creation.SellPriceBTC);

    that.valueBTC = ko.pureComputed(function () {
        if (mainModel.hasValues()) {
            if (that.isSold()) {
                return that.amount() * that.sellPricePerShare();
            } else {
                return that.amount() * mainModel.coinValues()[that.coin().CoinId].PriceBtc;
            }
        } else {
            return 0;
        }
    });
    that.valueEUR = ko.pureComputed(function () {
        if (mainModel.hasValues()) {
            if (that.isSold()) {
                return that.amount() * that.sellPricePerShare() * that.sellPriceBTC();
            } else {
                return that.amount() * mainModel.coinValues()[that.coin().CoinId].PriceEur;
            }
        } else {
            return 0;
        }
    });

    return that;
}

function createTrade(trade) {
    var that = {};

    that.storeWallet = ko.observable(mainModel.wallets()[trade.StoreWalletId]);
    that.coin = ko.observable(mainModel.coins()[trade.CoinId]);
    that.amount = ko.observable(trade.Amount);

    that.buyDate = ko.observable(trade.BuyDate);
    that.buyWallet = ko.observable(mainModel.wallets()[trade.BuyWalletId]);
    that.buyPricePerShare = ko.observable(trade.BuyPricePerShare);
    that.buyPriceBTC = ko.observable(trade.BuyPriceBtc);
    that.buyValueBTC = ko.observable(that.amount() * that.buyPricePerShare());
    that.buyValueEUR = ko.observable(that.buyValueBTC() * that.buyPriceBTC());

    that.sellDate = ko.observable(trade.SellDate);
    that.isSold = ko.observable(that.sellDate() != null);
    that.sellWallet = ko.observable(mainModel.wallets()[trade.SellWalletId]);
    that.sellPricePerShare = ko.observable(trade.SellPricePerShare);
    that.sellPriceBTC = ko.observable(trade.SellPriceBtc);
    that.sellValueBTC = ko.observable(that.amount() * that.sellPricePerShare());
    that.sellValueEUR = ko.observable(that.sellValueBTC() * that.sellPriceBTC());

    that.comparePricePerShare = ko.pureComputed(function () {
        if (that.isSold()) {
            return that.sellPricePerShare();
        } else if (mainModel.hasValues()) {
            return mainModel.coinValues()[that.coin().CoinId].PriceBtc;
        } else {
            return 0;
        }
    });
    that.compareValueBTC = ko.pureComputed(function () {
        if (that.isSold()) {
            return that.sellValueBTC();
        } else if (mainModel.hasValues()) {
            return that.amount() * mainModel.coinValues()[that.coin().CoinId].PriceBtc;
        } else {
            return 0;
        }
    });
    that.compareValueEUR = ko.pureComputed(function() {
        if (that.isSold()) {
            return that.sellValueEUR();
        } else if (mainModel.hasValues()) {
            return that.compareValueBTC() * mainModel.coinValues()[1].PriceEur;
        } else {
            return 0;
        }
    });
    
    that.changeBTC = ko.pureComputed(function () {
        return that.compareValueBTC() - that.buyValueBTC();
    });
    that.changeBTCPercent = ko.pureComputed(function () {
        return that.changeBTC() / that.buyValueBTC() * 100;
    });
    that.changeEUR = ko.pureComputed(function () {
        return that.compareValueEUR() - that.buyValueEUR();
    });
    that.changeEURPercent = ko.pureComputed(function () {
        return that.changeEUR() / that.buyValueEUR() * 100;
    });

    that.showDetails = ko.observable(false);
    that.toggleDetails = function () {
        that.showDetails(!that.showDetails());
    }

    return that;
}

//function beginGetCoinValues() {
//    xhr.callService(endGetCoinValues, "Home", "GetCoinValues", {});
//}

//function endGetCoinValues(coinValues) {
//    mainModel.coinValues(coinValues);
//    mainModel.hasValues(true);
//}