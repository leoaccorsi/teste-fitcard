using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestePraticoModel.Model;
using Moq;
using TestePraticoServices.Service;
using TestePratico.Controllers;
using TestePraticoRepository.Interface;
using System.Web.Mvc;
using System.Linq;
using TestePraticoModel.ViewModel;
using AutoMapper;
using TestePratico.Mappers;

namespace TestePraticoTests.ControllerTest
{
    [TestClass]
    public class EstabelecimentoControllerTest
    {
        Mock<IEstabelecimentoRepository> repositoryMock;
        EstabelecimentoService service;
        EstabelecimentoController controller;

        [TestInitialize]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            repositoryMock = new Mock<IEstabelecimentoRepository>();
            service = new EstabelecimentoService(repositoryMock.Object);
            controller = new EstabelecimentoController(service);
        }

        [TestMethod]
        public void Testar_Listagem_Estabelecimentos_E_Retornar_Tela_Index()
        {
            // arrange
            var estabelecimentos = new List<EstabelecimentoModel>();
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 1", cnpj = "11.111.111/1111-11" });
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 2", cnpj = "22.222.222/2222-22" });
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 3", cnpj = "33.333.333/3333-33" });

            repositoryMock.Setup(x => x.GetAll()).Returns(estabelecimentos);

            // act
            var result = controller.Index() as ViewResult;

            // assert
            var model = result.ViewData.Model as List<EstabelecimentoModel>;
            Assert.AreEqual(3, model.Count);

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Testar_Cadastro_Estabelecimento()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "11.111.111/1111-11"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as RedirectToRouteResult;

            // assert
            repositoryMock.Verify(x => x.Create(It.IsAny<EstabelecimentoModel>()), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Model_Invalida()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel();

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            controller.ModelState.AddModelError("key", "error");

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_CNPJ_Repetido()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "11.111.111/1111-11"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            var estabelecimentos = new List<EstabelecimentoModel>();
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 1", cnpj = "11.111.111/1111-11" });
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 2", cnpj = "22.222.222/2222-22" });
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 3", cnpj = "33.333.333/3333-33" });

            repositoryMock.Setup(x => x.FindByCnpj(estabelecimento.cnpj)).Returns(estabelecimentos.Where(x => x.cnpj == estabelecimento.cnpj).FirstOrDefault());

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Cadastrar_CNPJ_Nao_Repetido()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "11.111.111/1111-11"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            var estabelecimentos = new List<EstabelecimentoModel>();
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 2", cnpj = "22.222.222/2222-22" });
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 3", cnpj = "33.333.333/3333-33" });

            repositoryMock.Setup(x => x.FindByCnpj(estabelecimento.cnpj)).Returns(estabelecimentos.Where(x => x.cnpj == estabelecimento.cnpj).FirstOrDefault());

            // act
            var result = controller.Create(estabelecimentoVM) as RedirectToRouteResult;

            // assert
            repositoryMock.Verify(x => x.Create(It.IsAny<EstabelecimentoModel>()), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
