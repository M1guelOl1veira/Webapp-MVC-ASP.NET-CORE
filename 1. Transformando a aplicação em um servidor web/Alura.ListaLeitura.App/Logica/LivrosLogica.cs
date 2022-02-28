using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    //Adicionar controller no nome da classe é padrão e necessário para utilizaro roteamente MVC do ASP.NET Core.
    public class LivrosController : Controller
    {
        public IEnumerable<Livro> Livros { get; set; }

        //HttpContext contem toda informação da requisição enviada.
        //Reposta a requisição que passa o Id do livro.
        //O estágio Model Binding do request pipeline faz a adequação necessária de cada action(métodos) e é executado antes da action, no caso encontrar um id na requisição.
        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return livro.Detalhes();
        }

        public IActionResult ParaLer()
        {
            //RequestDelegate tem como retorno o tipo Task, estudar paralelismo para mais informações.
            //Código executado quando uma requisição chegar.
            //Quando a classe herda a classe controller ela é capaz de chamar o objeto na View, pq Controller é uma classe padrão do framework.
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.ParaLer.Livros;
            return View("lista");
        }

        public IActionResult Lendo()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lendo.Livros;
            return View("lista");
        }
 
        public IActionResult Lidos()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lidos.Livros;
            return View("lista");
        }
    }
}
