using System;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Uno.WebView2.Collapsed;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private async void OnShowAndNavigateWebViewCollapsedClick(object sender, RoutedEventArgs e)
    {
        try
        {
            WebViewCollapsed.Visibility = Visibility.Visible;
            await Navigate(WebViewCollapsed, WebViewCollapsedStatusTextBlock);
        }
        catch (Exception ex)
        {
            OnError(WebViewCollapsedStatusTextBlock, ex);
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

public class VisibilityToStringConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Visibility visibility)
        {
            return visibility.ToString();
        }
        return "Unknown";
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
