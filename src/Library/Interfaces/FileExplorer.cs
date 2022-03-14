using System.IO;
using System.Collections.Generic;

namespace PathFinder.Interfaces{
    public interface FileExplorer
    {
        FileExplorer goToParent();
        FileExplorer goToDir(string dirname);
        string[] getFiles();
        string[] getDirs();
    }
}