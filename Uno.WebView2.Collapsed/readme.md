
# Project Uno.WebView2.Collapsed

## Description

The experiment demonstrates differences in how the `NavigationCompleted` event is raised. The behavior depends on whether the `WebView2` element is visible or collapsed when the `MainPage` opens.

### Project Creation

This project is created using **Uno.Template** v6.7.30.

```console
dotnet new unoapp -o Uno.WebView2.Collapsed -preset "recommended" -platforms "desktop" -vscode False -presentation "mvvm" -http "none" -di False -log "none" -nav "blank" -toolkit False -theme-service False
```

### Test Results

- Windows - `NavigationCompleted` event is raised for both variants, regardless of visibility.
- Linux - N/A
- macOS - N/A
