using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hethongquanlylab.DAO;
using Hethongquanlylab.Models;
using System.IO;
using Hethongquanlylab.Common;
using OfficeOpenXml;
using Newtonsoft.Json;
using Hethongquanlylab.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Hethongquanlylab.Controllers.Super.BanDaoTao
{
    public class BDTController : SuperController
    {
        public BDTController()
        {
            unit = "Ban Đào Tạo";
            unitVar = "BDT";
        }
    }
}
