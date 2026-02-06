using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.FaceRecognition.Commands
{
    public record RegisterFaceCommand(
        int UserId,
        string ImageBase64
    ) : IRequest<bool>;

}
