using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NewsReader.Models;
using NewsReader.Services;
using NewsReader.Util;
using NewsReader.Views;

namespace NewsReader.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly RssFeedService _rssFeedService;
        private bool _isConfigurationControlVisible;
        public bool IsConfigurationControlVisible
        {
            get => _isConfigurationControlVisible;
            set
            {
                _isConfigurationControlVisible = value;
                OnPropertyChanged(nameof(IsConfigurationControlVisible));
            }

        }

        private bool _isBookmarkControlVisible;
        public bool IsBookmarkControlVisible
        {
            get => _isBookmarkControlVisible;
            set
            {
                _isBookmarkControlVisible = value;
                OnPropertyChanged(nameof(IsBookmarkControlVisible));
            }

        }

        private DateTimeOffset _lastUpdate = DateTimeOffset.Now;
        public DateTimeOffset LastUpdate
        {
            get => _lastUpdate;
            set
            {
                _lastUpdate = value;
                OnPropertyChanged(nameof(LastUpdate));
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public RssLinkCollection SourceList { get; set; }
        public RssFeedCollection BookmarkList { get; set; }
        public RssFeedCollection FeedList { get; set; }

        private ICommand _addRssLinkCommand;
        public ICommand AddRssLinkCommand => _addRssLinkCommand ??
            (_addRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRssLinkWindow = new RssLinkAddWindow
                    {
                        RssLinks = SourceList
                    };
                    changeRssLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRssLinkWindow.ShowDialog();
                }));

        private ICommand _editRssLinkCommand;
        public ICommand EditRssLinkCommand => _editRssLinkCommand ??
            (_editRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRssLinkWindow = new RssLinkEditWindow
                    {
                        RssLink = SelectedRssLink
                    };
                    changeRssLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRssLinkWindow.ShowDialog();
                }));

        private ICommand _removeRssLinkCommand;
        public ICommand RemoveRssLinkCommand => _removeRssLinkCommand ??
            (_removeRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRssLinkWindow = new RssLinkRemoveWindow
                    {
                        RssLink = SelectedRssLink,
                        RssLinks = SourceList
                    };
                    changeRssLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRssLinkWindow.ShowDialog();
                }));

        private ICommand _isEnabledRssLinkCommand;
        public ICommand IsEnabledRssLinkCommand => _isEnabledRssLinkCommand ??
            (_isEnabledRssLinkCommand = new RelayCommand(() =>
                {
                    SelectedRssLink.IsEnabled = !SelectedRssLink.IsEnabled;
                    ConfigurationService.Links = SourceList;
                }));

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??
            (_refreshCommand = new RelayCommand(UpdateFeedList));

        private ICommand _settingsCommand;
        public ICommand SettingsCommand => _settingsCommand ??
            (_settingsCommand = new RelayCommand(() =>
                {
                    IsConfigurationControlVisible = !IsConfigurationControlVisible;
                }));

        private ICommand _bookmarkCommand;
        public ICommand BookmarkCommand => _bookmarkCommand ??
            (_bookmarkCommand = new RelayCommand(() =>
               {
                   IsBookmarkControlVisible = !IsBookmarkControlVisible;
                   if (IsBookmarkControlVisible)
                   {
                       BookmarkList.Clear();
                       foreach (var x in FeedList.ToList())
                       {
                           if (x.IsMarked) BookmarkList.Add(x);
                       }
                       OnPropertyChanged(nameof(BookmarkList));
                   }
               }));

        public CategoryVisibilityViewModel CategoryVisibility { get; set; }

        private RssLink _selectedRssLink;
        public RssLink SelectedRssLink
        {
            get => _selectedRssLink;
            set
            {
                _selectedRssLink = value;
                OnPropertyChanged(nameof(SelectedRssLink));
            }
        }

        public MainWindowViewModel()
        {

            FeedList = new RssFeedCollection();
            BookmarkList = new RssFeedCollection();
            CategoryVisibility = new CategoryVisibilityViewModel();

            _rssFeedService = new RssFeedService();

            SourceList = ConfigurationService.Links;
            UpdateFeedList();
        }

        private void OnClose_RSSLinkWindow(object sender, EventArgs e)
        {
            if ((sender as Window)?.DialogResult == true)
            {
                ConfigurationService.Links = SourceList;
                UpdateFeedList();
            }
        }

        private void UpdateFeedList()
        {
            if (IsRefreshing) { return; }

            IsRefreshing = true;
            foreach (var feed in SourceList)
            {
                UpdateFeed(feed);
            }
            FeedList.ToList().Sort();

            LastUpdate = DateTimeOffset.Now;
            IsRefreshing = false;
        }
        private void UpdateFeed(RssLink rssLink)
        {
            if (FeedList == null) { return; }

            try
            {
                _rssFeedService.GetRssFeeds(rssLink).ForEach(f => FeedList.Add(f));               
            }
            catch (Exception)
            {
                rssLink.IsValid = false;
            }
        }
    }
}
