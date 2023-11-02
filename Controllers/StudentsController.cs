using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NPWalks.API.Controllers
{
    // http://localhost:5282/api/students
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents(){

            string[] studentNames=new string[]{"Bibek","Riya","Sushil","Aashish"};
            return Ok(studentNames);
        }
        
    }
}