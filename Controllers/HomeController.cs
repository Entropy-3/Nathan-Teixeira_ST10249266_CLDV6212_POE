using CLDV_SEM2_POE.Models;
using Microsoft.AspNetCore.Mvc;
using CLDV_SEM2_POE.Services;
using System.Diagnostics;

namespace CLDV_SEM2_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlobService blobService;
        private readonly TableService tableService;
        private readonly QueueService queueService;
        private readonly FileService fileService;
        
        //-------------------------------------------------------------------------------------------------------\\
        //constructor
        public HomeController(BlobService blobService1, TableService tableService1, QueueService queueService1, FileService fileService1)
        {
            blobService = blobService1;
            tableService = tableService1;
            queueService = queueService1;
            fileService = fileService1;
        }
        //-------------------------------------------------------------------------------------------------------\\

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that uploads an image to blob storage
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                await blobService.UploadBlobAsync("product-images", file.FileName, stream);
            }
            return RedirectToAction("Index");
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that adds a customer profile to the table storage
        [HttpPost]
        public async Task<IActionResult> AddCustomerProfile(CustomerProfile profile)
        {
            if (ModelState.IsValid)
            {
                await tableService.AddEntityAsync(profile);
            }
            return RedirectToAction("Index");
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that processes an order
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            await queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");
            return RedirectToAction("Index");
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that uploads a contract to the file storage
        [HttpPost]
        public async Task<IActionResult> UploadContract(IFormFile file)
        {
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                await fileService.UploadFileAsync("contracts-logs", file.FileName, stream);
            }
            return RedirectToAction("Index");
        }
    }
}
//---------------------------------------------EOF----------------------------------------------------------\\