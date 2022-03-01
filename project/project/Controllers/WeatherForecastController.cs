using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project.Common;
using System.Data;
using static project.DB;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("GetList")]
        public List<AccountUser> GetList(AccountUser account1)
        {
            Parameter parameters = new Parameter() { parameter = "@UserID", length = 0, type = "int", value = account1.UserID };
            DB insert = new DB();
            List<AccountUser> list = new List<AccountUser>();          
            insert.GetOne("usp_Area_GetListByDeptID", parameters,out DataSet ds);
            /*if (ds.Tables[0].Rows.Count < 1)
            {
                return Result.ResultOk();
            }*/
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AccountUser account = new AccountUser();
                account.FullName = row["FullName"].ToString();
                account.UserID = row["UserID"].GetHashCode();
                list.Add(account);
            }
            return list;
        }
        [HttpPost("Insert")]
        public Result Insert(AccountUser account1)
        {
            Result resulttrue = new Result(true);
            Result resultfalse = new Result(false);
            Procedure insert = new Procedure();
            insert.Insert("usp_Insert","@DeptID", "@TestingRoomID", account1.UserID, account1.UserID,out bool isSuccess);
            if (isSuccess == false) return resultfalse;
            else return resulttrue;
        }
        [HttpPost("GetOne")]
        public Result GetOne(AccountUser account1)
        {
            Result resulttrue = new Result(true);
            Result resultfalse = new Result(false);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { parameter = "@UserID", length = 0, type = "bigint", value = account1.UserID });
            DB insert = new DB();
            insert.StoreResuftOutput("usp_Area_GetListByDeptID", parameters, out string result);
            return resultfalse;
        }
        [HttpPost("InsertTest")]
        public Result GetOne(Test test)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { parameter = "@a", length = 0, type = "int", value = test.a });
            parameters.Add(new Parameter { parameter = "@b", length = 0, type = "nvarchar", value = test.b });
            Test.GetParameterOutput(parameters, out ParameterOutput output);
            if (output == null) return Result.ResultOk;
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter { parameter = "@a", length = 0, type = "int", value = output.paramOutput1 });
            parameter.Add(new Parameter { parameter = "@b", length = 0, type = "nvarchar", value = output.paramOutput });
            Test.Insert(parameter,out string result);
            if(result==null) return Result.ResultOk;
            return Result.ResultOk;
        }
        [HttpPost("GetOne1")]
        public Result GetOne1(Test test)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { parameter = "@a", length = 0, type = "int", value = test.a });
            parameters.Add(new Parameter { parameter = "@b", length = 0, type = "nvarchar", value = test.b });
            Test.GetTestbyID(parameters, out DataSet output);
            List<Test> list = new List<Test>();
            Test test1 = new Test();
            foreach (DataRow row in output.Tables[0].Rows)
            {
                test1.b = row["b"].ToString();
                test1.a = row["a"].GetHashCode();
            }
            /*if (output == null) return Result.ResultOk;*/
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter { parameter = "@a", length = 0, type = "int", value = test1.a });
            parameter.Add(new Parameter { parameter = "@b", length = 0, type = "nvarchar", value = test1.b });
            Test.Insert(parameter, out string result);
            if (result == null) return Result.ResultOk;
            return Result.ResultOk;
        }
    }
}
