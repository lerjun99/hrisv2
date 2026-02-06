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
    public class IdentifyFaceHandler
        : IRequestHandler<IdentifyFaceCommand, int?>
    {
        private readonly IFaceRecognitionService _faceService;

        public IdentifyFaceHandler(IFaceRecognitionService faceService)
        {
            _faceService = faceService;
        }

        public async Task<int?> Handle(IdentifyFaceCommand request, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request.ImageBase64))
            {
                // Optional: log warning
                return null; // treat as unidentified
            }

            try
            {
                // Extract embedding from base64 image
                var embedding = await _faceService.ExtractEmbeddingAsync(request.ImageBase64);

                // Identify user by embedding
                var userId = await _faceService.IdentifyAsync(embedding);

                return userId; // null if not matched
            }
            catch (FormatException)
            {
                // Base64 was invalid
                // Optional: log warning
                return null;
            }
            catch (Exception ex)
            {
                // Optional: log unexpected errors
                throw new ApplicationException("Error identifying face", ex);
            }
        }
    }

}
