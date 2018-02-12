using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinView {
    public class MyDesignTimeServices : IDesignTimeServices {
        public void ConfigureDesignTimeServices(IServiceCollection services) {
            services.AddSingleton<IPluralizer, MyPluralizer>();
        }
    }

    public class MyPluralizer : IPluralizer {
        public string Pluralize(string name) {
            return Inflector.Pluralize(name) ?? name;
        }

        public string Singularize(string name) {
            return Inflector.Singularize(name) ?? name;
        }
    }
}
