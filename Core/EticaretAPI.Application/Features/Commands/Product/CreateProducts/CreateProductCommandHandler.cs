using EticaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Product.CreateProducts
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _ProductWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository ProductWriteRepository)
        {
            _ProductWriteRepository = ProductWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _ProductWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            }
            );
            await _ProductWriteRepository.SaveAsync();
            return new CreateProductCommandResponse();
        }
    }
}
