namespace IKProject.Application.Interfaces.File
{
    public interface IDocumentFileHelper
    {
        string Add(IDocumentFile file, string userId);
        bool IsValidFileType(IDocumentFile file);
    }
}
