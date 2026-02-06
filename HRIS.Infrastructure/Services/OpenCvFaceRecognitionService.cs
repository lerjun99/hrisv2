using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using HRIS.Application.Common.Interfaces;
using HRIS.Domain.Entities;
using HRIS.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class OpenCvFaceRecognitionService : IFaceRecognitionService
    {
        private readonly IHrisDbContext _context;

        public OpenCvFaceRecognitionService(IHrisDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Extracts embedding from a base64 image
        /// </summary>
        public async Task<float[]> ExtractEmbeddingAsync(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("Input image is null or empty", nameof(base64));

            try
            {
                // Remove data URI prefix if exists
                int commaIndex = base64.IndexOf(',');
                if (commaIndex >= 0)
                    base64 = base64[(commaIndex + 1)..]; // C# 8+ slice syntax

                // Remove whitespace, newlines
                base64 = base64.Trim().Replace("\n", "").Replace("\r", "");

                // Some browsers add spaces or unexpected characters; remove them
                base64 = base64.Replace(" ", "").Replace("\t", "");

                // Convert
                var imageBytes = Convert.FromBase64String(base64);

                // TODO: Replace with actual face embedding extraction (OpenCV/FaceNet)
                return await Task.FromResult(new float[128]);
            }
            catch (FormatException ex)
            {
                // Log the original string length for debugging
                Console.WriteLine($"Invalid Base64 string length: {base64.Length}");
                throw new FormatException("The provided string is not valid Base64.", ex);
            }
        }

        /// <summary>
        /// Saves embedding for a user in the database
        /// </summary>
        public async Task SaveEmbeddingAsync(int userId, float[] embedding)
        {
            if (embedding == null || embedding.Length == 0)
                throw new ArgumentException("Embedding is null or empty", nameof(embedding));

            _context.FaceProfiles.Add(new FaceProfile
            {
                UserId = userId,
                Embedding = Serialize(embedding),
                CreatedAt = DateTime.UtcNow
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.Storage.RetryLimitExceededException ex)
            {
                // Log inner exception for actual cause
                Console.WriteLine("SQL Error: " + ex.InnerException?.Message);
                throw; // rethrow so caller knows
            }
        }

        /// <summary>
        /// Identify user by face embedding
        /// </summary>
        public async Task<int?> IdentifyAsync(float[] input)
        {
            if (input == null || input.Length == 0)
                return null;

            var faces = _context.FaceProfiles.ToList(); // consider AsNoTracking for read-only

            foreach (var face in faces)
            {
                var stored = Deserialize(face.Embedding);
                var score = CosineSimilarity(input, stored);
                if (score > 0.85f) // threshold
                    return face.UserId;
            }

            return null;
        }

        #region --- Helpers ---

        private byte[] Serialize(float[] embedding)
        {
            var bytes = new byte[embedding.Length * sizeof(float)];
            Buffer.BlockCopy(embedding, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private float[] Deserialize(byte[] bytes)
        {
            var floats = new float[bytes.Length / sizeof(float)];
            Buffer.BlockCopy(bytes, 0, floats, 0, bytes.Length);
            return floats;
        }

        private float CosineSimilarity(float[] v1, float[] v2)
        {
            if (v1.Length != v2.Length) return 0;

            float dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }
            return dot / (float)(Math.Sqrt(normA) * Math.Sqrt(normB));
        }

        #endregion
    }
}
