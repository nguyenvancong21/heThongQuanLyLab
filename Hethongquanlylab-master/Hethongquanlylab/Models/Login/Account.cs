using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Hethongquanlylab.Models
{
    // Model chung cho Account
    public class Account
    {
        private string username;
        private string password;
        private string accountType;

        public string ID { get; set; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string AccountType { get => accountType; set => accountType = value; }
        public string AccountVar { get; set; }

        public Account (string ID, string username, string password, string accounttype, string accountVar = "")
        {
            this.ID = ID;
            this.Username = username;
            this.Password = password;
            this.AccountType = accounttype;
            this.AccountVar = accountVar;
        }
        public Account(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.Username = (string)row["UserName"];
            this.Password = (string)row["PassWord"];
            this.AccountType = (string)row["AccountType"];
            this.AccountVar = row["AccountVar"] == null? "N/A": row["AccountVar"].ToString();
        }
    }
}
