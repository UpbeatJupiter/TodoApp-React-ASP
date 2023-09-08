using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace todoapp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoAppController : Controller
	{
		private IConfiguration _configuration;
		public TodoAppController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		[Route("GetNotes")]
		public JsonResult GetNotes()
		{
			string query = "select * from dbo.notes";
			DataTable dt = new DataTable();
			string sqlDatasource = _configuration.GetConnectionString("todoappDBcon");
			SqlDataReader myReader;
			using (SqlConnection myConn = new SqlConnection(sqlDatasource))
			{
				myConn.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myConn))
				{
					myReader = myCommand.ExecuteReader();
					dt.Load(myReader);
					myReader.Close();
					myConn.Close();
				}
			}
			return new JsonResult(dt);
		}

		[HttpPost]
		[Route("AddNotes")]
		public JsonResult AddNotes([FromForm] string newNotes)
		{
			string query = "insert into dbo.notes values(@newNotes)";
			DataTable dt = new DataTable();
			string sqlDatasource = _configuration.GetConnectionString("todoappDBcon");
			SqlDataReader myReader;
			using (SqlConnection myConn = new SqlConnection(sqlDatasource))
			{
				myConn.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myConn))
				{
					myCommand.Parameters.AddWithValue("@newNotes", newNotes);
					myReader = myCommand.ExecuteReader();
					dt.Load(myReader);
					myReader.Close();
					myConn.Close();
				}
			}
			return new JsonResult("Added Successfully");
		}

		[HttpDelete]
		[Route("DeleteNotes")]
		public JsonResult DeleteNotes(int id)
		{
			string query = "delete from dbo.notes where id = @id";
			DataTable dt = new DataTable();
			string sqlDatasource = _configuration.GetConnectionString("todoappDBcon");
			SqlDataReader myReader;
			using (SqlConnection myConn = new SqlConnection(sqlDatasource))
			{
				myConn.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myConn))
				{
					myCommand.Parameters.AddWithValue("@id", id);
					myReader = myCommand.ExecuteReader();
					dt.Load(myReader);
					myReader.Close();
					myConn.Close();
				}
			}
			return new JsonResult("Deleted Successfully");
		}
	}
}
