using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.BlogCommands;
using RentACarProject.Application.Features.Mediator.Queries.AuthorQueries;
using RentACarProject.Application.Features.Mediator.Queries.BlogQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            var values = await _mediator.Send(new GetBlogQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var value = await _mediator.Send(new GetBlogByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand command)
        {
            await _mediator.Send(command);
            return Ok("Blog Added!");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            await _mediator.Send(new RemoveBlogCommand(id));
            return Ok("Blog Deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand command)
        {
            await _mediator.Send(command);
            return Ok("Blog Updated!");

        }

        [HttpGet("GetLastThreeBlogsWithAuthorsList")]
        public async Task<IActionResult> GetLastThreeBlogsWithAuthorsList()
        {
            var values = await _mediator.Send(new GetLastThreeBlogsWithAuthorsQuery());
            return Ok(values);
        }

        [HttpGet("GetAllBlogsWithAuthorsList")]
        public async Task<IActionResult> GetAllBlogsWithAuthorsList(int page = 1, int pageSize = 3)
        {
            var values = await _mediator.Send(new GetAllBlogsWithAuthorQuery());

            // Pagination 
            var paginatedBlogs = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //Determine the total number of pages.
            Response.Headers["X-Total-Count"] = values.Count().ToString();
            return Ok(paginatedBlogs);
        }

        //BlogID numaraya göre ilgili bloğun bilgileri geliyor
        [HttpGet("GetBlogByAuthorId")]
        public async Task<IActionResult> GetBlogByAuthorId(int id)
        {
            var values = await _mediator.Send(new GetBlogByAuthorIdQuery(id));
            return Ok(values);
        }

        [HttpGet("GetLastEightBlogsList")]
        public async Task<IActionResult> GetLastEightBlogsList()
        {
            var values = await _mediator.Send(new GetLastEightBlogsQuery());
            return Ok(values);
        }
    }
}
