using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hethongquanlylab.Models;
using System.Data;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.IO;
using System.Globalization;

namespace Hethongquanlylab.DAO
{
    public class NotificationDAO
    {
        private static NotificationDAO instance;
        public static NotificationDAO Instance
        {
            get { if (instance == null) instance = new NotificationDAO(); return NotificationDAO.instance; }
            private set { NotificationDAO.instance = value; }
        }

        private NotificationDAO() { }

        public List<Notification> GetNotificationList_Excel()
        {
            List<Notification> items = DataProvider<Notification>.Instance.GetListItem();
            return items;
        }

        public Notification GetNotificationModelbyId_Excel(int ID)
        {
            Notification item = DataProvider<Notification>.Instance.GetItem("ID", ID.ToString());
            return item;
        }

        public List<Notification> FindNotificationbyTitle(string notificationTitle)
        {
            List<Notification> item = DataProvider<Notification>.Instance.GetListItem("Title", notificationTitle);
            return item;
        }
        public List<Notification> GetNotificationListbyUnit(string Unit)
        {
            List<Notification> items = DataProvider<Notification>.Instance.GetListItem("Unit", Unit);
            return items;
        }
        public void DeleteNotification(String ID)
        {
            DataProvider<Notification>.Instance.DeleteItem("ID", ID);
        }
        public void AddNotification(Notification notification)
        {
            notification.Date  = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            DataTable data = DataProvider<Notification>.Instance.LoadData();
            DataRow newNotification = data.NewRow();

            var allAttr = typeof(Notification).GetProperties(); // Lấy danh sách attributes của class Notification

            foreach (var attr in allAttr)
                newNotification[attr.Name] = attr.GetValue(notification);


            data.Rows.Add(newNotification);

            DataProvider<Notification>.Instance.UpdateData(data);
        }

        public void EditNotification(Notification notification)
        {
            notification.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            DataTable data = DataProvider<Notification>.Instance.LoadData();
            DataRow newNotification = data.Select("ID=" + notification.ID).FirstOrDefault();

            if (newNotification != null)
            {
                var allAttr = typeof(Notification).GetProperties(); // Lấy danh sách attributes của class Notification
                foreach (var attr in allAttr)
                    newNotification[attr.Name] = attr.GetValue(notification);
            }

            DataProvider<Notification>.Instance.UpdateData(data);
        }
    }
}
