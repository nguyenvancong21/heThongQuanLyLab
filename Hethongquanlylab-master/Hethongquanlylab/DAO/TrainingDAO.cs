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
    public class TrainingDAO
    {
        private static TrainingDAO instance;
        public static TrainingDAO Instance
        {
            get { if (instance == null) instance = new TrainingDAO(); return TrainingDAO.instance; }
            private set { TrainingDAO.instance = value; }
        }

        private TrainingDAO() { }

        public List<Training> GetTrainingList()
        {
            List<Training> items = DataProvider<Training>.Instance.GetListItem();
            return items;
        }

        public List<Training> GetTrainingList(string unit, string var ="")
        {
            List<Training> items = DataProvider<Training>.Instance.GetListItem("Unit", unit);
            return items;
        }

        public Training GetTrainingModelbyId(string ID)
        {
            Training item = DataProvider<Training>.Instance.GetItem("ID", ID);
            return item;
        }

        public void EditTraing(Training training, string var = "")
        {
            DataTable data = DataProvider<Training>.Instance.LoadData();
            DataRow newTraining = data.Select("ID=" + training.ID).FirstOrDefault();

            if (newTraining != null)
            {
                var allAttr = typeof(Training).GetProperties(); // Lấy danh sách attributes của class Training
                foreach (var attr in allAttr)
                    newTraining[attr.Name] = attr.GetValue(training);
            }

            DataProvider<Training>.Instance.UpdateData(data);
        }

        public void AddTraining(Training training, string var = "") // Thêm mới quy trình vào sheetName
        {
            DataTable data = DataProvider<Training>.Instance.LoadData();
            DataRow newTraining = data.NewRow();

            var allAttr = typeof(Training).GetProperties(); // Lấy danh sách attributes của class Training

            foreach (var attr in allAttr)
                newTraining[attr.Name] = attr.GetValue(training);


            data.Rows.Add(newTraining);

            DataProvider<Training>.Instance.UpdateData(data);
        }

        public void DeleteTraining(String ID, string var ="")
        {
            DataProvider<Training>.Instance.DeleteItem("ID", ID);
        }
    }
}
