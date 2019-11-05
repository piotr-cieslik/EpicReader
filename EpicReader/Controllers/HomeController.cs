using System.Diagnostics;
using System.Threading.Tasks;
using EpicReader.Models;
using EpicReader.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EpicReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DocumentStorage _documentStorage;
        private readonly Queue _queue;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _documentStorage = new DocumentStorage();
            _queue = new Queue(_documentStorage);
        }

        public IActionResult Index()
        {
            var queued = _queue.QueuedDocuments();
            var processing = _queue.ProcessingDocuments();
            var processed = _queue.ProcessedDocuments();
            var viewModel =
                new IndexViewModel(
                    queued,
                    processing,
                    processed,
                    Url);
            return View(viewModel);
        }

        public async Task<IActionResult> Process(IFormFile file)
        {
            await _queue.Put(file.FileName, file.OpenReadStream());
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Result(string documentName)
        {
            var result =
                await _documentStorage.GetResultAsync(
                    new DocumentName(documentName));
            var viewModel =
                new ResultViewModel(
                    new DocumentName(documentName),
                    result);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
