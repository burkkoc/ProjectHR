using System;
using System.IO;
using System.Linq;
using IKProject.Application.Interfaces.File;

namespace IKProject.Application.Features.Custom
{
    public class DocumentFileHelper : IDocumentFileHelper
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".txt", ".pdf", ".doc", ".docx" };
        private readonly string _rootDirectory;

        public DocumentFileHelper(string rootDirectory)
        {
            _rootDirectory = rootDirectory ?? throw new ArgumentNullException(nameof(rootDirectory));
        }

        public bool IsValidFileType(IDocumentFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(extension);
        }

        public string Add(IDocumentFile file, string userId)
        {
            if (file == null || file.Length <= 0 || !IsValidFileType(file))
                throw new InvalidOperationException("Invalid file type or empty file.");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

            var directory = Path.Combine(_rootDirectory, "uploads", userId);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = Path.Combine(directory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.OpenReadStream().CopyTo(stream);
            }

            return filePath;
        }
    }

}
