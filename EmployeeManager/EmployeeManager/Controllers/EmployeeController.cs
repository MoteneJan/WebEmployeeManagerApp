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

    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get API Method
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT EmployeeId, EmployeeName, Department, convert(varchar(10),DateofJoining,120) AS DateOfJoining, PhotoFileName
                             FROM dbo.Employee";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection con = new SqlConnection(sqlDataSource)) 
            {
                con.Open();
                using(SqlCommand comm = new SqlCommand(query, con))
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
        public JsonResult Post(Employee emp)
        {
            string query = @"INSERT INTO dbo.Employee
                             (EmployeeName, Dapertment, DateofJoining, PhotoFileName)
                             VALUES(@EmployeeName, @Dapertment, @DateofJoining, @PhotoFileName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    comm.Parameters.AddWithValue("@Department", emp.Department);
                    comm.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    comm.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }

        //Put API Method
        [HttpPut]
        public JsonResult Delete(Employee emp)
        {
            string query = @"UPDATE dbo.Employee
                             SET EmployeeName = @EmployeeName, Department = @Department, DateOfJoining = @DateOfJoining, PhotoFileName = @PhotoFileName 
                             WHERE EmployeeId = @EmployeeId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    comm.Parameters.AddWithValue("@Department", emp.Department);
                    comm.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    comm.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Successfully Updated");
        }

        //Delete API Method
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE dbo.Employee
                             WHERE EmployeeId = @EmployeeId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetSqlDataSource("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@EmployeeId", id);
                    myReader = comm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }

        comm.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
    }
    
}
