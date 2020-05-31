using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Azmoon.Localization
{
    public static class AzmoonLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AzmoonConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AzmoonLocalizationConfigurer).GetAssembly(),
                        "Azmoon.Core.Localization.SourceFiles"
                    )
                )
            );

            //localizationConfiguration.ReturnGivenTextIfNotFound = false;
            localizationConfiguration.LogWarnMessageIfNotFound = true;
            localizationConfiguration.WrapGivenTextIfNotFound = true;
        }
    }
}
