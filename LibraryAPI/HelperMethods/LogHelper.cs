using LibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAPI.HelperMethods
{
    public class LogHelper
    {
        private libraryManagementEntities db = new libraryManagementEntities();
        public void InsertLog(string user_email, string message)
        {
            log log = new log
            {
                time = DateTime.Now,
                message = message,
                user_email = user_email
            };

            db.logs.Add(log);
            db.SaveChanges();
        }
    }
}