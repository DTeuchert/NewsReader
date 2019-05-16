using System.Collections.Generic;
using NewsReader.Models;
using NewsReader.Service;

namespace NewsReader.ViewModel
{
    internal class CategoryVisibilityViewModel : BaseViewModel
    {
        private readonly Dictionary<RssCategory, bool> _visibleCategories;

        public CategoryVisibilityViewModel()
        {
            _visibleCategories = ConfigurationService.VisibleCategories;
        }

        public bool General
        {
            get => _visibleCategories[RssCategory.General];
            set
            {
                _visibleCategories[RssCategory.General] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(General));
            }
        }

        public bool Sport
        {
            get => _visibleCategories[RssCategory.Sport];
            set
            {
                _visibleCategories[RssCategory.Sport] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Sport));
            }
        }

        public bool Technology
        {
            get => _visibleCategories[RssCategory.Technology];
            set
            {
                _visibleCategories[RssCategory.Technology] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Technology));
            }
        }

        public bool Health
        {
            get => _visibleCategories[RssCategory.Health];
            set
            {
                _visibleCategories[RssCategory.Health] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Health));
            }
        }

        public bool Economy
        {
            get => _visibleCategories[RssCategory.Economy];
            set
            {
                _visibleCategories[RssCategory.Economy] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Economy));
            }
        }

        public bool Career
        {
            get => _visibleCategories[RssCategory.Career];
            set
            {
                _visibleCategories[RssCategory.Career] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Career));
            }
        }

        public bool International
        {
            get => _visibleCategories[RssCategory.International];
            set
            {
                _visibleCategories[RssCategory.International] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(International));
            }
        }

        public bool Politics
        {
            get => _visibleCategories[RssCategory.Politics];
            set
            {
                _visibleCategories[RssCategory.Politics] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Politics));
            }
        }

        public bool Cultural
        {
            get => _visibleCategories[RssCategory.Cultural];
            set
            {
                _visibleCategories[RssCategory.Cultural] = value;
                OnChange_RSSCategory();
                OnPropertyChanged(nameof(Cultural));
            }
        }

        private void OnChange_RSSCategory()
        {
            ConfigurationService.VisibleCategories = _visibleCategories;
        }
    }
}
