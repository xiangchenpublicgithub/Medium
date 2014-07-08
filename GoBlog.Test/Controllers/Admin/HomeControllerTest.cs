﻿using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Models;
using GoBlog.Persistence;
using GoBlog.Test.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Test.Controllers.Admin
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Mock<IRepository> repository;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            //Mapper.AssertConfigurationIsValid();
            repository = RepositoryMockHelper.MockRepository();
            controller = new HomeController(repository.Object);
        }

        [Test]
        public void IndexReturnsCorrectView()
        {
            // act
            var actual = controller.Index() as ViewResult;

            // assert
            Assert.NotNull(actual);
            Assert.AreEqual("Index", actual.ViewName);
        }

        [Test]
        public void IndexReturnsCorrectModel()
        {
            // act
            var actual = controller.Index() as ViewResult;
            var model = actual.Model as IEnumerable<PostViewModel>;

            // assert
            Assert.NotNull(model);
            Assert.That(model.Count(), Is.EqualTo(4));
        }

        [Test]
        public void DeleteDeletesPost()
        {
            var actual = controller.Delete("dynamic-contagion-part-one") as RedirectToRouteResult;
            Assert.NotNull(actual);
            Assert.That(repository.Object.Posts.Count(), Is.EqualTo(3));
        }

        [Test]
        public void EditReturnsCorrectView()
        {
            var actual = controller.Edit("dynamic-contagion-part-one") as ViewResult;
            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void EditReturnsCorrectModel()
        {
            var actual = controller.Edit("dynamic-contagion-part-one") as ViewResult;
            var model = actual.Model as PostInputModel;
            Assert.NotNull(model);
            Assert.That(model.Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void EditPostReturnsCorrectView()
        {
            var model = new PostInputModel { Slug = "dynamic-contagion-part-one" };

            var actual = controller.Edit(model) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void EditPostEditsPost()
        {
            var model = new PostInputModel { Slug = "dynamic-contagion-part-one", Content = "test" };

            var actual = controller.Edit(model) as ViewResult;

            var post = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            Assert.That(post.Content, Is.EqualTo("test"));
        }

        [Test]
        public void InvalidModelReturnsCorrectViewAndModel()
        {
            controller.ModelState.AddModelError("", "");
            var model = new PostInputModel();
            
            var actual = controller.Edit(model) as ViewResult;
            
            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
            Assert.AreEqual(model, actual.Model);
        }
    }
}