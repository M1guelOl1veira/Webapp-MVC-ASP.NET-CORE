using Alura.ListaLeitura.App.Logica;
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
            builder.MapRoute("Livros/ParaLer", LivrosLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.LivrosLidos);
            //Criando uma rota com template.
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.AdicionarLivro);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes); //int é utilizado para dizer que só atende rotas que utilizam inteiro, caso contrário dará 404(não encontrada).
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);
            //Constrói essa coleção.
            var rotas = builder.Build();
            app.UseRouter(rotas);

            //  app.Run(Roteamento);
        }

        

        

        
    }
}