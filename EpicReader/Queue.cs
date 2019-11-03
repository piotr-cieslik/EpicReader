using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EpicReader
{
    internal sealed class Queue
    {
        private Storage _storage;

        public Queue()
        {
            _storage = new Storage();
        }

        public async Task<string> Put(string fileName, Stream stream)
        {
            var temporaryFileName = fileName + ".tmp";
            await _storage.WriteFileAsync(temporaryFileName, stream);
            _storage.RenameFile(temporaryFileName, fileName);
            return string.Empty;
        }

        public IEnumerable<string> List() => _storage.GetFiles();
    }
}