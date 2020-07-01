using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WebApplication.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection("Server = localhost; Database=Majster; Trusted_Connection=True;");
        public int LoginCheck(Logins ad)
        {
            SqlCommand com = new SqlCommand("Sp_Users", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@User", ad.Login);
            com.Parameters.AddWithValue("@Pass", ad.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;

        }
    }
}
