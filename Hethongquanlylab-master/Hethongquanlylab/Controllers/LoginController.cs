using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hethongquanlylab.DAO;
using Hethongquanlylab.Models;
using Hethongquanlylab.Models.Login;

namespace Hethongquanlylab.Controllers
{
    public class LoginController : Controller
    {

        // Index

        public IActionResult ChangeToLoginIndex() //Action đệm, tránh HttpPost
        {

            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult Index()
        {
            AccountLoginInput accountLoginInput = new AccountLoginInput();

            accountLoginInput.UserName = TempData["AccName"] == null ? null : TempData["AccName"].ToString(); // Lưu thông tin người dùng nhập
            TempData["AccName"] = null;
            accountLoginInput.Password = TempData["Password"] == null ? null : TempData["Password"].ToString();
            TempData["Password"] = null;
            String loginSubmit = TempData["LoginSubmit"] == null ? "0" : TempData["LoginSubmit"].ToString();


            if (loginSubmit == "1")
            {
                if (accountLoginInput.UserName == null) // Chưa nhập tên đăng nhập
                {
                    TempData["msg"] = "Vui lòng nhập tên đăng nhập!";
                    return View("./Views/Shared/Login/Login.cshtml", accountLoginInput);
                }

                if ((accountLoginInput.UserName == "Admin") && (accountLoginInput.Password == "labthaysinhadmin123"))
                {
                    return RedirectToAction("Index", "Admin");
                }

                Account user = AccountDAO.Instance.GetAccountbyUsername_Excel(accountLoginInput.UserName);

                if (user == null && loginSubmit == "1")  // Không tìm thấy tên đăng nhập trong database
                {
                    TempData["msg"] = "Tài khoản không tồn tại!";
                    return View("./Views/Shared/Login/Login.cshtml", accountLoginInput);
                }

                // Tìm thấy tên đăng nhập trong database
                if (accountLoginInput.Password == null) // Chưa nhập mật khẩu
                {
                    TempData["msg"] = "Vui lòng nhập mật khẩu";
                    return View("./Views/Shared/Login/Login.cshtml", accountLoginInput);
                }

                else if (accountLoginInput.Password != user.Password) // Nhập sai mật khẩu
                {
                    TempData["msg"] = "Bạn nhập sai mật khẩu!";
                    return View("./Views/Shared/Login/Login.cshtml", accountLoginInput);
                }
                else // Đúng mật khẩu và chuyển hướng loại tài khoản
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.AccountType = user.AccountType;
                    HttpContext.Session.SetString("LoginSession", JsonConvert.SerializeObject(userSession));// set Student Session thành 1 JsonConvert 

                    TempData["LoginSubmit"] = "0"; // reset submit var

                    if (user.AccountType == "User")
                        return RedirectToAction("Index", "User");
                    else if (user.AccountType == "Super")
                    {
                        TempData["LoginSubmit"] = "0";

                        if (user.Username == "BanNhanSu") return RedirectToAction("Index", "BNS");
                        else if (user.Username == "BanDaoTao") return RedirectToAction("Index", "BDT");
                        else if (user.Username == "BanTruyenThong") return RedirectToAction("Index", "BTT");
                        else if (user.Username == "BanGiamSat") return RedirectToAction("Index", "BGS");
                        else if (user.Username == "BanDieuHanh") return RedirectToAction("Index", "BDH");
                        else if (user.Username == "BanDoiSong") return RedirectToAction("Index", "BDS");
                        else if (user.Username == "BanCoVan") return RedirectToAction("Index", "BCV");
                        else if (user.Username == "BanSuKien") return RedirectToAction("Index", "BSK");
                        else if (user.Username == "PTLT") return RedirectToAction("Index", "PTLT");
                        else if (user.Username == "PTCK") return RedirectToAction("Index", "PTCK");
                        else if (user.Username == "PTTDH") return RedirectToAction("Index", "PTTDH");
                        else if (user.Username == "PTNN") return RedirectToAction("Index", "PTNN");
                        else if (user.Username == "PTPTBT") return RedirectToAction("Index", "PTPTBT");
                        else if (user.Username == "NhaSangLap") return RedirectToAction("Index", "NSL");
                        else if (user.Username == "NhaDongSangLap") return RedirectToAction("Index", "NDSL");
                        else return RedirectToAction("Index", "BNS");
                    }

                    else if (user.AccountType == "Admin")
                        return RedirectToAction("Index", "Admin");

                }
            }
            return View("./Views/Shared/Login/Login.cshtml", accountLoginInput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AccountLoginInput input)
        {
            TempData["AccName"] = input.UserName;
            TempData["Password"] = input.Password;
            TempData["LoginSubmit"] = "1";
            return RedirectToAction("Index", "Login");
        }
        // End Index

        // Đổi mật khẩu

        public IActionResult ChangeToChangePassword() //Action đệm, tránh HttpPost
        {
            return RedirectToAction("ChangePassword", "Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userSession = JsonConvert.DeserializeObject<UserLogin>(HttpContext.Session.GetString("LoginSession"));
            Account user = AccountDAO.Instance.GetAccountbyUsername_Excel(userSession.UserName);
            ChangePasswordInput changePasswordInput = new ChangePasswordInput();

            changePasswordInput.OldPassword = TempData["OldPass"] == null ? null : TempData["OldPass"].ToString(); // Lưu thông tin người dùng nhập
            TempData["OldPass"] = null;
            changePasswordInput.NewPassword = TempData["NewPass"] == null ? null : TempData["NewPass"].ToString();
            TempData["NewPass"] = null;
            changePasswordInput.ReNewPassword = TempData["ReNewPass"] == null ? null : TempData["ReNewPass"].ToString();
            TempData["ReNewPass"] = null;
            String changePassSubmit = TempData["ChangePassSubmit"] == null ? "0" : TempData["ChangePassSubmit"].ToString();


            if (changePassSubmit == "1")
            {
                if (changePasswordInput.OldPassword == null)
                {
                    TempData["msg"] = "Vui lòng nhập mật khẩu cũ";
                }
                else if (changePasswordInput.OldPassword != user.Password)
                {
                    TempData["msg"] = "Bạn nhập sai mật khẩu hiện tại!";
                }
                else if (changePasswordInput.NewPassword == null)
                {
                    TempData["msg"] = "Vui lòng nhập mật khẩu mới";
                }
                else if (changePasswordInput.ReNewPassword == null)
                {
                    TempData["msg"] = "Vui lòng xác nhận lại mật khẩu mới";
                }

                else if (changePasswordInput.OldPassword == user.Password)
                {
                    if (changePasswordInput.NewPassword != changePasswordInput.ReNewPassword) TempData["msg"] = "Mật khẩu xác nhận không trùng khớp!";
                    else
                    {
                        TempData["msg"] = "Đổi mật khẩu thành công!";
                        AccountDAO.Instance.ChangePassword(user.Username, changePasswordInput.NewPassword);
                        return RedirectToAction("ChangeToLoginIndex", "Login");
                    }
                }
            }

            return View("./Views/Shared/Login/ChangePassword.cshtml", changePasswordInput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordInput input)
        {
            TempData["OldPass"] = input.OldPassword;
            TempData["NewPass"] = input.NewPassword;
            TempData["ReNewPass"] = input.ReNewPassword;
            TempData["ChangePassSubmit"] = "1";
            return RedirectToAction("ChangePassword", "Login");

        }

        //
        public IActionResult ChangeToForgotPassword() //Action đệm, tránh HttpPost
        {
            return RedirectToAction("ForgotPassword", "Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("./Views/Shared/Login/ForgotPassword.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(String accName, String pass)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
