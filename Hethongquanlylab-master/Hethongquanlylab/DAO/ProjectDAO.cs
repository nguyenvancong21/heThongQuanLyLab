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
    public class ProjectDAO
    {
        private static ProjectDAO instance;
        public static ProjectDAO Instance
        {
            get { if (instance == null) instance = new ProjectDAO(); return ProjectDAO.instance; }
            private set { ProjectDAO.instance = value; }
        }

        private ProjectDAO() { }

        public List<Project> GetProjectList()
        {
            List<Project> items = DataProvider<Project>.Instance.GetListItem();
            return items;
        }

        public List<Project> GetProjectList(string unit, string var = "")
        {
            List<Project> items = DataProvider<Project>.Instance.GetListItem("Unit", unit);
            return items;
        }

        public Project GetProjectModelbyId(string ID)
        {
            Project item = DataProvider<Project>.Instance.GetItem("ID", ID);
            return item;
        }

        public void EditProject(Project project)
        {
            DataTable data = DataProvider<Project>.Instance.LoadData();
            DataRow newProject = data.Select("ID=" + project.ID).FirstOrDefault();

            if (newProject != null)
            {
                var allAttr = typeof(Project).GetProperties(); // Lấy danh sách attributes của class Project
                foreach (var attr in allAttr)
                    newProject[attr.Name] = attr.GetValue(project);
            }

            DataProvider<Project>.Instance.UpdateData(data);

        }

        public void AddProject(Project project) // Thêm mới quy trình vào sheetName
        {
            DataTable data = DataProvider<Project>.Instance.LoadData();
            DataRow newProject = data.NewRow();

            var allAttr = typeof(Project).GetProperties(); // Lấy danh sách attributes của class Project

            foreach (var attr in allAttr)
                newProject[attr.Name] = attr.GetValue(project);


            data.Rows.Add(newProject);

            DataProvider<Project>.Instance.UpdateData(data);
        }

        public void DeleteProject(String ID)
        {
            DataProvider<Project>.Instance.DeleteItem("ID", ID);
        }
        
    }
}
