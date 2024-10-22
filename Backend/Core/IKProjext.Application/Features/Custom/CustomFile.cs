using IKProject.Application.Interfaces.File;
using System.IO;

namespace IKProject.Application.Features.Custom
{
    public class CustomFile : IFile
    {
        public byte[] Content { get; }
        public string FileName { get; }

        public CustomFile(byte[] content)
        {
            Content = content;
            FileName = "photo.jpg"; // Provide a default name or get it from some other source
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(Content);
        }

        public long Length => Content.Length;
    }
}
