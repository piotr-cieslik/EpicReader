using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            foreach(var director in Enum.GetValues(typeof(Directory)).Cast<Directory>())
            {
                System.IO.Directory.CreateDirectory(PathOfDirectory(director));
            }
        }

        public async Task WriteFileAsync(
            Directory directory,
            string fileName,
            Stream stream)
        {
            var filePath = PathOfFileInDirectory(fileName, directory);
            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();
        }

        public void MoveFile(string fileName, Directory source, Directory target)
        {
            var oldPath = PathOfFileInDirectory(fileName, source);
            var newPath = PathOfFileInDirectory(fileName, target);
            File.Move(oldPath, newPath);
        }

        public IReadOnlyCollection<string> GetFilesInDirectory(Directory directory)
        {
            return System.IO.Directory.GetFiles(PathOfDirectory(directory));
        }

        private string PathOfFileInDirectory(string fileName, Directory directory)
        {
            return Path.Combine(PathOfDirectory(directory), fileName);
        }

        private string PathOfDirectory(Directory directory)
        {
            return Path.Combine(_root, NameOfDirectory(directory));
        }

        private string NameOfDirectory(Directory directory)
        {
            return directory switch
            {
                Directory.Temporary => "temporary",
                Directory.Queued => "queued",
                Directory.Processing => "processing",
                Directory.Processed => "processed",
                Directory.Result => "result",
                _ => throw new ArgumentException(),
            };
        }

        public enum Directory
        {
            Temporary,
            Queued,
            Processing,
            Processed,
            Result,
        }
    }
}