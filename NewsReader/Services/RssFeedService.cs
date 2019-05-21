using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using NewsReader.Models;

namespace NewsReader.Services
{
    internal class RssFeedService
    {
        private readonly List<string> _guidList = new List<string>();

        public List<RssFeed> GetRssFeeds(RssLink rssLink)
        {
           using (var reader = XmlReader.Create(rssLink.Link))
           {
                var rssFeeds = new List<RssFeed>();
                var feed = SyndicationFeed.Load(reader);

                foreach (var item in feed.Items)
                {
                    if (_guidList.Any(x => x.Equals(item.Id)))
                    {
                        continue;
                    }

                    var rssFeed = new RssFeed
                    {
                        Guid = item.Id,
                        Source = rssLink,
                        Date = item.PublishDate.UtcDateTime,
                        Title = item.Title.Text,
                        Link = item.Links[0].Uri,
                        Description = item.Summary?.Text ?? string.Empty,
                        Thumbnail = (item.Links.Count >= 2) ? item.Links[1].Uri : null
                    };

                    rssFeed.Category.Add(RssCategory.General);

                    if (item.Categories.Any(x =>
                        x.Name.Equals("Sport") ||
                        x.Name.Equals("Fußball"))) rssFeed.Category.Add(RssCategory.Sport);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Technology") ||
                        x.Name.Equals("Digital"))) rssFeed.Category.Add(RssCategory.Technology);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Gesundheit"))) rssFeed.Category.Add(RssCategory.Health);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Wirtschaft"))) rssFeed.Category.Add(RssCategory.Economy);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Karriere"))) rssFeed.Category.Add(RssCategory.Career);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("International"))) rssFeed.Category.Add(RssCategory.International);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Politik"))) rssFeed.Category.Add(RssCategory.Politics);
                    if (item.Categories.Any(x =>
                        x.Name.Equals("Kultur"))) rssFeed.Category.Add(RssCategory.Cultural);

                    _guidList.Add(rssFeed.Guid);
                    rssFeeds.Add(rssFeed);
                }
                return rssFeeds;
            }
        }
    }
}
