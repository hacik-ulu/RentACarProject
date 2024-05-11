using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.RentCarInterfaces
{
    public interface IRentCarRepository
    {
        Task<List<RentCar>> GetByFilterAsync(Expression<Func<RentCar, bool>> filter);
    }
}
