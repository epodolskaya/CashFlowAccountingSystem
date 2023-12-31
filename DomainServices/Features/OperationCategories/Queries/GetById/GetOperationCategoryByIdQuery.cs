﻿using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

public class GetOperationCategoryByIdQuery : IRequest<OperationCategory>
{
    public long Id { get; set; }

    public GetOperationCategoryByIdQuery(long id)
    {
        Id = id;
    }
}