using MauiReactJSHybridApp;
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

            BindingContext = this;
        }

        public string TodoAppTitle => $"Todo items: {_todoDataStore.GetData().Count}";

        private void OnTodoDataChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TodoAppTitle));
        }



    }

}
