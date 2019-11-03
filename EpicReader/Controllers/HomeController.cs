using System.Diagnostics;
using System.Linq;
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
        private readonly Queue _queue;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _queue = new Queue();
        }

        public IActionResult Index()
        {
            var documentIdentifiers = _queue.QueuedDocuments();
            var viewModel =
                new IndexViewModel(
                    documentIdentifiers,
                    Enumerable.Empty<string>());
            return View(viewModel);
        }

        public async Task<IActionResult> Process(IFormFile file)
        {
            await _queue.Put(file.FileName, file.OpenReadStream());
            return new EmptyResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
