using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.TagCloudInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.TagCloudRepositories
{
    public class TagCloudRepository : ITagCloudRepository
    {
        private readonly RentACarContext _context;

        public TagCloudRepository(RentACarContext repository)
        {
            _context = repository;
        }

        public Task<List<TagCloud>> GetTagCloudsByBlogID(int id)
        {
            var values = _context.TagClouds.Where(x => x.BlogID == id).ToListAsync();
            return values;
        }
    }
}
