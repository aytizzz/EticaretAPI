using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public  class RemoveProductImageCommandRequest:IRequest<RemoveProductImageCommandResponse>
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
    }
}
