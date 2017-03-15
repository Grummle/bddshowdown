using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using Ploeh.AutoFixture;
using Protractor;
using Should;
using TodoMvc;

namespace Test
{
    [TestClass]
    public class AngularTodoMvcTests
    {
        private AngularTodoMvc _page;
        private Fixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _fixture=new Fixture();
            _page= new AngularTodoMvc(new NgWebDriver(new ChromeDriver()));
        }

        [TestMethod]
        public void should_add_new_item()
        {
            var itemText = _fixture.Create<string>();

            _page.NavigateTo().AddNewItem(itemText).Text.ShouldEqual(itemText);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _page?.Dispose();
        }
    }
}
