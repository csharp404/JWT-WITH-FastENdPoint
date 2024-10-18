using MediatR;
using VerticalSlicingApi.Domain;

namespace VerticalSlicingApi.Feature.Update
{
    public sealed record UpdateRequestCommand(Product Product) : IRequest;

}
