using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.IO;
using System.Dynamic;

namespace MajsterFinale.Controllers
{
    public class UserController : Controller
    {
        UserRepository userRepository = new UserRepository();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Panel()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
            }
            return View();
        }
       
        public ActionResult Messages(int id)
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                MESSAGE UsersMessage = new UserRepository().GetUserMessages(uID);
            }
            return View();
        }
    }
}