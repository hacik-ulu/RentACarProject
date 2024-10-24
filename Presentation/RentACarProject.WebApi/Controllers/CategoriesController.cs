﻿using Microsoft.AspNetCore.Mvc;
using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations;
using RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;
using RentACarProject.Application.Features.CQRS.Queries.CategoryQueries;

namespace RentACarProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        private readonly GetCategoryByIdQueryHandler _getCategoryByIdQueryHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;
        private readonly GetCategoryWithBlogCountHandler _getCategoryWithBlogCountHandler;

        public CategoriesController(CreateCategoryCommandHandler createCategoryCommandHandler, GetCategoryByIdQueryHandler getCategoryByIdQueryHandler, GetCategoryQueryHandler getCategoryQueryHandler, UpdateCategoryCommandHandler updateCategoryCommandHandler, RemoveCategoryCommandHandler removeCategoryCommandHandler, GetCategoryWithBlogCountHandler getCategoryWithBlogCountHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _getCategoryByIdQueryHandler = getCategoryByIdQueryHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _removeCategoryCommandHandler = removeCategoryCommandHandler;
            _getCategoryWithBlogCountHandler = getCategoryWithBlogCountHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _getCategoryQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var values = await _getCategoryByIdQueryHandler.Handle(new GetCategoryByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _createCategoryCommandHandler.Handle(command);
                return Ok("Category added!");
            }
           
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _removeCategoryCommandHandler.Handle(new RemoveCategoryCommand(id));
            return Ok("Category deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _updateCategoryCommandHandler.Handle(command);
                return Ok("Category added!");
            }
        }

        [HttpGet("GetCategoryWithBlogCount")]
        public async Task<IActionResult> GetCategoryWithBlogCount()
        {
            var values = await _getCategoryWithBlogCountHandler.Handle();
            return Ok(values);
        }


    }
}
