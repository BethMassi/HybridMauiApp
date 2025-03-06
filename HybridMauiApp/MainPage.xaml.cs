using System.Text.Json.Serialization;

namespace HybridMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Set the class that defines the methods that can be invoked from JavaScript.
            hwv.SetInvokeJavaScriptTarget<ComputeJSInvokeTarget>(new ComputeJSInvokeTarget(this));          
  
        }

        int count;
        private void SendMessageButton_Clicked(object sender, EventArgs e)
        {
            hwv.SendRawMessage($"Hello from C#! #{count++}");            
        }

        private void InvokeJSNullMethodButton_Clicked(object sender, EventArgs e)
        {
            // Testing https://github.com/dotnet/maui/pull/27094
            hwv.InvokeJavaScriptAsync<string>("SendAlert", 
                null, //will not throw an exception in .NET 9 SR4 (9.0.40)
                [$"Hello from C#! #{count++}"], 
                [SampleInvokeJsContext.Default.String]);
        }

        private void InvokeJSApiMethodButton_Clicked(object sender, EventArgs e)
        {
            hwv.InvokeJavaScriptAsync<string>("CallApiAsync", null);                
        }
        

        private async void InvokeJSMethodButton_Clicked(object sender, EventArgs e)
        {
            var statusResult = "";

            var x = 123d;
            var y = 321d;
            
            var result = await hwv.InvokeJavaScriptAsync<ComputationResult>(
                "AddNumbers",
                SampleInvokeJsContext.Default.ComputationResult,
                [x, null, y, null],
                [SampleInvokeJsContext.Default.Double, null, SampleInvokeJsContext.Default.Double, null]);

            if (result is null)
            {
                statusResult += Environment.NewLine + $"Got no result for operation with {x} and {y} 😮";
            }
            else
            {
                statusResult += Environment.NewLine + $"Used operation {result.operationName} with numbers {x} and {y} to get {result.result}";
            }

            Dispatcher.Dispatch(() => statusText.Text += statusResult);
        }

        private async void InvokeAsyncJSMethodButton_Clicked(object sender, EventArgs e)
        {
            var statusResult = "";

            var asyncFuncResult = await hwv.InvokeJavaScriptAsync<Dictionary<string, string>>(
                "EvaluateMeWithParamsAndAsyncReturn",
                SampleInvokeJsContext.Default.DictionaryStringString,
                ["new_key", "new_value"],
                [SampleInvokeJsContext.Default.String, SampleInvokeJsContext.Default.String]);

            if (asyncFuncResult == null)
            {
                statusResult += Environment.NewLine + $"Got no result from EvaluateMeWithParamsAndAsyncReturn 😮";
            }
            else
            {
                statusResult += Environment.NewLine + $"Got result from EvaluateMeWithParamsAndAsyncReturn: {string.Join(",", asyncFuncResult)}";
            }

            Dispatcher.Dispatch(() => statusText.Text += statusResult);
        }

        private void hwv_RawMessageReceived(object sender, HybridWebViewRawMessageReceivedEventArgs e)
        {
            Dispatcher.Dispatch(() => statusText.Text += Environment.NewLine + e.Message);
        }

        public class ComputationResult
        {
            public double result { get; set; }
            public string? operationName { get; set; }
        }

        // This type's attributes specify JSON serialization info to preserve type structure for trimmed builds.
        [JsonSourceGenerationOptions(WriteIndented = true)]
        [JsonSerializable(typeof(ComputationResult))]
        [JsonSerializable(typeof(double))]
        [JsonSerializable(typeof(string))]
        [JsonSerializable(typeof(Dictionary<string, string>))]
        internal partial class SampleInvokeJsContext : JsonSerializerContext
        {
        }


        /// <summary>
        /// This class defines the methods that can be invoked from JavaScript.
        /// </summary>
        private sealed class ComputeJSInvokeTarget
        {
            private MainPage _mainPage;            

            public ComputeJSInvokeTarget(MainPage mainPage)
            {
                _mainPage = mainPage;                
            }

            public int AddNumbers(int number1, int number2)
            {
                var result = number1 + number2;

                _mainPage.Dispatcher.Dispatch(() => _mainPage.statusText.Text += Environment.NewLine + $"Got number {result}");
                return result;
            }

        }

    }

}
