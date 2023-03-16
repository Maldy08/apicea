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
                            <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD"" crossorigin=""anonymous"">
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
                                        <span><strong>COMISION ESTATAL DEL AGUA DE BAJA CALIFORNIA</strong></span> <br>
                                        <span>OFICINA CEA MEXICALI</span> 
                                        <span>FORMATO DE COMISION</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr>"); //Aqui ira el contenido dinamico.

            sb.Append(@"
                    <div className='container mt-4'>
                <div className='row'>
                    <div className='col-md-6 border'>
                        <div className='p-2'><span className='gris'>BUENO POR: </span><span><b>{`$${ formatoComision.importe.toFixed(2)}`}</b></span></div>
                    </div>

                    <div className='col border'>
                        <div className='p-2'> <span className='gris'>NO. DE OFICIO: </span> <span><b>{`V${formatoComision.oficina}-${ formatoComision.noViat }/${ formatoComision.ejercicio }`}</b></span></div>
                        <div className='p-2'><span className='gris'>FECHA: </span><span>{ `${fechaViatico.getDate().toString()} DE ${ getMes( fechaViatico )} DE ${ formatoComision.ejercicio }` }</span></div>
                    </div>
                    
                </div>
            </div>
                    </body>
                    </html>");

            return sb.ToString();

        }
    }
}
