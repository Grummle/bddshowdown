using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        [TestMethod]
        public void should_edit_existing_item()
        {
            var itemText = _fixture.Create<string>();
            var newText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);

            _page.DoubleClickItem(item).SelectAll().SendKeys(newText + Keys.Enter);
            
            item.Text.ShouldEqual(newText);
        }

        [TestMethod]
        public void should_delete_existing_item()
        {
            var itemText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);
            item.Text.ShouldEqual(itemText);

            _page.HoverOver(item);
            item.DeleteItem();

            _page.Items.Any(x=>x.Text == itemText).ShouldBeFalse();
        }

        [TestMethod]
        public void should_mark_existing_complete()
        {
            var itemText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);
            item.Text.ShouldEqual(itemText);

            item.CompleteItem().IsCompleted().ShouldBeTrue();
        }

        [TestMethod]
        public void should_add_completed_class()
        {
            var itemText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);
            item.Text.ShouldEqual(itemText);

            item.CompleteItem().GetAttribute("class").Contains("completed").ShouldBeTrue();
        }

        [TestMethod]
        public void should_hide_item_when_completed_and_active_displayed()
        {
            var itemText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);
            item.Text.ShouldEqual(itemText);

            item.CompleteItem();

            _page.SetActive();
            Thread.Sleep(2000);

            _page.Items.Any(x=>x.Text==itemText).ShouldBeFalse();
        }

        [TestMethod]
        public void should_show_item_when_completed_and_completed_displayed()
        {
            var itemText = _fixture.Create<string>();

            var item = _page.NavigateTo().AddNewItem(itemText);
            item.Text.ShouldEqual(itemText);

            item.CompleteItem();

            _page.SetCompleted();

            _page.Items.Any(x=>x.Text==itemText).ShouldBeTrue();
        }

        [TestMethod]
        public void should_display_uncompleted_item_count()
        {
            _page.NavigateTo().AddNewItem(_fixture.Create<string>());
            _page.NavigateTo().AddNewItem(_fixture.Create<string>());
            _page.NavigateTo().AddNewItem(_fixture.Create<string>());
            _page.NavigateTo().AddNewItem(_fixture.Create<string>());
            _page.NavigateTo().AddNewItem(_fixture.Create<string>());
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();


            _page.ItemsLeft.ShouldEqual(5);
        }

        [TestMethod]
        public void should_clear_completed_items()
        {
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();
            _page.NavigateTo().AddNewItem(_fixture.Create<string>()).CompleteItem();

            _page.SetCompleted();
            _page.ClearCompleted();

            _page.Items.Count.ShouldEqual(0);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _page?.Dispose();
        }
    }
}
