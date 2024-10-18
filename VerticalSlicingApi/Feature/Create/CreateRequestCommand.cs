using MediatR;
using VerticalSlicingApi.DTOs;

namespace VerticalSlicingApi.Feature.Create
{
    public class CreateRequestCommand : IRequest<bool>, IRequest
    {
      public   ProductDTO product;

      public CreateRequestCommand(ProductDTO product)
      {
          this.product = product;
      }
    }
}
