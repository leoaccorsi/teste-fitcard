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

            var repositoryMock = new Mock<ICategoriaRepository>();
            repositoryMock.Setup(x => x.GetAll()).Returns(categorias);

            var service = new CategoriaService(repositoryMock.Object);
            var controller = new CategoriaController(service);

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
            
            var repositoryMock = new Mock<ICategoriaRepository>();

            var service = new CategoriaService(repositoryMock.Object);
            var controller = new CategoriaController(service);

            // act
            var result = controller.Create(categoria) as RedirectToRouteResult;

            // assert
            repositoryMock.Verify(x => x.Create(categoria), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}