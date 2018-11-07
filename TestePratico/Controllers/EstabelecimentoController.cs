using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Mappers;
using TestePraticoModel.Enum;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;
using TestePraticoServices;
using TestePraticoServices.Helpers;
using TestePraticoServices.Interface;
using TestePraticoServices.Service;

namespace TestePratico.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private IEstabelecimentoService _estabelecimentoService;

        public EstabelecimentoController() : this(new EstabelecimentoService())
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }

        public EstabelecimentoController(IEstabelecimentoService estabelecimentoService)
        {
            _estabelecimentoService = estabelecimentoService;
        }
        
        public ActionResult Index()
        {
            var estabelecimentos = _estabelecimentoService.GetAll();
            return View("Index", estabelecimentos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new EstabelecimentoViewModel());
        }

        [HttpPost]
        public ActionResult Create(EstabelecimentoViewModel estabelecimentoVM)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);
                var retorno = _estabelecimentoService.Create(model);

                if (retorno != ERetornoEstabelecimento.Cadastrado)
                {
                    //Popular ViewBag com o erro
                    ViewBag.erro = RetornoHelper.RetornoEstabelecimento(retorno);

                    return View("Create", estabelecimentoVM);
                }

                return RedirectToAction("Index");
            }

            return View("Create", estabelecimentoVM);
        }
    }
}