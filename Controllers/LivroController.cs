using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec_mvc.Controllers
{
    [Route("[controller]")]
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();

        public IActionResult Index()
        {
             ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

             List<Livro> listaLivros = context.Livro.ToList();
             
             var livrosReservados = context.LivroReserva.ToDictionary(livro => livro.LivroID, livror => livror.DtReserva);
             ViewBag.Livros = listaLivros;
             ViewBag.LivrosComReserva = livrosReservados;


            return View();
            
        }
        [Route("Cadastro")]
        // Metodo que retorna a tela de cadastro
        public IActionResult Cadastro(){
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;
            ViewBag.Categorias = context.Categoria.ToList();
            



            return View();
        }
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form){
            Livro novoLivro = new Livro();

            novoLivro.Nome = form["Nome"].ToString();
            novoLivro.Descricao = form["Descricao"].ToString();
            novoLivro.Editora = form["Editora"].ToString();
            novoLivro.Escritor = form["Escritor"].ToString();
            novoLivro.Idioma = form["Idioma"].ToString();

            context.Livro.Add(novoLivro);

            context.SaveChanges();
        
        }















        
        

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}