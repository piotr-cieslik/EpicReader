using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class DocumentStorage
    {
        private readonly string _root;

        public DocumentStorage()
        {
            const string appFolder = "epic-reader";
            _root = Path.Combine(Path.GetTempPath(), appFolder);

            foreach(var director in Enum.GetValues(typeof(Directory)).Cast<Directory>())
            {
                System.IO.Directory.CreateDirectory(PathOfDirectory(director));
            }
        }

        public async Task WriteDocumentAsync(
            Directory directory,
            DocumentIdentifier documentIdentifier,
            Stream stream)
        {
            var filePath = PathOfFileInDirectory(documentIdentifier.ToString(), directory);
            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();
        }

        public void MoveDocument(
            DocumentIdentifier documentIdentifier,
            Directory source,
            Directory target)
        {
            var oldPath = PathOfFileInDirectory(documentIdentifier.ToString(), source);
            var newPath = PathOfFileInDirectory(documentIdentifier.ToString(), target);
            File.Move(oldPath, newPath);
        }

        public IReadOnlyCollection<DocumentIdentifier> GetDocument(Directory directory)
        {
            return System.IO.Directory.GetFiles(PathOfDirectory(directory))
                .Select(x => Path.GetFileName(x))
                .Select(x => DocumentIdentifier.Parse(x))
                .ToArray();
        }

        public async Task<byte[]> GetContentAsync(
            Directory directory,
            DocumentIdentifier documentIdentifier)
        {
            return await File.ReadAllBytesAsync(
                PathOfFileInDirectory(
                    documentIdentifier.ToString(),
                    directory));
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