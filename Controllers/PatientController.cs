using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBHelper;
using System.Data;
using System.Data.SqlClient;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        // GET: PatientController
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
            DataSet ds = new DataSet();
            ds.Tables.Add(Instance.Query());
            return Tool.ToListDic(ds);
        }
      
    }
}
