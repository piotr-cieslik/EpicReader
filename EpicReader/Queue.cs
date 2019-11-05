using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class Queue
    {
        private readonly DocumentStorage _documentStorage;

        public Queue(DocumentStorage documentStorage)
        {
            _documentStorage = documentStorage;
        }

        public async Task<DocumentName> Put(string fileName, Stream stream)
        {
            var documentName =
                new DocumentName(
                    new Timestamp(DateTime.Now),
                    Guid.NewGuid(),
                    new FileName(fileName));
            await _documentStorage.WriteDocumentAsync(
                DocumentStorage.Directory.Temporary,
                documentName,
                stream);
            _documentStorage.MoveDocument(
                documentName,
                DocumentStorage.Directory.Temporary,
                DocumentStorage.Directory.Queued);
            return documentName;
        }

        public IEnumerable<DocumentName> QueuedDocuments()
        {
            return _documentStorage.GetDocumentNames(DocumentStorage.Directory.Queued);
        }

        public IEnumerable<DocumentName> ProcessingDocuments()
        {
            return _documentStorage.GetDocumentNames(DocumentStorage.Directory.Processing);
        }

        public IEnumerable<DocumentName> ProcessedDocuments()
        {
            return _documentStorage.GetDocumentNames(DocumentStorage.Directory.Processed);
        }
    }
}