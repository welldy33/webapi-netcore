using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Helper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnv;

        // GET: PatientController
        [Obsolete]
        public PatientController(IHostingEnvironment hostingEnv, ILoggerManager logger)
        {
            _hostingEnv = hostingEnv;
            _logger = logger;
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
        [Obsolete]
        public List<Dictionary<string,object>> QueryFile(Dictionary<string,object>arg) {
            //var a = _hostingEnv.WebRootPath;
            //var fileName = Path.GetFileName("GetAllData.sql");
            //var filePath = Path.Combine(_hostingEnv.WebRootPath, "_service\\_sqlHelper\\Patient\\", fileName);
            //string sqlStr = System.IO.File.ReadAllText(filePath);

            // HttpContext.Request.Host;// Current.Server.MapPath("/UploadedFiles");
            throw new CustomsException("Cannot Query Now");
            return Tool.ToListDic(DBHelper.Query("Patient.GetAllData.DEV", arg, _hostingEnv));
        }
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<string> GetLog()
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public void SaveNewData (Dictionary<string,object>arg){
            DBHelper.Execute("Insert#mos_patient@DEV", arg);
        }
    }
}
