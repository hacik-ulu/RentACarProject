﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.Mediator.Commands.CommentCommands;
using RentACarProject.Application.Features.RepositoryPattern;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.CommentDtos;


namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGenericRepository<Comment> _commentsRepository;
        private readonly IMediator _mediator;

        public CommentsController(IGenericRepository<Comment> repository, IMediator mediator)
        {
            _commentsRepository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var comments = _commentsRepository.GetAll();
            var commentDtos = comments.Select(c => new ResultCommentDto
            {
                CommentID = c.CommentID,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                Description = c.Description,
                BlogID = c.BlogID,
                Title = c.Blog.Title
            }).ToList();

            return Ok(commentDtos);
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

        [HttpGet("CommentListByBlog")]
        public IActionResult CommentListByBlog(int id)
        {
            var comments = _commentsRepository.GetCommentsByBlogId(id);
            var commentDtos = comments.Select(c => new ResultCommentDto
            {
                CommentID = c.CommentID,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                Description = c.Description,
                BlogID = c.BlogID,
                Title = c.Blog.Title
            }).ToList();

            return Ok(commentDtos);
        }

        [HttpGet("CommentCountByBlog")]
        public IActionResult CommentCountByBlog(int id)
        {
            var value = _commentsRepository.GetCountCommentByBlog(id);
            return Ok(value);
        }

        [HttpPost("CreateCommentWithMediator")]
        public async Task<IActionResult> CreateCommentWithMediator(CreateCommentCommand command)
        {
            await _mediator.Send(command);
            return Ok("Comment Added!");
        }

    }
}
