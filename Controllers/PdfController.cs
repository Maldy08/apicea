using apicea.Utility;
using Gehtsoft.PDFFlow.Builder;
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

        [HttpGet]
        public FileStream CreatePDF()
        {

            //var documentBuilder = DocumentBuilder.New();
            DocumentBuilder.New()
            //Add a section and section content:
            .AddSection()
                .AddParagraph("Hello World!")
            //Build a file:
            .ToDocument().Build("Result.pdf");

            var myStream = new FileStream("Result.pdf", FileMode.Create);
            //Create a document builder:
            DocumentBuilder.New()
            //Add a section and section content:
            .AddSection()
                .AddParagraph("Hello World!")
            //Build a file:
            .ToDocument().Build(myStream);
            //myStream.Close();
            return myStream;

        }

    }
}
