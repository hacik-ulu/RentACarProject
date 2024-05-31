﻿using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.ReviewInterfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsByCarIDAsync(int id);
    }
}