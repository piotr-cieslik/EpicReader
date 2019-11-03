namespace EpicReader
{
    public sealed class TemporaryDocumentIdentifier
    {
        private readonly DocumentIdentifier _documentIdentifier;

        public TemporaryDocumentIdentifier(DocumentIdentifier documentIdentifier) => _documentIdentifier = documentIdentifier;

        public override string ToString() => _documentIdentifier.ToString() + ".tmp";
    }
}
