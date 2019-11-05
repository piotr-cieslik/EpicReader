using System;

namespace EpicReader
{
    public sealed class Result
    {
        public Result(
            string status,
            string text,
            DateTime processingStartTime,
            DateTime processingEndTime)
        {
            Status = status;
            Text = text;
            ProcessingStartTime = processingStartTime;
            ProcessingEndTime = processingEndTime;
        }

        public string Status { get; }

        public string Text { get; }

        public DateTime ProcessingStartTime { get; }

        public DateTime ProcessingEndTime { get; }
    }
}
