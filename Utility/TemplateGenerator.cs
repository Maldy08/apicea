using System.Text;

namespace apicea.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            var employees = DataStorage.GetAllEmployees();
            var sb = new StringBuilder();
            sb.Append(@"

                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");
            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }

        public static string GetHtmlString2(IWebHostEnvironment hostEnvironment)
        {
            var sb = new StringBuilder();
           // var css = Path.Combine(hostEnvironment.WebRootPath, "assets", "bootstrap.min.css");
            //var logo = Path.Combine(hostEnvironment.WebRootPath, "assets", "logobcycea.jpg");


            sb.Append(@" 
                <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                         <link href='bootstrap.min.css' rel='stylesheet' integrity='sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65' crossorigin='anonymous'>
                        <title></title>
                    </head>
                    <body>");

            sb.Append(@"    
                    <div class='container mt-4'>
                            <div class='d-flex flex-row'>
                                <div class='p-2'>
                                    <img src='{0}' style='width: 90%;'  alt='LOGO'>
                                </div>
                                <div class='p-2'>
                                    <div class='d-flex flex-column text-center'>
                                        <span><strong>COMISION ESTATAL DEL AGUA DE BAJA CALIFORNIA</strong></span> 
                                        <span>OFICINA CEA MEXICALI</span> 
                                        <span>FORMATO DE COMISION</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr>"); //Aqui ira el contenido dinamico.

            sb.Append(@"
                    </body>
                    </html>");

            return sb.ToString();

        }
    }
}
