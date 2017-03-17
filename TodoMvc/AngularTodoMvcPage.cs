using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Protractor;

namespace TodoMvc
{
    public class AngularTodoMvcPage : IDisposable
    {
        private readonly NgWebDriver _driver;
        public AngularTodoMvcPage(NgWebDriver driver)
        {
            _driver = driver;
            _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));
        }

        public AngularTodoMvcPage NavigateTo()
        {
            _driver.Url = "http://todomvc.com/examples/angularjs/#/";

            return this;
        }

        public IWebElement AddNewItem(string itemText)
        {
            _driver.FindElement(NgBy.Model("newTodo")).SendKeys(itemText+Keys.Enter);
            return _driver.FindElements(NgBy.Repeater("todo in todos")).FirstOrDefault(x=>x.Text==itemText);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public IWebElement DoubleClickItem(IWebElement item)
        {
            new Actions(_driver).DoubleClick(item).Perform();
            return _driver.SwitchTo().ActiveElement();
        }

        public List<IWebElement> Items
        {
            get { return _driver.FindElements(NgBy.Repeater("todo in todos")).Cast<IWebElement>().ToList(); }
        }

        public IWebElement HoverOver(IWebElement element)
        {
            new Actions(_driver).MoveToElement(element).Perform();
            return element;
        }

        public AngularTodoMvcPage SetAll()
        {
            _driver.FindElement(By.Id("filters")).FindElement(By.LinkText("All")).Click();
            return this;
        }

        public AngularTodoMvcPage SetActive()
        {
            _driver.FindElement(By.Id("filters")).FindElement(By.LinkText("Active")).Click();
            return this;
        }

        public AngularTodoMvcPage SetCompleted()
        {
            _driver.FindElement(By.Id("filters")).FindElement(By.LinkText("Completed")).Click();
            return this;
        }

        public int ItemsLeft
        {
            get
            {
                return  int.Parse(new Regex(@"\d+").Match(_driver.FindElement(By.Id("todo-count")).Text).Value);
            }
        }

        public void ClearCompleted()
        {
            _driver.FindElement(By.Id("clear-completed")).Click();
        }
    }

    public static class IWebelementExtensions
    {
        public static IWebElement SelectAll(this IWebElement element)
        {
            element.SendKeys(Keys.Control+"a");
            return element;
        }

        public static void DeleteItem(this IWebElement item)
        {
            item.FindElement(By.ClassName("destroy")).Click();
        }

        public static IWebElement CompleteItem(this IWebElement item)
        {
            item.FindElement(NgBy.Model("todo.completed")).Click();
            return item;
        }

        public static bool IsCompleted(this IWebElement item)
        {
            return (bool)((NgWebElement) item).Evaluate("todo.completed");
        }
    }
}
