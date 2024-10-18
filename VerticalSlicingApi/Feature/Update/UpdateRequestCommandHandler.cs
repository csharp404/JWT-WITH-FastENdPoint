using MediatR;
using VerticalSlicingApi.Service;

namespace VerticalSlicingApi.Feature.Update
{
    public class UpdateRequestCommandHandler(ProductService service):IRequestHandler<UpdateRequestCommand>
    {
        public Task Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            service.Update(request.Product);
            return Task.CompletedTask;
        }
    }
}
