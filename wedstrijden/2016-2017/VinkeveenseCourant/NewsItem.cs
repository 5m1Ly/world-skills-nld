using System;

namespace VinkeveenseCourant
{
    class NewsItem
    {
        public int id { get; set; }
        public String title { get; set; }
        public String content { get; set; }
        public String category { get; set; }
        public DateTime date { get; set; }

        public NewsItem(int id, String title, String content, String category, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.category = category;
            this.date = date;
        }
    }
}
