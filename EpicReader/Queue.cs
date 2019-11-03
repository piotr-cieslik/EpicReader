using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class Queue
    {
        private DocumentStorage _storage;

        public Queue()
        {
            _storage = new DocumentStorage();
        }

        public async Task<DocumentIdentifier> Put(string fileName, Stream stream)
        {
            var documentIdentifier =
                new DocumentIdentifier(
                    DateTime.Now,
                    fileName);
            await _storage.WriteDocumentAsync(
                DocumentStorage.Directory.Temporary,
                documentIdentifier,
                stream);
            _storage.MoveDocument(
                documentIdentifier,
                DocumentStorage.Directory.Temporary,
                DocumentStorage.Directory.Queued);
            return documentIdentifier;
        }

        public IEnumerable<DocumentIdentifier> QueuedDocuments()
        {
            var filePaths = _storage.GetDocument(DocumentStorage.Directory.Queued);
            var fileNames = filePaths.Select(x => Path.GetFileName(x));
            var documentIdentifiers = fileNames.Select(x => DocumentIdentifier.Parse(x));
            return documentIdentifiers;
        }
    }
}