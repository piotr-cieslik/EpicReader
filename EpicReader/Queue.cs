using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<DocumentName> Put(string fileName, Stream stream)
        {
            var documentName =
                new DocumentName(
                    new Timestamp(DateTime.Now),
                    Guid.NewGuid(),
                    new FileName(fileName));
            await _storage.WriteDocumentAsync(
                DocumentStorage.Directory.Temporary,
                documentName,
                stream);
            _storage.MoveDocument(
                documentName,
                DocumentStorage.Directory.Temporary,
                DocumentStorage.Directory.Queued);
            return documentName;
        }

        public IEnumerable<DocumentName> QueuedDocuments()
        {
            return _storage.GetDocument(DocumentStorage.Directory.Queued);
        }

        public IEnumerable<DocumentName> ProcessingDocuments()
        {
            return _storage.GetDocument(DocumentStorage.Directory.Processing);
        }

        public IEnumerable<DocumentName> ProcessedDocuments()
        {
            return _storage.GetDocument(DocumentStorage.Directory.Processed);
        }

        public async Task<string> ResultAsync(DocumentName documentName)
        {
            var bytes = await _storage.GetContentAsync(DocumentStorage.Directory.Result, documentName);
            var text = System.Text.Encoding.UTF8.GetString(bytes);
            return text;
        }
    }
}