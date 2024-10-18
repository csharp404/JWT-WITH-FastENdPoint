using MediatR;
using VerticalSlicingApi.Service;

namespace VerticalSlicingApi.Feature.Delete
{
    public class DeleteRequestCommanHandler(ProductService service):IRequestHandler<DeleteRequestCommand>
    {
        public Task Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            service.Delete(request.id);
            return Task.CompletedTask;
        }
    }
}
