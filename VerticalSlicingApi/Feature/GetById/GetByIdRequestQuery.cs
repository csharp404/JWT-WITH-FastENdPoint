using MediatR;
using VerticalSlicingApi.Domain;

namespace VerticalSlicingApi.Feature.GetById
{
    public sealed record GetByIdRequestQuery(int id) : IRequest<Product>;

   
}
