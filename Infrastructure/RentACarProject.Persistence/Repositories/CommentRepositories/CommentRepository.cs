using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Features.RepositoryPattern;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.CommentDtos;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CommentRepositories
{
    public class CommentRepository<T> : IGenericRepository<Comment>
    {
        private readonly RentACarContext _context;

        public CommentRepository(RentACarContext context)
        {
            _context = context;
        }

        public void Create(Comment entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public List<Comment> GetAll()
        {
            // Select comments with desired properties and include Blog data
            var comments = _context.Comments
                .Include(comment => comment.Blog) // Include Blog navigation property
                .Select(comment => new Comment
                {
                    CommentID = comment.CommentID,
                    BlogID = comment.BlogID,
                    CreatedDate = comment.CreatedDate,
                    Description = comment.Description,
                    Name = comment.Name,
                    // Access Blog properties directly through navigation property
                    Blog = comment.Blog // Access Blog object with properties like Title
                })
                .ToList();

            return comments;
        }



        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }

        public List<Comment> GetCommentsByBlogId(int id)
        {
            return _context.Set<Comment>()
                    .Include(c => c.Blog) 
                    .Where(x => x.BlogID == id)
                    .Select(c => new Comment
                    {
                        CommentID = c.CommentID,
                        Name = c.Name,
                        CreatedDate = c.CreatedDate,
                        Description = c.Description,
                        BlogID = c.BlogID,
                        Blog = new Blog
                        {
                            Title = c.Blog.Title 
                        }
                    })
                    .ToList();
        }

        public void Remove(Comment entity)
        {
            var value = _context.Comments.Find(entity.CommentID);
            _context.Comments.Remove(value);
            _context.SaveChanges();
        }

        public void Update(Comment entity)
        {
            _context.Update(entity);
            _context.SaveChanges();

        }

        public int GetCountCommentByBlog(int id)
        {
            return (_context.Comments.Where(x => x.BlogID == id).Count());
        }
    }
}
