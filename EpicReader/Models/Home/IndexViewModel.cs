using System.Collections.Generic;
using System.Linq;

namespace EpicReader.Models.Home
{
    public sealed class IndexViewModel
    {
        private readonly IEnumerable<DocumentIdentifier> _queued;
        private readonly IEnumerable<DocumentIdentifier> _processing;
        private readonly IEnumerable<DocumentIdentifier> _processed;

        public IndexViewModel(
            IEnumerable<DocumentIdentifier> queued,
            IEnumerable<DocumentIdentifier> processing,
            IEnumerable<DocumentIdentifier> processed)
        {
            _queued = queued;
            _processing = processing;
            _processed = processed;
        }

        public IEnumerable<string> Queued() => _queued.Select(x => x.FileName());

        public IEnumerable<string> Processing() => _processing.Select(x => x.FileName());

        public IEnumerable<string> Processed() => _processed.Select(x => x.FileName());
    }
}
