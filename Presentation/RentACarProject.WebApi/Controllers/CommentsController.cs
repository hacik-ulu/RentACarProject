using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.RepositoryPattern;
using RentACarProject.Domain.Entities;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGenericRepository<Comment> _commentsRepository;

        public CommentsController(IGenericRepository<Comment> repository)
        {
            _commentsRepository = repository;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _commentsRepository.GetAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {

            _commentsRepository.Create(comment);
            return Ok("Comment added!");
        }

        [HttpDelete]
        public IActionResult RemoveComment(int id)
        {
           var value = _commentsRepository.GetById(id);
            _commentsRepository.Remove(value);
            return Ok("Comment deleted!");
        }

        [HttpPut]
        public IActionResult UpdateComment(Comment comment)
        {
            _commentsRepository.Update(comment);
            return Ok("Comment updated!");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _commentsRepository.GetById(id);
            return Ok(value);
        }
    }
}
