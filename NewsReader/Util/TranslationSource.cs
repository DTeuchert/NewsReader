﻿using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace NewsReader.Util
{
    public class TranslationSource
        : INotifyPropertyChanged
    {
        public static TranslationSource Instance { get; } = new TranslationSource();

        private readonly ResourceManager _resManager = Resources.Strings.Resources.ResourceManager;
        private CultureInfo _currentCulture;

        public string this[string key] => _resManager.GetString(key, _currentCulture);

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (Equals(_currentCulture, value))
                {
                    return;
                }

                _currentCulture = value;
                var @event = PropertyChanged;
                @event?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
