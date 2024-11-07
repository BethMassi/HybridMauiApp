using System.Text.Json.Serialization;

namespace HybridMauiApp
{
    public partial class ReactPage : ContentPage
    {
        private readonly TodoDataStore _todoDataStore;

        public ReactPage()
        {
            InitializeComponent();

            _todoDataStore = new TodoDataStore();
            _todoDataStore.TaskDataChanged += OnTodoDataChanged;

            myHybridWebView.SetInvokeJavaScriptTarget<TodoJSInvokeTarget>(new TodoJSInvokeTarget(this, _todoDataStore));

            BindingContext = this;
        }

        public string TodoAppTitle => $"Todo items: {_todoDataStore.GetData().Where(t => t.is_completed).Count()} done / {_todoDataStore.GetData().Count} total";

        private void OnTodoDataChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TodoAppTitle));
        }

        private async void SendUpdatedTasksToJS(IList<TodoTask> tasks)
        {
            _ = await MainThread.InvokeOnMainThreadAsync(async () =>
                _ = await myHybridWebView.InvokeJavaScriptAsync<string>(
                    methodName: "globalSetData",
                    returnTypeJsonTypeInfo: TodoAppJsContext.Default.String,
                    paramValues: [tasks],
                    paramJsonTypeInfos: [TodoAppJsContext.Default.IListTodoTask]));
        }

        /// <summary>
        /// This class defines the methods that can be invoked from JavaScript.
        /// </summary>
        private sealed class TodoJSInvokeTarget
        {
            private ReactPage _reactPage;
            private readonly TodoDataStore _todoDataStore;

            public TodoJSInvokeTarget(ReactPage reactPage, TodoDataStore todoDataStore)
            {
                _reactPage = reactPage;
                _todoDataStore = todoDataStore;
            }

            public void StartTaskLoading()
            {
                _reactPage.SendUpdatedTasksToJS(_todoDataStore.GetData());
            }

            public void SetTodos(TodoTask[] todos)
            {
                _todoDataStore.SetData(todos);
            }
        }

        // This type's attributes specify JSON serialization info to preserve type structure for trimmed builds.
        [JsonSourceGenerationOptions(WriteIndented = false)]
        [JsonSerializable(typeof(IList<TodoTask>))]
        [JsonSerializable(typeof(string))]
        internal partial class TodoAppJsContext : JsonSerializerContext
        {
        }
    }
}
