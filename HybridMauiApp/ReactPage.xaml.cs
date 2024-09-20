using System.Text.Json;
using System.Text.Json.Serialization;

namespace HybridMauiApp
{
    public partial class ReactPage : ContentPage
    {
        private readonly TodoDataStore _todoDataStore;
        private readonly TodoJSInvokeTarget _todoJSInvokeTarget;

        public ReactPage()
        {
            InitializeComponent();

            _todoDataStore = new TodoDataStore();
            _todoDataStore.TaskDataChanged += OnTodoDataChanged;

            _todoJSInvokeTarget = new TodoJSInvokeTarget(this, _todoDataStore);

            BindingContext = this;
        }

        public string TodoAppTitle => $"Todo items: {_todoDataStore.GetData().Count}";

        private void OnTodoDataChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TodoAppTitle));
        }

        private void myHybridWebView_RawMessageReceived(object sender, HybridWebViewRawMessageReceivedEventArgs e)
        {
            // NOTE: This is only needed until HybridWebView add support for JavaScript to invoke C# methods
            if (e.Message is not null && e.Message.StartsWith("Invoke:"))
            {
                var invokeInfo = e.Message.Substring("Invoke:".Length);
                var nextColonIndex = invokeInfo.IndexOf(':');
                var methodName = invokeInfo.Substring(0, nextColonIndex);
                var paramValues = invokeInfo.Substring(nextColonIndex + 1);

                switch (methodName)
                {
                    case "AddTask":
                        var addData = JsonSerializer.Deserialize<IList<TodoTask>>(paramValues);
                        _todoJSInvokeTarget.AddTask(new TodoTask { id = addData.First().id, name = addData.First().name, completed = addData.First().completed });
                        break;
                    case "DeleteTask":
                        var deleteData = JsonSerializer.Deserialize<string[]>(paramValues);
                        _todoJSInvokeTarget.DeleteTask(deleteData[0]);
                        break;
                    case "EditTask":
                        var editData = JsonSerializer.Deserialize<IList<string>>(paramValues);
                        _todoJSInvokeTarget.EditTask(editData[0], editData[1]);
                        break;
                    case "StartTaskLoading":
                        _todoJSInvokeTarget.StartTaskLoading();
                        break;
                    case "ToggleCompletedTask":
                        var toggleCompletedData = JsonSerializer.Deserialize<string[]>(paramValues);
                        _todoJSInvokeTarget.ToggleCompletedTask(toggleCompletedData[0]);
                        break;
                }
            }
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

            public void AddTask(TodoTask newTask)
            {
                _todoDataStore.AddTask(newTask);
            }

            public void EditTask(string id, string newName)
            {
                _todoDataStore.EditTask(id, newName);
            }

            public void DeleteTask(string id)
            {
                _todoDataStore.DeleteTask(id);
            }

            public void ToggleCompletedTask(string id)
            {
                _todoDataStore.ToggleCompletedTask(id);
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
