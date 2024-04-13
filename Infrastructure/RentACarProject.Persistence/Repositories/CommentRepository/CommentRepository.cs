﻿using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Features.RepositoryPattern;
using RentACarProject.Domain.Entities;
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
            return _context.Comments.Select(x => new Comment
            {
                Name = x.Name,
                Description = x.Description,
                BlogID = x.BlogID,
                CreatedDate = x.CreatedDate,
                CommentID = x.CommentID
            }).ToList();

        }

        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }

        public List<Comment> GetCommentsByBlogId(int id)
        {
            return _context.Set<Comment>().Where(x => x.BlogID == id).ToList();
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
    }
}
