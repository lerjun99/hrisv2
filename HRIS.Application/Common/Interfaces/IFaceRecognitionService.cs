using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces
{
    public interface IFaceRecognitionService
    {
        Task<float[]> ExtractEmbeddingAsync(string base64Image);
        Task<int?> IdentifyAsync(float[] embedding);
        Task SaveEmbeddingAsync(int userId, float[] embedding);
    }
}
