﻿#region

using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HyPlayer.Controls;
using HyPlayer.HyPlayControl;

#endregion

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace HyPlayer.Pages;

/// <summary>
///     可用于自身或导航至 Frame 内部的空白页。
/// </summary>
public sealed partial class DownloadPage : Page, IDisposable
{
    public DownloadPage()
    {
        InitializeComponent();
    }

    public void Dispose()
    {
        DLList.Children.Clear();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        HyPlayList.OnTimerTicked += DownloadPage_Elapsed;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        HyPlayList.OnTimerTicked -= DownloadPage_Elapsed;
    }

    private void DownloadPage_Elapsed()
    {
        _ = Common.Invoke(() =>
        {
            if (DLList.Children.Count != DownloadManager.DownloadLists.Count)
            {
                while (DLList.Children.Count > DownloadManager.DownloadLists.Count)
                    DLList.Children.RemoveAt(DLList.Children.Count - 1);

                while (DLList.Children.Count < DownloadManager.DownloadLists.Count)
                    DLList.Children.Add(new SingleDownload(DLList.Children.Count));
            }

            foreach (var uiElement in DLList.Children)
            {
                var dl = (SingleDownload)uiElement;
                dl.UpdateUI();
            }
        });
    }

    private void Button_Cleanall_Click(object sender, RoutedEventArgs e)
    {
        DownloadManager.DownloadLists.ForEach(t =>
        {
            if (t.Status == 1)
                t.downloadOperation = null;
        });
        DownloadManager.DownloadLists = new List<DownloadObject>();
    }

    private async void OpenDownloadFolder_Click(object sender, RoutedEventArgs e)
    {
        await Launcher.LaunchFolderPathAsync(Common.Setting.downloadDir);
    }
}