﻿using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {
        public string Incluir(Livro livro)
        {
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return "O livro foi adicionado com sucesso!";
        }
        //IActionResult é chamado pelo AspNet MVC para processar o resultado de um método action. 
        public IActionResult ExibeFormulario()
        {
            //Como o próprio nome diz, mostra na tela o código html com nome formulario.
            //ViewResult tem um caminho padrão de procura que é Views/{PrefixoDoController}/{nome do arquivo}.
            var html = new ViewResult { ViewName = "formulario" };
            return html;
        }
    }
}
