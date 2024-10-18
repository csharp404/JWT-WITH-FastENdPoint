using MediatR;
using VerticalSlicingApi.Domain;

namespace VerticalSlicingApi.Feature.GetAll
{
    public class GetAllRequestQuery : IRequest<List<Product>>
    {

    }
}
