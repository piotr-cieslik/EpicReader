using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EpicReader.Models.Home
{
    public sealed class IndexViewModel
    {
        private readonly IEnumerable<DocumentIdentifier> _queued;
        private readonly IEnumerable<DocumentIdentifier> _processing;
        private readonly IEnumerable<DocumentIdentifier> _processed;
        private readonly IUrlHelper _url;

        public IndexViewModel(
            IEnumerable<DocumentIdentifier> queued,
            IEnumerable<DocumentIdentifier> processing,
            IEnumerable<DocumentIdentifier> processed,
            IUrlHelper url)
        {
            _queued = queued;
            _processing = processing;
            _processed = processed;
            _url = url;
        }

        public IEnumerable<string> Queued() => _queued.Select(x => x.FileName());

        public IEnumerable<string> Processing() => _processing.Select(x => x.FileName());

        public IEnumerable<(string FileName, string Url)> Processed() =>
            _processed.Select(
                x => new
                {
                    FileName = x.FileName(),
                    Url = _url.Action("Result", null, new { documentIdentifier = x.ToString() }),
                })
            .Select(x => (x.FileName, x.Url));
    }
}
