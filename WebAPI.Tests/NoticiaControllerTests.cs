using Microsoft.AspNetCore.Mvc;
using TesTDD.WebAPI.Noticia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPI.Tests.Setup;
using Xunit;

namespace WebAPI.Tests
{
    public class NoticiaControllerTests : TestBase
    {
        public NoticiaControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact(DisplayName = "Dada una Noticia válida cuando solicitamos que se incluya POST retorne StatusCode = Created")]
        public async Task PostWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();
            NoticiaModel model = new NoticiaModel() { Titulo = "Alerta", Contenido = "Virus" };

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/Noticia", model);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact(DisplayName = "Dada una Noticia inválida CUANDO solicitamos que se incluya debe retornar StatusCode = BadRequest")]
        public async Task PostWhenIsNotValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();
            NoticiaModel model = new NoticiaModel() { Titulo = "Alerta" };

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/Noticia", model);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Dada una Noticia válida CUANDO solicitamos una modificacion esta debe retornar StatusCode = NoContent")]
        public async Task PutWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.PutAsJsonAsync("/api/Noticia/0d46be7a-3417-4a06-adff-3e1090bf4ea9", new NoticiaModel() { Titulo = "Internacional", Contenido = "Terremoto" });

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "Dada una Noticia inexistente CUANDO solicitamos una modificacion DEBE retornar StatusCode = NotFound")]
        public async Task PutWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.PutAsJsonAsync($"/api/Noticia/{Guid.NewGuid()}", new NoticiaModel() { Titulo = "Deportes", Contenido = "Mesi" });

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Dado que tenemos al menos una Noticia existente CUANDO solicitamos uma lista DEBE retornar StatusCode = OK lista de NoticiaModel")]
        public async Task GetWhenExists()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync("/api/Noticia");
            var noticias = await response.Content.ReadFromJsonAsync<IEnumerable<NoticiaModel>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(noticias);
        }

        [Fact(DisplayName = "Dada una Noticia existente CUANDO consultamos por Id DEBE retornar StatusCode = OK")]
        public async Task GetByIdWhenExists()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync("/api/Noticia/0d46be7a-3417-4a06-adff-3e1090bf4ea9");
            var noticia = await response.Content.ReadFromJsonAsync<NoticiaModel>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(noticia);
            Assert.Equal(Guid.Parse("0d46be7a-3417-4a06-adff-3e1090bf4ea9"), noticia.Id);
        }

        [Fact(DisplayName = "Dada una Noticia inexistente CUANDO consultamos por Id DEBE retornar StatusCode = NotFound")]
        public async Task GetByIdWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.GetAsync($"/api/Noticia/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Dada una Noticia válida CUANDO solicitamos borrar DEBE retornar StatusCode = NoContent")]
        public async Task DeleteWhenIsValid()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.DeleteAsync("/api/Noticia/d13a07df-085d-4830-a26d-976fa06c1074");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "Dada una Noticia inexistente CUANDO solicitamos eliminar DEBE retornar StatusCode = NotFound")]
        public async Task DeleteWhenNotExist()
        {
            //Arrange
            HttpClient httpClient = CreateHttpClient();

            //Act
            var response = await httpClient.DeleteAsync($"/api/Noticia/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
