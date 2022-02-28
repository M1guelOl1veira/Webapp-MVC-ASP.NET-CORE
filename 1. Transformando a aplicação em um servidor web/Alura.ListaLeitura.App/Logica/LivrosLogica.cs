using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    //Adicionar controller no nome da classe é padrão e necessário para utilizaro roteamente MVC do ASP.NET Core.
    public class LivrosController
    {
        //HttpContext contem toda informação da requisição enviada.
        //Reposta a requisição que passa o Id do livro.
        //O estágio Model Binding do request pipeline faz a adequação necessária de cada action(métodos) e é executado antes da action, no caso encontrar um id na requisição.
        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return livro.Detalhes();
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            //RequestDelegate tem como retorno o tipo Task, estudar paralelismo para mais informações.
            //Código executado quando uma requisição chegar.
            var _repo = new LivroRepositorioCSV();
            var html = UtilidadesHTML.CarregaArquivoHTML("paraLer");

            foreach (var livros in _repo.ParaLer.Livros)
            {
                html = html
                    .Replace("#Novo-Item#", $"<li>{livros.Titulo} - {livros.Autor}</li>#Novo-Item#");
            }
            //_repo.ParaLer.ToString(); Transforma a lista ParaLer em uma string.

            html = html.Replace("#Novo-Item#", "");

            return context.Response.WriteAsync(html);
        }

        public string Lendo()
        {
            var _repo = new LivroRepositorioCSV();
            return _repo.Lendo.ToString();
        }

        public string Lidos()
        {
            var _repo = new LivroRepositorioCSV();
            return _repo.Lidos.ToString();
        }
        public string Teste()
        {
            return "aaa";
        }
    }
}
