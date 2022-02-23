using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        public void Configure(IApplicationBuilder app)
        {
            //IApplicationBuilder Constrói o pipeline(fluxo de requisição resposta) de requisição para a minha aplicação.
            //Criar uma coleção de rotas com o objeto do tipo RouterBuilder, que recebe a aplicação como parâmetro.
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            var rotas = builder.Build();
            app.UseRouter(rotas);

            //  app.Run(Roteamento);
        }
        //HttpContext contem toda informação da requisição enviada.
        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new Dictionary<string, RequestDelegate>//Tipo da chave e tipo do valor respectivamente. RequestDelegate é um tipo que permite passar um método como argumento.
            {
                { "/Livros/ParaLer", LivrosParaLer },
                { "/Livros/Lendo", LivrosLendo },
                { "/Livros/Lidos", LivrosLidos }
            };

            //context.Request.Path É o caminho da requisição.
            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho não encontrado!");
 
        }
        public Task LivrosParaLer(HttpContext context)
        {
            //RequestDelegate tem como retorno o tipo Task, estudar paralelismo para mais informações.
            //Código executado quando uma requisição chegar.
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.ParaLer.ToString());
            //_repo.ParaLer.ToString(); Transforma a lista ParaLer em uma string.
        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }
    }
}