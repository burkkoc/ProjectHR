using System.IO;
using IKProject.Application.Interfaces.File;

namespace IKProject.Application.Features.Custom
{
    public class DocumentFile : IDocumentFile
    {
        public byte[] Content { get; }
        public string FileName { get; }

        public DocumentFile(byte[] content, string fileName)
        {
            Content = content;
            FileName = fileName;
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(Content);
        }

        public long Length => Content.Length;
    }
}
