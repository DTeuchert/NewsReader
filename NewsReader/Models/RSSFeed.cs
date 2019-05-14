using NewsReader.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NewsReader.Models
{

    public class RssFeedCollection : ObservableCollection<RssFeed>
    {
        public void Sort<RssFeed>()
        {
            InternalSort(Items.OrderBy(x => x.Date));
        }

        private void InternalSort(IEnumerable<RssFeed> sortedItems)
        {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }
    }

    public class RssFeed : BaseModel, IComparable<RssFeed>
    {
        public string Guid { get; set; }
        public RssLink Source { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Link { get; set; }
        public Uri Thumbnail { get; set; }

        private List<RssCategory> _category = new List<RssCategory>();
        public List<RssCategory> Category => _category;

        private bool _hasBeenSeen;
        public bool HasBeenSeen
        {
            get => _hasBeenSeen;
            set
            {
                _hasBeenSeen = value;
                OnPropertyChanged(nameof(HasBeenSeen));
            }
        }

        private bool _isMarked;
        public bool IsMarked
        {
            get => _isMarked;
            set
            {
                _isMarked = value;
                OnPropertyChanged(nameof(IsMarked));
            }
        }

        private bool _isCollapsed;
        public bool IsCollapsed
        {
            get => _isCollapsed;
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
        public int CompareTo(RssFeed other)
        {
            if (Date == other.Date) return 0;
            return Date.CompareTo(other.Date);
        }
        #endregion 
    }
}
