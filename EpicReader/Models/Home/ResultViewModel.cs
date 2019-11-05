namespace EpicReader.Models.Home
{
    public class ResultViewModel
    {
        private readonly DocumentName _documentName;
        private readonly Result _result;

        public ResultViewModel(DocumentName documentName, Result result)
        {
            _documentName = documentName;
            _result = result;
        }

        public string FileName() => _documentName.FileName();

        public string Text() => _result.Text;

        public string ProcessingTime() => ((int)(_result.ProcessingEndTime - _result.ProcessingStartTime).TotalSeconds).ToString();
    }
}
