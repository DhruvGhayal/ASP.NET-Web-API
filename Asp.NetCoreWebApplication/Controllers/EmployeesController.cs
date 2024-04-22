using Asp.NetCoreWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Asp.NetCoreWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public String GetEmployees()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * From Employee", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> emplist = new List<Employee>();
            Response rs = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["EmpId"]);
                    emp.EmpName = Convert.ToString(dt.Rows[i]["EmpName"]);
                    emp.Password = Convert.ToInt32(dt.Rows[i]["Password"]);
                    emplist.Add(emp);
                }
            }
            if(emplist.Count > 0)
            {
              return  JsonConvert.SerializeObject(emplist); 
            }
            else
            {
                rs.StatusCode = 100;
                rs.ErrorMessage = "No data Found";
               return JsonConvert.SerializeObject(rs);
            }

        }
    }
}
