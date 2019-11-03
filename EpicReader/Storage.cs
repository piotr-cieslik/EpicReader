using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class Storage
    {
        private readonly string _root;

        public Storage()
        {
            const string appFolder = "epic-reader";
            _root = Path.Combine(Path.GetTempPath(), appFolder);
        }

        public async Task WriteFileAsync(string fileName, Stream stream)
        {
            var filePath = Path.Combine(_root, fileName);
            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();
        }

        public void RenameFile(string oldName, string newName)
        {
            var oldPath = Path.Combine(_root, oldName);
            var newPath = Path.Combine(_root, newName);
            File.Move(oldPath, newPath);
        }

        public IReadOnlyCollection<string> GetFiles()
        {
            return Directory.GetFiles(_root);
        }
    }
}