using NewsReader.Model;
using NewsReader.Model.Enum;
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

        private Dictionary<RSSCategory, bool> _visibleCategories = new Dictionary<RSSCategory, bool>
        {
            { RSSCategory.General, true },
            { RSSCategory.Sport, true },
            { RSSCategory.Technology, true },
            { RSSCategory.Health, true },
            { RSSCategory.Economy, true },
            { RSSCategory.Career, true },
            { RSSCategory.International, true },
            { RSSCategory.Politics, true },
            { RSSCategory.Cultural, true }
        };
                
        public bool TabVisibility_General 
        {
            get { return _visibleCategories[RSSCategory.General]; }
            set 
            {
                _visibleCategories[RSSCategory.General] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_General));
            } 
        }   
        public bool TabVisibility_Sport
        {
            get { return _visibleCategories[RSSCategory.Sport]; }
            set
            {
                _visibleCategories[RSSCategory.Sport] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Sport));
            }
        }
        public bool TabVisibility_Technology
        {
            get { return _visibleCategories[RSSCategory.Technology]; }
            set
            {
                _visibleCategories[RSSCategory.Technology] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Technology));
            }
        }
        public bool TabVisibility_Health
        {
            get { return _visibleCategories[RSSCategory.Health]; }
            set
            {
                _visibleCategories[RSSCategory.Health] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Health));
            }
        }
        public bool TabVisibility_Economy
        {
            get { return _visibleCategories[RSSCategory.Economy]; }
            set
            {
                _visibleCategories[RSSCategory.Economy] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Economy));
            }
        }
        public bool TabVisibility_Career
        {
            get { return _visibleCategories[RSSCategory.Career]; }
            set
            {
                _visibleCategories[RSSCategory.Career] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Career));
            }
        }
        public bool TabVisibility_International
        {
            get { return _visibleCategories[RSSCategory.International]; }
            set
            {
                _visibleCategories[RSSCategory.International] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_International));
            }
        }
        public bool TabVisibility_Politics
        {
            get { return _visibleCategories[RSSCategory.Politics]; }
            set
            {
                _visibleCategories[RSSCategory.Politics] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(TabVisibility_Politics));
            }
        }
        public bool TabVisibility_Cultural
        {
            get { return _visibleCategories[RSSCategory.Cultural]; }
            set
            {
                _visibleCategories[RSSCategory.Cultural] = value;
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
                updateFeed(feed);
            }
            FeedList.ToList().Sort();

            LastUpdate = DateTimeOffset.Now;
            IsRefreshing = false;
        }
        private void updateFeed(RSSLink RSSLink)
        {
            if (FeedList == null) { return; }
                    
            try
            {
                using (var reader = XmlReader.Create(RSSLink.Link.ToString()))
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
                                Source = RSSLink,
                                Date = item.PublishDate.UtcDateTime,
                                Title = item.Title.Text,
                                Link = item.Links[0].Uri
                            };

                            if (item.Summary != null) news.Description = item.Summary.Text;
                            if (item.Links.Count >= 2) news.Thumbnail = item.Links[1].Uri;

                            news.Category.Add(RSSCategory.General);

                            if (item.Categories.Any(x => 
                                x.Name.Equals("Sport") || 
                                x.Name.Equals("Fußball"))) news.Category.Add(RSSCategory.Sport);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Technology") || 
                                x.Name.Equals("Digital"))) news.Category.Add(RSSCategory.Technology);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Gesundheit"))) news.Category.Add(RSSCategory.Health);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Wirtschaft"))) news.Category.Add(RSSCategory.Economy);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Karriere"))) news.Category.Add(RSSCategory.Career);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("International"))) news.Category.Add(RSSCategory.International);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Politik"))) news.Category.Add(RSSCategory.Politics);
                            if (item.Categories.Any(x => 
                                x.Name.Equals("Kultur"))) news.Category.Add(RSSCategory.Cultural);
       
                            FeedList.Add(news);
                            _guidList.Add(news.Guid);
                        }           
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
