using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using NewsReader.Models;
using NewsReader.Util;

namespace NewsReader.Model
{

    public class RSSFeedCollection : ObservableCollection<RSSFeed>
    {
        public void Sort<RSSFeed>()
        {
            InternalSort(Items.OrderBy(x => x.Date));
        }

        private void InternalSort(IEnumerable<RSSFeed> sortedItems)
        {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }
    }

    public class RSSFeed : BaseModel, IComparable<RSSFeed>
    {
        public string Guid { get; set; }
        public RSSLink Source { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Link { get; set; }
        public Uri Thumbnail { get; set; }

        private List<RssCategory> _category = new List<RssCategory>();
        public List<RssCategory> Category { 
            get { return _category; } 
        }

        private bool _hasBeenSeen;
        public bool HasBeenSeen
        {
            get
            {
                return _hasBeenSeen;
            }
            set
            {
                _hasBeenSeen = value;
                OnPropertyChanged(nameof(HasBeenSeen));
            }
        }
                
        private bool _isMarked;
        public bool IsMarked
        {
            get
            {
                return _isMarked;
            }
            set
            {
                _isMarked = value;
                OnPropertyChanged(nameof(IsMarked));
            }
        }

        private bool _isCollapsed = false;
        public bool IsCollapsed
        {
            get
            {
                return _isCollapsed;
            }
            set
            {
                _isCollapsed = value;
                OnPropertyChanged(nameof(IsCollapsed));
            }
        }

        public ICommand ChangeIsCollapsed
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsCollapsed = !IsCollapsed;
                });
            }
        }
        public ICommand ChangeIsMarked
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsMarked = !IsMarked;
                });
            }
        }
        public ICommand OpenLink
        {
            get
            {
                return new RelayCommand(() =>
                {
                    System.Diagnostics.Process.Start(Link.ToString());
                    HasBeenSeen = true;
                });
            }
        }

        #region IComparable Members
        public int CompareTo(RSSFeed other)
        {
            if (this.Date == other.Date) return 0;
            return this.Date.CompareTo(other.Date);
        }
        #endregion 
    }
}
