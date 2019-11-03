using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class Queue
    {
        private Storage _storage;

        public Queue()
        {
            _storage = new Storage();
        }

        public async Task<DocumentIdentifier> Put(string fileName, Stream stream)
        {
            var documentIdentifier =
                new DocumentIdentifier(
                    DateTime.Now,
                    fileName);
            await _storage.WriteFileAsync(
                Storage.Directory.Temporary,
                documentIdentifier.ToString(),
                stream);
            _storage.MoveFile(
                documentIdentifier.ToString(),
                Storage.Directory.Temporary,
                Storage.Directory.Queued);
            return documentIdentifier;
        }

        public IEnumerable<DocumentIdentifier> QueuedDocuments()
        {
            var filePaths = _storage.GetFilesInDirectory(Storage.Directory.Queued);
            var fileNames = filePaths.Select(x => Path.GetFileName(x));
            var documentIdentifiers = fileNames.Select(x => DocumentIdentifier.Parse(x));
            return documentIdentifiers;
        }
    }
}