using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.File
{
    public interface IFileHelper
    {
        string Add(IFile file, string root, string userId);
    }
}
