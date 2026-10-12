using Microsoft.UI;

namespace Uno.WebView2.Collapsed;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private async void OnCreateAndNavigateWebViewClick(object sender, RoutedEventArgs e)
    {
        try
        {
            WebViewPlaceholder.Child = null;

            Microsoft.UI.Xaml.Controls.WebView2 webView2 = new();
            WebViewPlaceholder.Child = webView2;

            await webView2.EnsureCoreWebView2Async();
            webView2.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewCreatedStatusTextBlock, args);
            webView2.CoreWebView2.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewCreatedStatusTextBlock, args);

            await Navigate(webView2, WebViewCreatedStatusTextBlock);
        }
        catch (Exception ex)
        {
            OnError(WebViewCreatedStatusTextBlock, ex);
        }
    }

    private async void OnShowAndNavigateWebViewClick(object sender, RoutedEventArgs e)
    {
        try
        {
            WebViewCollapsed.Visibility = Visibility.Visible;
            await WebViewCollapsed.EnsureCoreWebView2Async();
            WebViewCollapsed.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewCollapsedStatusTextBlock, args);
            WebViewCollapsed.CoreWebView2.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewCollapsedStatusTextBlock, args);
            await Navigate(WebViewCollapsed, WebViewCollapsedStatusTextBlock);
        }
        catch (Exception ex)
        {
            OnError(WebViewCollapsedStatusTextBlock, ex);
        }
    }

    private async void OnOpacityAndNavigateWebViewClick(object sender, RoutedEventArgs e)
    {
        try
        {
            WebViewOpacity.Opacity = 1;
            await WebViewOpacity.EnsureCoreWebView2Async();
            WebViewOpacity.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewOpacityStatusTextBlock, args);
            WebViewOpacity.CoreWebView2.NavigationCompleted += (s, args) => UpdateNavigationStatusTextBlock(WebViewOpacityStatusTextBlock, args);
            await Navigate(WebViewOpacity, WebViewOpacityStatusTextBlock);
        }
        catch (Exception ex)
        {
            OnError(WebViewOpacityStatusTextBlock, ex);
        }
    }

    private async void OnNavigateWebViewVisibleClick(object sender, RoutedEventArgs e)
    {
        try
        {
            await Navigate(WebViewVisible, WebViewVisibleStatusTextBlock);
        }
        catch (Exception ex)
        {
            OnError(WebViewVisibleStatusTextBlock, ex);
        }
    }

    private void WebViewCollapsed_NavigationCompleted(Microsoft.UI.Xaml.Controls.WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        UpdateNavigationStatusTextBlock(WebViewCollapsedStatusTextBlock, args);
    }

    private void WebViewOpacity_NavigationCompleted(Microsoft.UI.Xaml.Controls.WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        UpdateNavigationStatusTextBlock(WebViewOpacityStatusTextBlock, args);
    }

    private void WebViewVisible_NavigationCompleted(Microsoft.UI.Xaml.Controls.WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        UpdateNavigationStatusTextBlock(WebViewVisibleStatusTextBlock, args);
    }

    private static async Task Navigate(Microsoft.UI.Xaml.Controls.WebView2 webView2, TextBlock textBlock)
    {
        textBlock.Text = "Navigating...";
        textBlock.Foreground = new SolidColorBrush(Colors.Orange);

        await webView2.EnsureCoreWebView2Async();
        webView2.CoreWebView2.Navigate("https://platform.uno");
    }

    private static void UpdateNavigationStatusTextBlock(TextBlock textBlock, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        textBlock.Text = $"Navigation completed: {args.HttpStatusCode}";
        textBlock.Foreground = args.IsSuccess ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.LightCoral);
    }

    private static void OnError(TextBlock textBlock, Exception ex)
    {
        textBlock.Text = $"Error: {ex.Message}";
        textBlock.Foreground = new SolidColorBrush(Colors.Red);
    }
}
