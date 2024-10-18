using MediatR;

namespace VerticalSlicingApi.Feature.Delete
{
    public sealed record DeleteRequestCommand(int id) : IRequest;
}
