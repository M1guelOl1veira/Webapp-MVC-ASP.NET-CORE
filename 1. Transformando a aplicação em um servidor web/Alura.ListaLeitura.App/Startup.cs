using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        //Adição do serviço de roteamento.
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
            //Criando uma rota com template.
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", AdicionarLivro);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes); //int é utilizado para dizer que só atende rotas que utilizam inteiro, caso contrário dará 404(não encontrada).
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);
            //Constrói essa coleção.
            var rotas = builder.Build();
            app.UseRouter(rotas);

            //  app.Run(Roteamento);
        }

        private Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                //Maneira de extrair o valor do path da rota(query) ou do formulário(form).
                //Get envia informações no endereço e Post no cabeçalho.
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First()
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso!");

        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHTML(string nomeDoArquivo)
        {
            var endereco = $"C:/Workspace/Webapp-MVC-ASP.NET-Core/1. Transformando a aplicação em um servidor web/Alura.ListaLeitura.App/HTML/{nomeDoArquivo}.html";
            using (var paginaHTML = File.OpenText(endereco))
            {
                return paginaHTML.ReadToEnd();
            }
        }

        //HttpContext contem toda informação da requisição enviada.
        //Reposta a requisição que passa o Id do livro.+
        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task AdicionarLivro(HttpContext context)
        {
            var livro = new Livro()
            {
                //Maneira de extrair o valor do path da rota.
                Titulo = context.GetRouteValue("nome").ToString(),
                Autor = context.GetRouteValue("autor").ToString()

            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso!");
        }

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
            var html = CarregaArquivoHTML("paraLer");

            foreach (var livros in _repo.ParaLer.Livros)
            {
                html = html
                    .Replace("#Novo-Item#", $"<li>{livros.Titulo} - {livros.Autor}</li>#Novo-Item#");
            }
            //_repo.ParaLer.ToString(); Transforma a lista ParaLer em uma string.

            html = html.Replace("#Novo-Item#", "");

            return context.Response.WriteAsync(html);
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