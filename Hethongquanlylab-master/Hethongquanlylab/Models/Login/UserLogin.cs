using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hethongquanlylab.Models.Login
{
    // Lưu thông tin đăng nhập vào Session
    [Serializable]
    public class UserLogin
    {
        public string UserName { set; get; }
        public string AccountType { set; get; }
    }
}
