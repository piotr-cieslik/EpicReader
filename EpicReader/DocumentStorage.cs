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
            DocumentName documentName,
            Stream stream)
        {
            var filePath = PathOfFileInDirectory(documentName.ToString(), directory);
            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();
        }

        public void MoveDocument(
            DocumentName documentName,
            Directory source,
            Directory target)
        {
            var oldPath = PathOfFileInDirectory(documentName.ToString(), source);
            var newPath = PathOfFileInDirectory(documentName.ToString(), target);
            File.Move(oldPath, newPath);
        }

        public IReadOnlyCollection<DocumentName> GetDocumentNames(Directory directory)
        {
            return System.IO.Directory.GetFiles(PathOfDirectory(directory))
                .Select(x => Path.GetFileName(x))
                .Select(x => new DocumentName(x))
                .ToArray();
        }

        public async Task<Result> GetResultAsync(DocumentName documentName)
        {
            var result =
                await File.ReadAllTextAsync(
                    PathOfFileInDirectory(
                        documentName.ToString() + ".json",
                        Directory.Result));
            var serializableResult =
                System.Text.Json.JsonSerializer.Deserialize<SerializableResult>(result);
            return new Result(
                serializableResult.status,
                serializableResult.text,
                DateTimeOffset.FromUnixTimeSeconds(serializableResult.processingStartTime).UtcDateTime,
                DateTimeOffset.FromUnixTimeSeconds(serializableResult.processingEndTime).UtcDateTime);
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

        public sealed class SerializableResult
        {
            public string status { get; set; }

            public string text { get; set; }

            public long processingStartTime { get; set; }

            public long processingEndTime { get; set; }
        }
    }
}