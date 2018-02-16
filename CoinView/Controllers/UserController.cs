using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinView.Models.Database;
using CoinView.Models;

namespace CoinView.Controllers {
    public class UserController : Controller {

        private readonly CoinViewContext db;

        public UserController(CoinViewContext _context) {
            db = _context;
        }

        public IActionResult GetData(string id) {
            int userID = Int32.Parse(id);

            UserViewModel model = new UserViewModel() {
                User = db.Users.Single(u => u.UserId == userID)
            };

            return View("User", model);
        }


    }
}