namespace EpicReader.Models.Home
{
    public class ResultViewModel
    {
        private readonly DocumentIdentifier _documentIdentifier;
        private readonly string _text;

        public ResultViewModel(DocumentIdentifier documentIdentifier, string text)
        {
            _documentIdentifier = documentIdentifier;
            _text = text;
        }

        public string FileName() => _documentIdentifier.FileName();

        public string Text() => _text;
    }
}
