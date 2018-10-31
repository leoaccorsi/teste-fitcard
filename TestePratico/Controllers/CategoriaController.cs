using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePraticoModel.Model;
using TestePraticoServices.Interface;
using TestePraticoServices.Service;

namespace TestePratico.Controllers
{
    public class CategoriaController : Controller
    {
        private ICategoriaService _categoriaService;

        public CategoriaController() : this(new CategoriaService())
        {
        }

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public ActionResult Index()
        {
            var categorias = _categoriaService.GetAll();
            return View("Index", categorias);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CategoriaModel());
        }

        [HttpPost]
        public ActionResult Create(CategoriaModel categoria)
        {
            if (ModelState.IsValid)
            {
                var cadastrou = _categoriaService.Create(categoria);

                if (!cadastrou)
                {
                    return View("Create", categoria);
                }

                return RedirectToAction("Index");
            }

            return View("Create", categoria);
        }
    }
}