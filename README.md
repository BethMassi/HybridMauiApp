# HybridMauiApp demo

This app shows the .NET MAUI 9 HybridWebView control in two scenarios:

1. A basic sample with a HybridWebView that demonstrates sending "raw messages" between .NET/C# and JavaScript, and invokes methods between .NET/C# and JavaScript.
1. A React sample with a HybridWebView that demonstrates embedding a React "Todo" app into .NET MAUI and using .NET to save/load data but use React for the UI

## Requirements

- .NET 9
- Visual Studio or Visual Studio Code

## Updating the app

1. To update the "basic sample" part of the app:
   * Change [MainPage.xaml](HybridMauiApp/MainPage.xaml)/[MainPage.xaml.cs](HybridMauiApp/MainPage.xaml.cs) for the .NET code
   * Change the [HybridMauiApp/Resources/Raw/HybridSamplePage](HybridMauiApp/Resources/Raw/HybridSamplePage) folder for the JS/HTML/CSS
1. To update the "React todo" part of the app:
   * Change [ReactPage.xaml](HybridMauiApp/ReactPage.xaml)/[ReactPage.xaml.cs](HybridMauiApp/ReactPage.xaml.cs) for the .NET UI code
   * Change [TodoDataStore.cs](HybridMauiApp/TodoDataStore.cs) for the .NET data store code (this sample stores everything in-memory, but could be changed to use permanent storage)
   * For the React part of the code:
      * Clone the https://github.com/Eilon/create-a-todo-app-with-React repo
      * Open it in Visual Studio Code and make any changes you want
      * From a shell/command window run `npm install` (just once) to ensure you have all the dependencies, and then `npx next build` to generate the output
      * Delete the contents of the [HybridMauiApp/Resources/Raw/ReactTodoApp](HybridMauiApp/Resources/Raw/ReactTodoApp) folder and replace it with the output of the `out` folder of the React app you just built

## .NET Conf 2024 video

Watch the whole "Building Hybrid Apps with .NET MAUI" recording here: https://www.youtube.com/live/hM4ifrqF_lQ?feature=shared&t=26965

And this specific app starts at this timestamp: https://www.youtube.com/live/hM4ifrqF_lQ?feature=shared&t=28143
