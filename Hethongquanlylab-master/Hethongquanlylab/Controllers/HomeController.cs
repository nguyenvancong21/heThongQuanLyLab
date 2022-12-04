using Hethongquanlylab.Models;
using Hethongquanlylab.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hethongquanlylab.Common;

namespace Hethongquanlylab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<string> links = Function.Instance.getLinks();

        public IActionResult Index()
        {
            String page;
            var urlQuery = Request.HttpContext.Request.Query;
            page = urlQuery["page"]; // Lấy trang thông báo
            var notificationList = Function.Instance.getNotifications(page);
            notificationList.Items = notificationList.Items.Where(s => s.Inner == false).ToList();
            notificationList.Link = links;
            return View("~/Views/Home/Home.cshtml", notificationList);
        }

        [HttpPost]
        public IActionResult Index(int currentPage = 1)
        {
            return RedirectToAction("Index", new { page = currentPage });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotificationDetail()
        {
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            var currenId = Convert.ToInt32(CurrentID);

            var notification = NotificationDAO.Instance.GetNotificationModelbyId_Excel(currenId);
            var item = new ItemDetail<Notification>(notification, "Home");
            item.Link = links;
            return View("./Views/Home/NotificationDetail.cshtml", item);
        }
    }
}
