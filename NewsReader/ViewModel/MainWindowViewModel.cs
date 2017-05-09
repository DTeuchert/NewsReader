using System;
using System.Linq;
using System.Xml;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NewsReader.Model;
using NewsReader.Model.Enum;
using NewsReader.Util;
using NewsReader.View;

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
                OnPropertyChanged("IsConfigurationControlVisible");
            }
        
        }

        private bool _isBookmarkControlVisible;
        public bool IsBookmarkControlVisible
        {
            get { return _isBookmarkControlVisible; }
            set
            {
                _isBookmarkControlVisible = value;
                OnPropertyChanged("IsBookmarkControlVisible");
            }

        }

        private DateTimeOffset _lastUpdate = DateTimeOffset.Now;
        public DateTimeOffset LastUpdate 
        { 
            get { return _lastUpdate; }
            set {
                _lastUpdate = value;
                OnPropertyChanged("LastUpdate");
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        public RSSLinkCollection SourceList { get; set; }
        public RSSFeedCollection BookmarkList { get; set; }
        public RSSFeedCollection FeedList { get; set; }
        
        public ICommand AddRssLinkCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkAddWindow 
                    {
                        RSSLinkList = SourceList
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                });
            }
        }
        public ICommand EditRssLinkCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkEditWindow
                    {
                        EditLink = SourceList_SelectedItem
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                });
            }
        }
        public ICommand RemoveRssLinkCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var changeRSSLinkWindow = new RssLinkRemoveWindow
                    {
                        RemoveLink = SourceList_SelectedItem,
                        RSSLinkList = SourceList
                    };
                    changeRSSLinkWindow.Closed += OnClose_RSSLinkWindow;
                    changeRSSLinkWindow.ShowDialog();
                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(updateFeedList);
            }
        }
        public ICommand SettingsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsConfigurationControlVisible = !IsConfigurationControlVisible;
                });
            }
        }
        public ICommand BookmarkCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsBookmarkControlVisible = !IsBookmarkControlVisible;
                    if (IsBookmarkControlVisible)
                    {
                        BookmarkList.Clear();
                        foreach(var x in FeedList.ToList()){
                            if (x.IsMarked) BookmarkList.Add(x);
                        }
                        OnPropertyChanged("BookmarkList");
                    }
                });
            }
        }

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
                OnPropertyChanged("TabVisibility_General");
            } 
        }   
        public bool TabVisibility_Sport
        {
            get { return _visibleCategories[RSSCategory.Sport]; }
            set
            {
                _visibleCategories[RSSCategory.Sport] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Sport");
            }
        }
        public bool TabVisibility_Technology
        {
            get { return _visibleCategories[RSSCategory.Technology]; }
            set
            {
                _visibleCategories[RSSCategory.Technology] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Technology");
            }
        }
        public bool TabVisibility_Health
        {
            get { return _visibleCategories[RSSCategory.Health]; }
            set
            {
                _visibleCategories[RSSCategory.Health] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Health");
            }
        }
        public bool TabVisibility_Economy
        {
            get { return _visibleCategories[RSSCategory.Economy]; }
            set
            {
                _visibleCategories[RSSCategory.Economy] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Economy");
            }
        }
        public bool TabVisibility_Career
        {
            get { return _visibleCategories[RSSCategory.Career]; }
            set
            {
                _visibleCategories[RSSCategory.Career] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Career");
            }
        }
        public bool TabVisibility_International
        {
            get { return _visibleCategories[RSSCategory.International]; }
            set
            {
                _visibleCategories[RSSCategory.International] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_International");
            }
        }
        public bool TabVisibility_Politics
        {
            get { return _visibleCategories[RSSCategory.Politics]; }
            set
            {
                _visibleCategories[RSSCategory.Politics] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Politics");
            }
        }
        public bool TabVisibility_Cultural
        {
            get { return _visibleCategories[RSSCategory.Cultural]; }
            set
            {
                _visibleCategories[RSSCategory.Cultural] = value;
                OnChange_RSSCategory();
                OnPropertyChanged("TabVisibility_Cultural");
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
                OnPropertyChanged("SourceList_SelectedItem");
            }
        }

        public MainWindowViewModel(){

            FeedList = new RSSFeedCollection();
            BookmarkList = new RSSFeedCollection();

            updateSourceList();
            updateFeedList();
            loadVisibleTabs();
        }

        private void OnClose_RSSLinkWindow(object sender, EventArgs e)
        {
            if ((sender as Window).DialogResult == true)
            {
                SerializeHandler.Save_Links(SourceList);
                updateFeedList();
            }          
        }
        private void OnChange_RSSCategory()
        {
            SerializeHandler.Save_Tabs(_visibleCategories);
        }

        private void updateSourceList()
        {
            SourceList = SerializeHandler.Load_Links();
        }
        private void loadVisibleTabs()
        {
            _visibleCategories = SerializeHandler.Load_Tabs();
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
            if (FeedList == null) { return; };
                    
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
                                Source = feed.Authors?.FirstOrDefault()?.Name ?? RSSLink.Title,
                                Date = item.PublishDate,
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
