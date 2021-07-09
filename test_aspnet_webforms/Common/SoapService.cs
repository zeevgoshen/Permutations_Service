using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Threading.Tasks;

namespace test_aspnet_webforms.Common
{
    public class SoapService : ISoapService
    {
        //ASMXService.TodoService todoService;
        similar wordMatchService;

        public List<TodoItem> Items { get; private set; } = new List<TodoItem>();

        public SoapService()
        {
            wordMatchService = new similar();
            //wordMatchService.
            //todoService = new ASMXService.TodoService();
            //todoService.Url = Constants.SoapUrl;
            //...
        }

        public Task<List<TodoItem>> RefreshDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveTodoItemAsync(TodoItem item, bool isNewItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTodoItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
