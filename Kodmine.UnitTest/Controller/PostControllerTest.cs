using NUnit.Framework;
using Kodmine.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Kodmine.Core.Interfaces;
using Moq;
using Kodmine.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.UnitTest.Controllers
{
    [TestFixture]
    public class PostControllerTest
    {
        Mock<IConfiguration> configuration;
        PostController controller;
        Mock<IPostRepository> postRepository;
        Mock<ITagRepository> tagRepository;
        Mock<IPostTagRepository> postTagRepository;
        Mock<IRubricRepository> rubricRepository;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            //TestContext.CurrentContext.TestDirectory

            postRepository = new Mock<IPostRepository>();
            tagRepository = new Mock<ITagRepository>();
            postTagRepository = new Mock<IPostTagRepository>();
            rubricRepository = new Mock<IRubricRepository>();
            configuration = new Mock<IConfiguration>();
            controller = new PostController(Helpers.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory),
                                                postRepository.Object, 
                                                tagRepository.Object, 
                                                postTagRepository.Object, 
                                                rubricRepository.Object);
        }

        [Test]
        public void CleanContent_ReturnType_JsonResult()
        {
            string text = "";

            var text_cleaned = controller.CleanContent(text);

            Assert.IsInstanceOf<JsonResult>((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned);
        }

        [Test]
        public void CleanContent_RemoveUnnecessaryText_RemoveDiv()
        {
            string text = "text1 <div class=\"abc\">text2</div> text3";

            var text_cleaned = controller.CleanContent(text);

            Assert.AreEqual("text1 text2 text3", ((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned).Value);
        }

        [Test]
        public void CleanContent_RemoveUnnecessaryText_RemoveSpan()
        {
            string text = "text1 <span class=\"abc\">text2</span> text3";

            var text_cleaned = controller.CleanContent(text);

            Assert.AreEqual("text1 text2 text3", ((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned).Value);
        }

        [Test]
        public void CleanContent_RemoveUnnecessaryText_RemoveCode()
        {
            string text = "text1 <code class=\"abc\">text2</code> text3";

            var text_cleaned = controller.CleanContent(text);

            Assert.AreEqual("text1 text2 text3", ((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned).Value);
        }

        [Test]
        public void CleanContent_RemoveUnnecessaryText_RemoveTagStyle()
        {
            string text = "text1 <p style=\"color:green;font-size:14px\">text2</p> text3";
            
            var text_cleaned = controller.CleanContent(text);

            Assert.AreEqual("text1 <p>text2</p> text3", ((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned).Value);
        }

        [Test]
        public void CleanContent_RemoveUnnecessaryText_RemoveTagId()
        {
            string text = "text1 <h2 id=\"mcetoc_1ch0bj12b0\">text2</h2> text3";

            var text_cleaned = controller.CleanContent(text);

            Assert.AreEqual("text1 <h2>text2</h2> text3", ((Microsoft.AspNetCore.Mvc.JsonResult)text_cleaned).Value);
        }

    }
}