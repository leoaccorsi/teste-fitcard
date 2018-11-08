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
        //Estabelecimento
        Mock<IEstabelecimentoRepository> repositoryMock;
        EstabelecimentoService service;
        EstabelecimentoController controller;

        //Categoria
        Mock<ICategoriaRepository> repositoryMockCategoria;
        CategoriaService serviceCategoria;

        [TestInitialize]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            //Setando categoria
            repositoryMockCategoria = new Mock<ICategoriaRepository>();
            serviceCategoria = new CategoriaService(repositoryMockCategoria.Object);

            var categorias = new List<CategoriaModel>();
            categorias.Add(new CategoriaModel() { nome = "Supermercado" });
            categorias.Add(new CategoriaModel() { nome = "Restaurante" });
            categorias.Add(new CategoriaModel() { nome = "Borracharia" });
            categorias.Add(new CategoriaModel() { nome = "Posto" });
            categorias.Add(new CategoriaModel() { nome = "Oficina" });

            repositoryMockCategoria.Setup(x => x.GetAll()).Returns(categorias);

            //Setando estabelecimento
            repositoryMock = new Mock<IEstabelecimentoRepository>();
            service = new EstabelecimentoService(repositoryMock.Object, serviceCategoria);
            controller = new EstabelecimentoController(service, serviceCategoria);            
        }

        [TestMethod]
        public void Testar_Listagem_Estabelecimentos_E_Retornar_Tela_Index()
        {
            // arrange
            var estabelecimentos = new List<EstabGridViewModel>();
            estabelecimentos.Add(new EstabGridViewModel() { razao_social = "Estabelecimento 1", cnpj = "11.111.111/1111-11" });
            estabelecimentos.Add(new EstabGridViewModel() { razao_social = "Estabelecimento 2", cnpj = "22.222.222/2222-22" });
            estabelecimentos.Add(new EstabGridViewModel() { razao_social = "Estabelecimento 3", cnpj = "33.333.333/3333-33" });

            repositoryMock.Setup(x => x.GetAll()).Returns(estabelecimentos);

            // act
            var result = controller.Index() as ViewResult;

            // assert
            var model = result.ViewData.Model as List<EstabGridViewModel>;
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
                cnpj = "94.335.598/0001-13"
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
                cnpj = "94.335.598/0001-13"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            var estabelecimentos = new List<EstabelecimentoModel>();
            estabelecimentos.Add(new EstabelecimentoModel() { razao_social = "Estabelecimento 1", cnpj = "94.335.598/0001-13" });
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
                cnpj = "94.335.598/0001-13"
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

        [TestMethod]
        public void Testar_Nao_Cadastrar_Estabelecimento_CNPJ_Invalido()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "11.222.333/4444-55"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Estabelecimento_Agencia_Invalida()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "94.335.598/0001-13",
                agencia = "1111",
                conta = "11.111-1"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Estabelecimento_Conta_Invalida()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "94.335.598/0001-13",
                agencia = "111-1",
                conta = "11.1111"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Estabelecimento_Email_Invalido()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "94.335.598/0001-13",
                email = "testeemail"
            };

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Estabelecimento_Categoria_Supermercado_Sem_Informar_Telefone()
        {
            // arrange
            var estabelecimentoVM = new EstabelecimentoViewModel()
            {
                razao_social = "Estabelecimento 1",
                cnpj = "94.335.598/0001-13",
                cod_categoria = 1,
                telefone = ""
            };

            var categoria = new CategoriaModel() { id = 1, nome = "Supermercado" };
            repositoryMockCategoria.Setup(x => x.GetSingle(categoria.id)).Returns(categoria);

            var estabelecimento = Mapper.Map<EstabelecimentoViewModel, EstabelecimentoModel>(estabelecimentoVM);

            // act
            var result = controller.Create(estabelecimentoVM) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(estabelecimento), Times.Never());

            var model = result.ViewData.Model as EstabelecimentoViewModel;
            Assert.AreEqual(estabelecimentoVM, model);

            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
