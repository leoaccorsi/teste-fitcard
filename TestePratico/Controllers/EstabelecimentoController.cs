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
    public class EstabelecimentoController : BaseController
    {
        private IEstabelecimentoService _estabelecimentoService;
        private ICategoriaService _categoriaService;

        public EstabelecimentoController() : this(new EstabelecimentoService(), new CategoriaService())
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }

        public EstabelecimentoController(IEstabelecimentoService estabelecimentoService, ICategoriaService categoriaService)
        {
            _estabelecimentoService = estabelecimentoService;
            _categoriaService = categoriaService;
        }

        public ActionResult Index()
        {
            var estabelecimentos = _estabelecimentoService.GetAll();
            return View("Index", estabelecimentos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EstabelecimentoViewModel();
            LoadModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EstabelecimentoViewModel estabelecimentoVM)
        {
            LoadModel(estabelecimentoVM);

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);
                var retorno = _estabelecimentoService.Create(model);

                if (retorno != ERetornoEstabelecimento.SucessoCadastro)
                {
                    Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.error);

                    return View("Create", estabelecimentoVM);
                }

                Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.success);
                return RedirectToAction("Index");
            }

            return View("Create", estabelecimentoVM);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = Mapper.Map<EstabelecimentoModel, EstabelecimentoViewModel>(_estabelecimentoService.GetSingle(id));
            LoadModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EstabelecimentoViewModel estabelecimentoVM)
        {
            LoadModel(estabelecimentoVM);

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);
                var retorno = _estabelecimentoService.Edit(model);

                if (retorno != ERetornoEstabelecimento.SucessoEdicao)
                {
                    Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.error);

                    return View("Edit", estabelecimentoVM);
                }

                Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.success);
                return RedirectToAction("Index");
            }

            return View("Edit", estabelecimentoVM);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            var retorno = _estabelecimentoService.Delete(id);

            Alert(RetornoHelper.RetornoEstabelecimento(retorno), NotificationType.success);

            return RedirectToAction("Index");
        }

        private void LoadModel(EstabelecimentoViewModel model)
        {
            model.Categorias = new SelectList(_categoriaService.GetAll(), "id", "nome"); ;
        }
    }
}