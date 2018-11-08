using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoServices.Helpers;
using TestePraticoServices.Interface;
using TestePraticoServices.Service;

namespace TestePratico.Controllers
{
    public class CategoriaController : BaseController
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
                var retorno = _categoriaService.Create(categoria);

                if (retorno != ERetornoEstabelecimento.SucessoCadastro)
                {
                    Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.error);

                    return View("Create", categoria);
                }

                Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.success);
                return RedirectToAction("Index");
            }

            return View("Create", categoria);
        }
    }
}