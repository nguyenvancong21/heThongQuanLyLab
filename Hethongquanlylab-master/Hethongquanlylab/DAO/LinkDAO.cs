using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hethongquanlylab.Models;
using OfficeOpenXml;


namespace Hethongquanlylab.DAO
{
    public class LinkDAO
    {
        private static LinkDAO instance;
        public static LinkDAO Instance
        {
            get { if (instance == null) instance = new LinkDAO(); return LinkDAO.instance; }
            private set { LinkDAO.instance = value; }
        }

        private ExcelPackage OpenFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(new FileInfo("./wwwroot/data/links.xlsx"));
            return package;
        }

        private Link loadDate(ExcelWorksheet workSheet, int row)
        {
            var name = workSheet.Cells[row, 1].Value;
            var val = workSheet.Cells[row, 2].Value;
            var altVal = workSheet.Cells[row, 3].Value;

            string Name = name == null ? "N/A" : name.ToString();
            string Val = val == null ? "N/A" : val.ToString();
            string AltVal = altVal == null ? "N/A" : altVal.ToString();
            var link = new Link(Name, Val, AltVal);
            return link;
        }

        public List<Link> GetLinkList()
        {
            var package = OpenFile();
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            var linkList = new List<Link>();
            int i = 2;
            while (workSheet.Cells[i, 1].Value != null)
            {
                var link = loadDate(workSheet, i);
                linkList.Add(link);
                i++;
            }
            return linkList;
        }

        public string GetLink(string var)
        {
            var package = OpenFile();
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            int i = 2;
            while (workSheet.Cells[i, 1].Value != null)
            {
                var key = workSheet.Cells[i, 1].Value;
                string Key = key == null ? "N/A" : key.ToString();
                if (Key == var)
                {
                    var link = workSheet.Cells[i, 2].Value;
                    string Link = link == null ? "N/A" : link.ToString();
                    return Link;
                }
                i++;
            }
            return "N/A";
        }

        public void EditLink(string var, string link)
        {
            var package = OpenFile();
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            int i = 2;
            while (workSheet.Cells[i, 1].Value != null)
            {
                var key = workSheet.Cells[i, 1].Value;
                string Key = key == null ? "N/A" : key.ToString();
                if (Key == var)
                {
                    workSheet.Cells[i, 2].Value = link;
                }
                i++;
            }
            package.Save();
        }
    }
}
