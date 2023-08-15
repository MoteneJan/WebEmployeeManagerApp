using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get API Method
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT DepartmentId, DepartmentName 
                             FROM dbo.Department";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }

        //Post API Method
        [HttpPost]
        public JsonResult Post(Department dept)
        {
            string query = @"INSERT INTO dbo.Department
                             VALUES(@DepartmentName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Successfully Added");
        }

        //Put API Method
        [HttpPut]
        public JsonResult Delete(Department dept)
        {
            string query = @"UPDATE dbo.Department
                             SET DepartmentName = @DepartmentName
                             WHERE DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@DepartmentId", dept.DepartmentId);
                    comm.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Successfully Updated");
        }

        //Delete API Method
        [HttpDelete({"id"})]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM dbo.Department
                             WHERE Departmentid = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Successfully Deleted");
        }

        comm.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
    }
    
}
