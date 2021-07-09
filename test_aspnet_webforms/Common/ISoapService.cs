using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using test_aspnet_webforms.Common;

namespace test_aspnet_webforms.Common
{
    public interface ISoapService
    {
        Task<List<TodoItem>> RefreshDataAsync();

        Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

        Task DeleteTodoItemAsync(string id);
    }
}