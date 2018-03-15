﻿using System.Windows.Data;

namespace NewsReader.Util
{
    public class LocalizationExtension : Binding
    {
        public LocalizationExtension(string name)
        : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}
