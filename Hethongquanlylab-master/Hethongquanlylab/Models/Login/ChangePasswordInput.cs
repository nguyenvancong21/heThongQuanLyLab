using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hethongquanlylab.Models.Login
{
    // Lưu thông tin người dùng nhập vào khi đổi mật khẩu
    public class ChangePasswordInput
    {
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }
        public string ReNewPassword { set; get; }
    }
}
