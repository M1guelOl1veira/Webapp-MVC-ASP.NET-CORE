using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    class CadastroLogica
    {
        public static Task ProcessaFormulario(HttpContext context)
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
        public static Task ExibeFormulario(HttpContext context)
        {
            var html = UtilidadesHTML.CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        public static Task AdicionarLivro(HttpContext context)
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
    }
}
