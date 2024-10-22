using System.IO;

namespace IKProject.Application.Interfaces.File
{
    public interface IDocumentFile
    {
        string FileName { get; }
        Stream OpenReadStream();
        long Length { get; }
    }
}
