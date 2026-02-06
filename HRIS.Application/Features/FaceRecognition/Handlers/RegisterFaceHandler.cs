using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.FaceRecognition.Handlers
{
    public class RegisterFaceHandler
     : IRequestHandler<RegisterFaceCommand, bool>
    {
        private readonly IFaceRecognitionService _faceService;

        public RegisterFaceHandler(IFaceRecognitionService faceService)
        {
            _faceService = faceService;
        }

        public async Task<bool> Handle(RegisterFaceCommand request, CancellationToken ct)
        {
            var embedding = await _faceService.ExtractEmbeddingAsync(request.ImageBase64);
            await _faceService.SaveEmbeddingAsync(request.UserId, embedding);
            return true;
        }
    }

}
