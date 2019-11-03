using System.Collections.Generic;

namespace EpicReader.Models.Home
{
    public sealed class IndexViewModel
    {
        public IndexViewModel(
            IEnumerable<string> queued,
            IEnumerable<string> processed)
        {
            Queued = queued;
            Processed = processed;
        }

        public IEnumerable<string> Queued { get; }

        public IEnumerable<string> Processed { get; }
    }
}
