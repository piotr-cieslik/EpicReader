namespace EpicReader.Models.Home
{
    public class ResultViewModel
    {
        private readonly DocumentName _documentName;
        private readonly string _text;

        public ResultViewModel(DocumentName documentName, string text)
        {
            _documentName = documentName;
            _text = text;
        }

        public string FileName() => _documentName.FileName();

        public string Text() => _text;
    }
}
