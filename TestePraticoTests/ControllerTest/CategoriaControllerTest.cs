using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestePraticoModel.Model;
using System.Collections.Generic;
using System.Linq;
using TestePratico.Controllers;
using TestePraticoRepository.Interface;
using TestePraticoServices.Service;
using System.Web.Mvc;

namespace TestePraticoTests.ControllerTest
{
    [TestClass]
    public class CategoriaControllerTest
    {

        Mock<ICategoriaRepository> repositoryMock;
        CategoriaService service;
        CategoriaController controller;

        [TestInitialize]
        public void SetUp()
        {
            repositoryMock = new Mock<ICategoriaRepository>();
            service = new CategoriaService(repositoryMock.Object);
            controller = new CategoriaController(service);
        }

        [TestMethod]
        public void Testar_Listagem_Categorias_E_Retornar_Tela_Index()
        {
            // arrange
            var categorias = new List<CategoriaModel>();
            categorias.Add(new CategoriaModel() { nome = "Supermercado" });
            categorias.Add(new CategoriaModel() { nome = "Restaurante" });
            categorias.Add(new CategoriaModel() { nome = "Borracharia" });
            categorias.Add(new CategoriaModel() { nome = "Posto" });
            categorias.Add(new CategoriaModel() { nome = "Oficina" });
            
            repositoryMock.Setup(x => x.GetAll()).Returns(categorias);
            
            // act
            var result = controller.Index() as ViewResult;

            // assert
            var model = result.ViewData.Model as List<CategoriaModel>;
            Assert.AreEqual(5, model.Count);

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Testar_Cadastro_Categoria()
        {
            // arrange
            var categoria = new CategoriaModel()
            {
                nome = "Supermercado"
            };
            
            // act
            var result = controller.Create(categoria) as RedirectToRouteResult;

            // assert
            repositoryMock.Verify(x => x.Create(categoria), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Model_Invalida()
        {
            // arrange
            var categoria = new CategoriaModel();

            controller.ModelState.AddModelError("key", "error");

            // act
            var result = controller.Create(categoria) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(categoria), Times.Never());

            var model = result.ViewData.Model as CategoriaModel;
            Assert.AreEqual(categoria, model);

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Nao_Cadastrar_Categoria_Repetida()
        {
            // arrange
            var categoria = new CategoriaModel()
            {
                nome = "Supermercado"
            }; 

            var categorias = new List<CategoriaModel>();
            categorias.Add(new CategoriaModel() { nome = "Supermercado" });
            categorias.Add(new CategoriaModel() { nome = "Restaurante" });
            categorias.Add(new CategoriaModel() { nome = "Borracharia" });
            categorias.Add(new CategoriaModel() { nome = "Posto" });
            categorias.Add(new CategoriaModel() { nome = "Oficina" });

            repositoryMock.Setup(x => x.FindByName(categoria.nome)).Returns(categorias.Where(x => x.nome == categoria.nome).FirstOrDefault());

            // act
            var result = controller.Create(categoria) as ViewResult;

            // assert
            repositoryMock.Verify(x => x.Create(categoria), Times.Never());

            var model = result.ViewData.Model as CategoriaModel;
            Assert.AreEqual(categoria, model);
            
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Testar_Cadastrar_Categoria_Nao_Repetida()
        {
            // arrange
            var categoria = new CategoriaModel()
            {
                nome = "Supermercado"
            };

            var categorias = new List<CategoriaModel>();
            categorias.Add(new CategoriaModel() { nome = "Restaurante" });
            categorias.Add(new CategoriaModel() { nome = "Borracharia" });
            categorias.Add(new CategoriaModel() { nome = "Posto" });
            categorias.Add(new CategoriaModel() { nome = "Oficina" });

            repositoryMock.Setup(x => x.FindByName(categoria.nome)).Returns(categorias.Where(x => x.nome == categoria.nome).FirstOrDefault());

            var result = controller.Create(categoria) as RedirectToRouteResult;

            // assert
            repositoryMock.Verify(x => x.Create(categoria), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}