using System;
using System.IO;
using IKProject.Application.Interfaces.File;

namespace IKProject.Application.Features.Custom
{
    public class FileHelper : IFileHelper
    {
        private readonly string _rootDirectory;

        public FileHelper(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        public string Add(IFile file, string root, string userId)
        {
            if (file == null || file.Length <= 0) return null;

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
