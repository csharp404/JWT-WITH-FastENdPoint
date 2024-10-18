using MediatR;
using VerticalSlicingApi.Domain;
using VerticalSlicingApi.Service;

namespace VerticalSlicingApi.Feature.GetById
{
    public class GetByIdRequestQueryHandler(ProductService service):IRequestHandler<GetByIdRequestQuery,Product>
    {
        public Task<Product> Handle(GetByIdRequestQuery request, CancellationToken cancellationToken)
        {
            var data = service.GetById(request.id);
            return data;
        }
    }
}
