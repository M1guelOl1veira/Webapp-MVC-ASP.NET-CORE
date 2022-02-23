using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var _repo = new LivroRepositorioCSV();

            //criar objeto para hospedar os pedidos web.
            IWebHost host = new WebHostBuilder() //WebHostBuilder constrói um hospedeiro web, mas não implementa a interface.
                .UseKestrel() //Cria um web host que usa a implentação do Kestrel.
                .UseStartup<Startup>() //Dizendo que o código de configuração ficará na classe Startup.
                .Build(); // O Build implementa essa interface.
                
            host.Run();

            //ImprimeLista(_repo.ParaLer); 
            //ImprimeLista(_repo.Lendo);
            //ImprimeLista(_repo.Lidos);
        }
          
        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }
}
