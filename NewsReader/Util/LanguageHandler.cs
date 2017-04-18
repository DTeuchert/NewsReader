using System;
using System.Threading;
using System.Windows;

namespace NewsReader.Util
{
    class LanguageHandler
    {
        public static void SetLanguageDictionary(ResourceDictionary app)
        {
            var dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "de-DE":
                    dict.Source = new Uri(@"Resources\Language\StringResources.de-DE.xaml", UriKind.Relative);
                    break;
                case "en-US":
                    dict.Source = new Uri(@"Resources\Language\StringResources.en-US.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri(@"Resources\Language\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            app.MergedDictionaries.Add(dict);
        }
    }
}
