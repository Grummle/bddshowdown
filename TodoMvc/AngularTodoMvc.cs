using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Protractor;

namespace TodoMvc
{
    public class AngularTodoMvc : IDisposable
    {
        private readonly NgWebDriver _driver;
        public AngularTodoMvc(NgWebDriver driver)
        {
            _driver = driver;
            _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));
        }

        public AngularTodoMvc NavigateTo()
        {
            _driver.Url = "http://todomvc.com/examples/angularjs/#/";

            return this;
        }

        public NgWebElement AddNewItem(string itemText)
        {
            _driver.FindElement(NgBy.Model("newTodo")).SendKeys(itemText+Keys.Enter);
            return _driver.FindElements(NgBy.Repeater("todo in todos")).FirstOrDefault(x=>x.Text==itemText);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
