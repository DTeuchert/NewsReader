using NewsReader.Util;

namespace NewsReader.Service
{
    class LocalizationService
    {
        public static void SetLanguage(string locale)
        {
            if (string.IsNullOrEmpty(locale)) locale = "en-US";
            ConfigurationService.Language = locale;
            TranslationSource.Instance.CurrentCulture =  new System.Globalization.CultureInfo(locale);
        }
    }
}
