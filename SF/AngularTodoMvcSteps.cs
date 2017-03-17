using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;
using Should;
using TechTalk.SpecFlow;
using TodoMvc;

namespace SF
{
    [Binding]
    public class AngularTodoMvcSteps
    {
        private AngularTodoMvcContext _context;
        public AngularTodoMvcSteps(AngularTodoMvcContext context)
        {
            _context = context;
        }

        [Given(@"I have navigated to Angular TodoMvc")]
        public void GivenIHaveNavigatedToAngularTodoMvc()
        {
            _context.Page.NavigateTo();
        }
        
        [Given(@"I have created an Item with text ""(.*)""")]
        [When(@"I have created an Item with text ""(.*)""")]
        public void WhenIHaveCreatedAnItemWithText(string p0)
        {
            _context.CurrentItem = _context.Page.AddNewItem(p0);
        }
        
        [Then(@"the item list should contain an item with ""(.*)""")]
        public void ThenTheItemListShouldContainAnItemWith(string p0)
        {
            _context.Page.Items.Any(x => x.Text == p0).ShouldBeTrue();
        }

        [When(@"I mark the item as completed")]
        [Given(@"I mark an item as completed")]
        [Given(@"I mark the item as completed")]
        public void WhenIMarkTheItemAsCompleted()
        {
            _context.CurrentItem.CompleteItem();
        }

        [Then(@"the item should have the ""(.*)"" class")]
        public void ThenTheItemShouldHaveTheClass(string p0)
        {
            _context.CurrentItem.GetAttribute("class").Contains("completed").ShouldBeTrue();
        }

        [Given(@"I have clicked ""All""")]
        public void GivenIHaveClicked()
        {
            Thread.Sleep(2000);
            _context.Page.SetAll();
        }

        [Given(@"I have marked the item ""(.*)"" completed")]
        [When(@"I have marked the item ""(.*)"" completed")]
        public void GivenIHaveMarkedTheItemCompleted(string p0)
        {
            _context.Page.Items.FirstOrDefault(x => x.Text == p0).CompleteItem();
            Thread.Sleep(1000);
        }

        [When(@"I click ""Clear Completed""")]
        public void WhenIClick()
        {
            _context.Page.ClearCompleted();
        }

        [Then(@"item ""(.*)"" should be gone")]
        public void ThenItemShouldBeGone(string p0)
        {
            _context.Page.Items.Any(x=>x.Text==p0).ShouldBeFalse();
        }

        [Then(@"item ""(.*)"" should exist")]
        public void ThenItemShouldExist(string p0)
        {
            _context.Page.Items.Any(x=>x.Text==p0).ShouldBeTrue();
        }

        [When(@"I delete the item with text ""(.*)""")]
        public void WhenIDeleteTheItemWithText(string p0)
        {
            _context.Page.Items.FirstOrDefault(x=>x.Text==p0).DeleteItem();
        }

        [When(@"I hover over item ""(.*)""")]
        public void WhenIHoverOverItem(string p0)
        {
            _context.Page.HoverOver(_context.CurrentItem);
        }

        [Given(@"I have added (.*) new Items")]
        public void GivenIHaveAddedNewItems(int p0)
        {
            for (var i = 0; i < p0; i++)
            {
                _context.CurrentItem = _context.Page.AddNewItem(Guid.NewGuid().ToString());
            }
        }

        [Then(@"the item count should be (.*)")]
        public void ThenTheItemCountShouldBe(int p0)
        {
            _context.Page.ItemsLeft.ShouldEqual(p0);
        }

        [When(@"I double click the item")]
        public void WhenIDoubleClickTheItem()
        {
            _context.CurrentItem = _context.Page.DoubleClickItem(_context.CurrentItem);
        }

        [When(@"insert new text ""(.*)""")]
        public void WhenInsertNewText(string p0)
        {
            _context.CurrentItem.SelectAll().SendKeys(p0+Keys.Enter);
        }

        [Given(@"I have clicked ""Active""")]
        public void GivenIHaveClickedActive()
        {
            Thread.Sleep(2000);
            _context.Page.SetActive();
        }

        [Given(@"I have clicked ""Completed""")]
        public void GivenIHaveClicked(string p0)
        {
            _context.Page.SetCompleted();
        }
    }

    public class AngularTodoMvcContext : IDisposable 
    {
        public AngularTodoMvcPage Page { get; set; }

        public AngularTodoMvcContext()
        {
            Page = new AngularTodoMvcPage(new NgWebDriver(new ChromeDriver())); 
        }

        public IWebElement CurrentItem { get; set; }

        public void Dispose()
        {
            Page?.Dispose();
        }
    }
}
