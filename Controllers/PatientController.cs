using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;
        // GET: PatientController
        public PatientController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        public void Connection()
        {
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder()
            {
                DataSource = "192.168.3.182,1433",
                InitialCatalog = "Develop",
                UserID = "sa",
                Password = "Digiman182"
            };
            SqlConnection conn = new SqlConnection(sConnB.ConnectionString);
        }
        [HttpGet]
        public List<Dictionary<string,object>> Index()
        {
            
            List<Dictionary<string, object>> ret = Tool.ToListDic(DBHelper.Query(@"
                SELECT * FROM mos_patient where gender=@gen","DEV",Tool.ToDic("GEn","M")));
            return ret;
        }
        [HttpPut]
        [Route("[action]")]
        public List<Dictionary<string,object>> QueryFile(Dictionary<string,object>arg) {
            //var a = _hostingEnv.WebRootPath;
            //var fileName = Path.GetFileName("GetAllData.sql");
            //var filePath = Path.Combine(_hostingEnv.WebRootPath, "_service\\_sqlHelper\\Patient\\", fileName);
            //string sqlStr = System.IO.File.ReadAllText(filePath);
         
            // HttpContext.Request.Host;// Current.Server.MapPath("/UploadedFiles");
            var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return Tool.ToListDic(DBHelper.Query("Patient.GetAllData.DEV", arg, _hostingEnv));
        }
    }
}
