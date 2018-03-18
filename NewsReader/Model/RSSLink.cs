using System;
using System.Collections.ObjectModel;

namespace NewsReader.Model
{
    [Serializable]
    public class RSSLinkCollection : ObservableCollection<RSSLink> { }

    [Serializable]
    public class RSSLink : BaseModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _link;
        public string Link
        {
            get { return _link; }
            set
            {
                _link = value;
                OnPropertyChanged(nameof(Link));
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
    }
}
