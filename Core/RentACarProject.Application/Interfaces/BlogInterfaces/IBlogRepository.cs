﻿using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.BlogInterfaces
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetLastThreeBlogsWithAuthorsAsync();
        Task<List<Blog>> GetAllBlogsWithAuthorsAsync();

    }
}
