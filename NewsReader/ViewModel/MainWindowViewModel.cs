using NewsReader.Model;
using NewsReader.Models;
using NewsReader.Service;
using NewsReader.Util;
using NewsReader.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace NewsReader.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private List<string> _guidList = new List<string>(); 

        private bool _isConfigurationControlVisible;
        public bool IsConfigurationControlVisible 
        {
            get { return _isConfigurationControlVisible; }
            set {
                _isConfigurationControlVisible = value;
                OnPropertyChanged(nameof(IsConfigurationControlVisible));
            }
        
        }

        private bool _isBookmarkControlVisible;
        public bool IsBookmarkControlVisible
        {
            get { return _isBookmarkControlVisible; }
            set
            {
                _isBookmarkControlVisible = value;
                OnPropertyChanged(nameof(IsBookmarkControlVisible));
            }

        }

        private DateTimeOffset _lastUpdate = DateTimeOffset.Now;
        public DateTimeOffset LastUpdate 
        { 
            get { return _lastUpdate; }
            set {
                _lastUpdate = value;
                OnPropertyChanged(nameof(LastUpdate));
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public RSSLinkCollection SourceList { get; set; }
        public RSSFeedCollection BookmarkList { get; set; }
        public RSSFeedCollection FeedList { get; set; }

        private ICommand _addRssLinkCommand;
        public ICommand AddRssLinkCommand => _addRssLinkCommand ??
            (_addRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkAddWindow
                    {
                        RSSLinkList = SourceList
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                }));

        private ICommand _editRssLinkCommand;
        public ICommand EditRssLinkCommand => _editRssLinkCommand ??
            (_editRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkEditWindow
                    {
                        EditLink = SourceList_SelectedItem
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                }));

        private ICommand _removeRssLinkCommand;
        public ICommand RemoveRssLinkCommand => _removeRssLinkCommand ??
            (_removeRssLinkCommand = new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkRemoveWindow
                    {
                        RemoveLink = SourceList_SelectedItem,
                        RSSLinkList = SourceList
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                }));

        private ICommand _isEnabledRssLinkCommand;
        public ICommand IsEnabledRssLinkCommand => _isEnabledRssLinkCommand ??
            (_isEnabledRssLinkCommand = new RelayCommand(() =>
                {
                    SourceList_SelectedItem.IsEnabled = !SourceList_SelectedItem.IsEnabled;
                    ConfigurationService.Links = SourceList;
                }));

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??
            (_refreshCommand = new RelayCommand(() => updateFeedList()));

        private ICommand _settingsCommand;
        public ICommand SettingsCommand => _settingsCommand ??
            (_settingsCommand = new RelayCommand(() =>
                {
                    IsConfigurationControlVisible = !IsConfigurationControlVisible;
                }));

        public ICommand _bookmarkCommand;
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

        private Dictionary<RssCategory, bool> _visibleCategories = new Dictionary<RssCategory, bool>
        {
            { RssCategory.General, true },
            { RssCategory.Sport, true },
            { RssCategory.Technology, true },
            { RssCategory.Health, true },
            { RssCategory.Economy, true },
            { RssCategory.Career, true },
            { RssCategory.International, true },
            { RssCategory.Politics, true },
            { RssCategory.Cultural, true }
        };
                
        public bool TabVisibility_General 
        {
            get { return _visibleCategories[RssCategory.General]; }
            set 
            {
                _visibleCategories[RssCategory.General] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_General));
            } 
        }   
        public bool TabVisibility_Sport
        {
            get { return _visibleCategories[RssCategory.Sport]; }
            set
            {
                _visibleCategories[RssCategory.Sport] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Sport));
            }
        }
        public bool TabVisibility_Technology
        {
            get { return _visibleCategories[RssCategory.Technology]; }
            set
            {
                _visibleCategories[RssCategory.Technology] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Technology));
            }
        }
        public bool TabVisibility_Health
        {
            get { return _visibleCategories[RssCategory.Health]; }
            set
            {
                _visibleCategories[RssCategory.Health] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Health));
            }
        }
        public bool TabVisibility_Economy
        {
            get { return _visibleCategories[RssCategory.Economy]; }
            set
            {
                _visibleCategories[RssCategory.Economy] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Economy));
            }
        }
        public bool TabVisibility_Career
        {
            get { return _visibleCategories[RssCategory.Career]; }
            set
            {
                _visibleCategories[RssCategory.Career] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Career));
            }
        }
        public bool TabVisibility_International
        {
            get { return _visibleCategories[RssCategory.International]; }
            set
            {
                _visibleCategories[RssCategory.International] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_International));
            }
        }
        public bool TabVisibility_Politics
        {
            get { return _visibleCategories[RssCategory.Politics]; }
            set
            {
                _visibleCategories[RssCategory.Politics] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Politics));
            }
        }
        public bool TabVisibility_Cultural
        {
            get { return _visibleCategories[RssCategory.Cultural]; }
            set
            {
                _visibleCategories[RssCategory.Cultural] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Cultural));
            }
        }


        private RSSLink _sourceList_SelectedItem;
        public RSSLink SourceList_SelectedItem
        { 
            get
            {
                return _sourceList_SelectedItem;
            }
            set
            {
                _sourceList_SelectedItem = value;
                OnPropertyChanged(nameof(SourceList_SelectedItem));
            }
        }

        public MainWindowViewModel(){

            FeedList = new RSSFeedCollection();
            BookmarkList = new RSSFeedCollection();

            SourceList = ConfigurationService.Links;
            _visibleCategories = ConfigurationService.VisibleCategories;
            updateFeedList();
        }

        private void OnClose_RSSLinkWindow(object sender, EventArgs e)
        {
            if ((sender as Window).DialogResult == true)
            {
                ConfigurationService.Links = SourceList;
                updateFeedList();
            }          
        }
        private void OnChange_RSSCategory()
        {
            ConfigurationService.VisibleCategories = _visibleCategories;
        }
        
        private void updateFeedList() 
        {
            if(IsRefreshing) { return; }

            IsRefreshing = true;
            foreach (var feed in SourceList)
            {
                UpdateFeed(feed);
            }
            FeedList.ToList().Sort();

            LastUpdate = DateTimeOffset.Now;
            IsRefreshing = false;
        }
        private void UpdateFeed(RSSLink rssLink)
        {
            if (FeedList == null) { return; }
                    
            try
            {
                using (var reader = XmlReader.Create(rssLink.Link.ToString()))
                {
                    var feed = SyndicationFeed.Load(reader);
                    if (feed == null) return;
                    
                    foreach (var item in feed.Items)
                    {
                        if (!_guidList.Any(x => x.Equals(item.Id)))
                        {
                            var news = new RSSFeed
                            {
                                Guid = item.Id,
                                Source = rssLink,
                                Date = item.PublishDate.UtcDateTime,
                                Title = item.Title.Text,
                                Link = item.Links[0].Uri
                            };

                            if (item.Summary != null) news.Description = item.Summary.Text;
                            if (item.Links.Count >= 2) news.Thumbnail = item.Links[1].Uri;

                            news.Category.Add(RssCategory.General);

                            if (item.Categories.Any(x => 
                                x.Name.Equals("Sport") || 
                                x.Name.Equals("Fußball"))) news.Category.Add(RssCategory.Sport);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Technology") || 
                                x.Name.Equals("Digital"))) news.Category.Add(RssCategory.Technology);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Gesundheit"))) news.Category.Add(RssCategory.Health);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Wirtschaft"))) news.Category.Add(RssCategory.Economy);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Karriere"))) news.Category.Add(RssCategory.Career);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("International"))) news.Category.Add(RssCategory.International);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Politik"))) news.Category.Add(RssCategory.Politics);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Kultur"))) news.Category.Add(RssCategory.Cultural);
       
                            FeedList.Add(news);
                            _guidList.Add(news.Guid);
                        }           
                    }
                }
            }
            catch(Exception e)
            {
                rssLink.IsValid = false;
            }
        }
    }
}
