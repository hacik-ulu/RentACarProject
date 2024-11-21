using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.AuthorInterfaces
{
    public interface IAuthorRepository
    {
        Task<List<Blog>> GetBlogListByAuthorIdAsync(int id);
    }
}
