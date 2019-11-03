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
            var temporaryDocumentIdentifier =
                new TemporaryDocumentIdentifier(documentIdentifier);
            await _storage.WriteFileAsync(
                temporaryDocumentIdentifier.ToString(),
                stream);
            _storage.RenameFile(
                temporaryDocumentIdentifier.ToString(),
                documentIdentifier.ToString());
            return documentIdentifier;
        }

        public IEnumerable<DocumentIdentifier> QueuedDocuments()
        {
            var filePaths = _storage.GetFiles();
            var fileNames = filePaths.Select(x => Path.GetFileName(x));
            var documentIdentifiers = fileNames.Select(x => DocumentIdentifier.Parse(x));
            return documentIdentifiers;
        }
    }
}