using MediatR;
using VerticalSlicingApi.Domain;
using VerticalSlicingApi.Service;

namespace VerticalSlicingApi.Feature.GetAll
{
    public class GetAllRequestQueryHandler(ProductService service):IRequestHandler<GetAllRequestQuery,List<Product>>
    {
        public async Task<List<Product>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
                var data = await service.GetAll();
                return data;
        }
    }
}
