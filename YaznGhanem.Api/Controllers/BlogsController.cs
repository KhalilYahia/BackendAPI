
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Iservices;
using System.Collections.Generic;

namespace firstProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        public readonly IBlogsService _blogService;
        public BlogsController(IBlogsService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// this for test
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpPost("AddNewBlog")]
       
        public IActionResult AddNewBlog(AddBlogDto dto)
        {
            if(!ModelState.IsValid) 
            {
               // return BadRequest(ModelState);
                return ValidationProblem(ModelState);
            }
           var res= _blogService.AddNewBlog(dto);

           return CreatedAtAction(nameof(res),res);
        }
    }
}
