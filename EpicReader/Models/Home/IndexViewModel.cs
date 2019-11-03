using System.Collections.Generic;
using System.Linq;

namespace EpicReader.Models.Home
{
    public sealed class IndexViewModel
    {
        public IndexViewModel(
            IEnumerable<DocumentIdentifier> queued,
            IEnumerable<string> processed)
        {
            Queued = queued.Select(x => x.FileName());
            Processed = processed;
        }

        public IEnumerable<string> Queued { get; }

        public IEnumerable<string> Processed { get; }
    }
}
