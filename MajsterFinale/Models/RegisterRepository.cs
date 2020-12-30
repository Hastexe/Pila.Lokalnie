using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace MajsterFinale.Models
{
    public class RegisterRepository
    {
        public string Encryption(String PASSWORD)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //szyfrowanie hasła  
            encrypt = md5.ComputeHash(encode.GetBytes(PASSWORD));
            StringBuilder encryptdata = new StringBuilder();
            //tworzenie nowego ciągu z zaszyfrowanego hasła 
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        [NonAction]
        public bool IsEmailExist(string MAIL)
        {
            using (BazaLocal db = new BazaLocal())
            {
                var v = db.USERS.Where(a => a.MAIL == MAIL).FirstOrDefault();
                return v != null;
            }
        }
        /*
        [NonAction]
        public bool IsLoginExist(string LOGIN)
        {
            using (BazaLocal db = new BazaLocal())
            {
                var v = db.USERS.Where(a => a.LOGIN == LOGIN).FirstOrDefault();
                return v != null;
            }
        }
        */

        [NonAction]
        public bool ArePasswordsSame(USERS USERS)
        {
            var password = USERS.PASSWORD;
            var repassword = USERS.REPASSWORD;

            if (password != repassword)
            {
            return true;
            }
            else return false;   
        }

        [NonAction]
        public bool AreMailsSame(USERS USERS)
        {
            var mail = USERS.MAIL;
            var remail = USERS.REMAIL;

            if (mail != remail)
            {
                return true;
            }
            else return false;
        }

        [NonAction]
        public bool IsPasswordNotNull(USERS USERS)
        {
            var password = USERS.PASSWORD;
            var repassword = USERS.REPASSWORD;

            if (password == null || repassword == null)
            {
                return true;
            }
            return false;
        }

        [NonAction]
        public bool IsMailNotNull(USERS USERS)
        {
            var mail = USERS.MAIL;
            //var remail = USERS.REMAIL;

            if (mail == null)
            {
                return true;
            }
            return false;
        }
        [NonAction]
        public bool AreTermsAccepted(USERS USERS)
        {
            var terms = USERS.TERMS;

            if (terms != true)
            {
                return true;
            }
            return false;
        }
    }
}