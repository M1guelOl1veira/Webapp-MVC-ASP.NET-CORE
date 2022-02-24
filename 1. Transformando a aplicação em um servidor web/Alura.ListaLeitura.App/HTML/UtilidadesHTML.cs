using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alura.ListaLeitura.App.HTML
{
    class UtilidadesHTML
    {
        public static string CarregaArquivoHTML(string nomeDoArquivo)
        {
            var endereco = $"C:/Workspace/Webapp-MVC-ASP.NET-Core/1. Transformando a aplicação em um servidor web/Alura.ListaLeitura.App/HTML/{nomeDoArquivo}.html";
            using (var paginaHTML = File.OpenText(endereco))
            {
                return paginaHTML.ReadToEnd();
            }
        }
    }
}
