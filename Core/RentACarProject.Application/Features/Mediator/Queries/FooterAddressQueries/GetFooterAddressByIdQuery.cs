﻿using MediatR;
using RentACarProject.Application.Features.Mediator.Results.FooterAddressResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.FooterAddressQueries
{
    public class GetFooterAddressByIdQuery : IRequest<GetFooterAddressByIdQueryResult>
    {
        public GetFooterAddressByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
