using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Hethongquanlylab.Models;
using Hethongquanlylab.DAO;
using Hethongquanlylab.Common;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Table;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Hethongquanlylab.Models.Login;

namespace Hethongquanlylab.Controllers
{
    public class AdminController : Controller
    {
        private string unit = "Admin";
        private string unitVar = "Admin";

        public List<string> links = Function.Instance.getLinks();

        public IActionResult Index()
        {
            return RedirectToAction("Account");
        }

        public IActionResult EditLinks()
        {
            var linkss = LinkDAO.Instance.GetLinkList();
            var linkList = new ItemDisplay<Link>();
            linkList.Items = linkss;
            linkList.SessionVar = unit;
            linkList.Link = links;
            return View(String.Format("./Views/{0}/Links/EditLinks.cshtml", unitVar), linkList);
        }

        [HttpPost]
        public IActionResult EditLinks(string Link_bieumau, string Link_lich)
        {
            LinkDAO.Instance.EditLink("Biểu mẫu", Link_bieumau);
            LinkDAO.Instance.EditLink("Lịch làm việc", Link_lich);

            return RedirectToAction("EditLinks");
        }

        public IActionResult Account()
        {
            // Khởi tạo
            String field;
            String sortOrder;
            String searchField;
            String searchString;
            String page;
            String varr;
            String mess;

            /// Lấy query, không có => đặt mặc định
            var urlQuery = Request.HttpContext.Request.Query; // Url: .../Member?Sort={sortOrder}&searchField={searchField}...
            field = urlQuery["field"];
            sortOrder = urlQuery["sort"];
            searchField = urlQuery["searchField"];
            searchString = urlQuery["SearchString"];
            page = urlQuery["page"];
            varr = urlQuery["var"];
            mess = urlQuery["mess"];

            field = field == null ? "All" : field;

            sortOrder = sortOrder == null ? "Name" : sortOrder; ;
            searchField = searchField == null ? "Name" : searchField;
            searchString = searchString == null ? "" : searchString;
            page = page == null ? "1" : page;
            varr = varr == null ? "0" : varr;
            mess = mess == null ? "0" : mess;
            int currentPage = Convert.ToInt32(page);

            /// Khởi tạo ItemDisplay<>
            var accountList = new ItemDisplay<Account>();
            accountList.Field = field;
            accountList.SortOrder = sortOrder;
            accountList.CurrentSearchField = searchField;
            accountList.CurrentSearchString = searchString;
            accountList.CurrentPage = currentPage;

            var accounts = AccountDAO.Instance.GetAccountList();

            accounts = Function.Instance.searchItems(accounts, accountList); // Tìm kiếm
            accounts = Function.Instance.sortItems(accounts, accountList.SortOrder); // Sắp xếp

            // Lấy danh sách items trong trang hiện tại
            accountList.Paging(accounts, 10);
            accountList.Paging(accounts, 10);
            if (accountList.PageCount > 0)
            {
                if (accountList.CurrentPage > accountList.PageCount) accountList.CurrentPage = accountList.PageCount;
                if (accountList.CurrentPage < 1) accountList.CurrentPage = 1;
                if (accountList.CurrentPage != accountList.PageCount)
                    try
                    {
                        accountList.Items = accountList.Items.GetRange((accountList.CurrentPage - 1) * accountList.PageSize, accountList.PageSize);
                    }
                    catch { }

                else
                    accountList.Items = accountList.Items.GetRange((accountList.CurrentPage - 1) * accountList.PageSize, accountList.Items.Count % accountList.PageSize == 0 ? accountList.PageSize : accountList.Items.Count % accountList.PageSize);
            }
            //

            accountList.SessionVar = unit;
            accountList.IsMessage = Convert.ToBoolean(Convert.ToInt32(varr));
            accountList.Link = links;
            if (mess == "1") accountList.Message = "Vui lòng nhập tên tài khoản!";
            else if (mess == "2") accountList.Message = "Vui lòng nhập mật khẩu";
            if (mess == "3") accountList.Message = "Tên tài khoản đã tồn tại!";
            return View(String.Format("./Views/{0}/Accounts/Account.cshtml", unitVar), accountList);
        }


        public IActionResult ChangeToAddAccount()
        {
            return RedirectToAction("Account", new { var = "1" });
        }

        public IActionResult ChangeToNotAddAccount()
        {
            return RedirectToAction("Account");
        }

        [HttpPost]
        public IActionResult Account(String Field, String sortOrder, String searchString, String searchField, int currentPage = 1)
        {
            return RedirectToAction("Account", new { field = Field, sort = sortOrder, searchField = searchField, searchString = searchString, page = currentPage });
        }

        [HttpPost]
        public IActionResult AddAccount(String UserName, String Password, String AccountType)
        {
            if (UserName == null) return RedirectToAction("Account", new { var = "1", mess = "1" });
            if (Password == null) return RedirectToAction("Account", new { var = "1", mess = "2" });
            var accounts = AccountDAO.Instance.GetAccountList();
            foreach (var acc in accounts)
            {
                if (UserName == acc.Username)
                {
                    return RedirectToAction("Account", new { var = "1", mess = "3" });
                }
            }

            var newAccount = new Account("0", UserName, Password, AccountType);
            AccountDAO.Instance.AddAccount(newAccount);
            return RedirectToAction("Account");
        }

        public IActionResult GenerateAccounts()
        {
            var Users = UserDAO.Instance.GetListUser();
            var Accounts = AccountDAO.Instance.GetAccountList();
            var UserNameList = new List<String>();
            foreach (var item in Accounts)
            {
                UserNameList.Add(item.Username);
            }

            foreach (var item in Users)
            {
                if (!UserNameList.Contains(item.LabID))
                {
                    var newAccount = new Account("0", item.LabID, item.LabID, "User");
                    AccountDAO.Instance.AddAccount(newAccount);
                }
            }

            List<String> superAccountList = new List<String>() {"NhaSangLap", "NhaDongSangLap", "BanNhanSu", "BanDieuHanh", "BanDoiSong", "BanSuKien", "BanDaoTao", "BanGiamSat", "BanTruyenThong", "BanCoVan", "PTLT", "PTCK", "PTTDH", "PTPTBT", "PTNN", "PTMKT" };

            foreach (string item in superAccountList)
            {
                if (!UserNameList.Contains(item))
                {
                    var newAccount = new Account("0", item, "12345", "Super", item);
                    AccountDAO.Instance.AddAccount(newAccount);
                }
            }
            
            return RedirectToAction("Account");
        }

        public IActionResult DeleteAccount()
        {
            var urlQuery = Request.HttpContext.Request.Query;
            String Key_delete = urlQuery["Key"];
            AccountDAO.Instance.DeleteAccount(Key_delete);
            return RedirectToAction("Account");
        }

    }
}
