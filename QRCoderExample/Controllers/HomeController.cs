using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using QRCoderExample.Models;

namespace QRCoderExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        
        [ValidateAntiForgeryToken]      
        [HttpPost]      
        public IActionResult Index(string txtQRCode) {      
            QRCodeGenerator _qrCode = new QRCodeGenerator();      
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode,QRCodeGenerator.ECCLevel.Q);      
            QRCode qrCode = new QRCode(_qrCodeData);      
            Bitmap qrCodeImage = qrCode.GetGraphic(20);      
            return View(BitmapToBytesCode(qrCodeImage));      
        }      
        [NonAction]      
        private static Byte[] BitmapToBytesCode(Bitmap image)      
        {      
            using (MemoryStream stream = new MemoryStream())      
            {      
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);      
                return stream.ToArray();      
            }      
        }  
        
    }
}