using MediatR;
using VerticalSlicingApi.DTOs;
using VerticalSlicingApi.Service;

namespace VerticalSlicingApi.Feature.Create
{
    public class CreateRequestCommandHandler(ProductService service):IRequestHandler<CreateRequestCommand>
    {
        public Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            service.Create(request.product);
            return Task.CompletedTask;
        }
    }
}
