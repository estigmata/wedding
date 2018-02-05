using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JalaFoundation.Dev23.Wedding.API.Models
{
    public class ImageStreamProvider : MultipartFormDataStreamProvider
    {
        public ImageStreamProvider(string uploadPath) : base(uploadPath)
        {

        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string oldFileName = headers.ContentDisposition.FileName;
            oldFileName = oldFileName.Replace("\"", string.Empty);
            var extension = Path.GetExtension(oldFileName);

            return $@"{Guid.NewGuid()}{extension}";
        }
    }
}