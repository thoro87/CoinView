using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView.Models {
    public static class UserFactory {

        public static User Janis;
        public static User JanisInvestment;
        public static User Lena;
        public static User LenaInvestment;

        static UserFactory() {
            Janis = new User() { ID = 1, Name = "Janis", AccountType = User.AccountTypes.Trading };
            JanisInvestment = new User() { ID = 2, Name = "Janis Investment", AccountType = User.AccountTypes.Investment };
            Lena = new User() { ID = 3, Name = "Lena", AccountType = User.AccountTypes.Trading };
            LenaInvestment = new User() { ID = 4, Name = "Lena Investment", AccountType = User.AccountTypes.Investment };
        }

        public static List<User> All() {
            return new List<User>() {
                Janis, Lena, JanisInvestment, LenaInvestment
            };
        }

    }
}
