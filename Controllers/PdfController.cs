using apicea.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace apicea.Controllers
{
    [Route("api-viaticos/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private IConverter _converter;
        private IWebHostEnvironment _environment;

        public PdfController(IConverter converter, IWebHostEnvironment environment)
        {
            _converter = converter;
            _environment = environment;
        }
        
        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
            
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHtmlString2(_environment),
               // WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(_environment.WebRootPath, "assets", "bootstrap.min.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file =  _converter.Convert(pdf);
            
            return File(file, "application/pdf", "Viaticos.pdf");
        }

    }
}
