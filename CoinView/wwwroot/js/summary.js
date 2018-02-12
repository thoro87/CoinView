var mainModel = {};

function init() {

    beginGetData();
}

function beginGetData() {
   xhr.callService(endGetData, "Summary", "GetData", { });
}

function endGetData(result) {
    mainModel.isInitialized = ko.observable(false);
    mainModel.coinValues = ko.observable(result.CoinValues);

    // invests
    mainModel.buysForInvests = ko.observable(Enumerable.From(result.Buys).Where(function (b) { return b.Purpose === "Invest"; }).ToArray());
    mainModel.investsSellValueEur = ko.pureComputed(function () {
        return Enumerable.From(mainModel.buysForInvests()).Select(function (b) { return b.AmountInWallet * mainModel.coinValues()[1].PriceEur; }).Sum();
    });
    mainModel.investsBuyValueEur = ko.pureComputed(function () {
        return -Enumerable.From(mainModel.buysForInvests()).Select(function (b) { return b.AmountBought * b.PriceEur; }).Sum();
    });
    mainModel.investsResultValueEur = ko.pureComputed(function () {
        return mainModel.investsSellValueEur() + mainModel.investsBuyValueEur();
    });

    // trades
    mainModel.buysForTrades = ko.observable(Enumerable.From(result.Buys).Where(function (b) { return b.Purpose === "Trade"; }).ToArray());
    mainModel.trades = ko.observable(result.OpenTrades);
    mainModel.tradesSellValueEur = ko.pureComputed(function () {
        return Enumerable.From(mainModel.trades()).Select(function (t) { return t.Amount * mainModel.coinValues()[t.CoinId].PriceEur; }).Sum();
    });
    mainModel.tradesBuyValueEur = ko.pureComputed(function () {
        return -Enumerable.From(mainModel.buysForTrades()).Select(function (b) { return b.AmountBought * b.PriceEur; }).Sum();
    });
    mainModel.tradesResultValueEur = ko.pureComputed(function () {
        return mainModel.tradesSellValueEur() + mainModel.tradesBuyValueEur();
    });

    // creations
    mainModel.creations = ko.observable(result.OpenCreations);
    mainModel.creationsSellValueEur = ko.pureComputed(function () {
        return Enumerable.From(mainModel.creations()).Select(function (c) { return c.Amount * mainModel.coinValues()[c.CoinId].PriceEur; }).Sum();
    });
    mainModel.creationsBuyValueEur = ko.observable(0);
    mainModel.creationsResultValueEur = ko.pureComputed(function () {
        return mainModel.creationsSellValueEur() + mainModel.creationsBuyValueEur();
    });

    ko.applyBindings(mainModel);

    $("#Throbber").css({ 'visibility': 'hidden' });
    $("#Content").css({ 'visibility': 'visible' });
}
