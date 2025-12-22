using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, AppDBContext db)
        {
            _db = db;
            _mapper = mapper;

        }


        // GET: api/values
        [HttpGet("Get_Category")]
        public async Task<ActionResult<Category_Tbl_ViewModel>> Get_Category()
        {
            var data = _db.Category_Tbl.Where(x => x.StatusID != 255).ToListAsync();
            return _mapper.Map<Category_Tbl_ViewModel>(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

