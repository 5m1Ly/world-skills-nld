using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VinkeveenseCourant
{
    public partial class Mainform : Form
    {
        private List<NewsItem> newsitems;

        public Mainform()
        {
            InitializeComponent();
            getNewsItems();
        }

        private void getNewsItems()
        {
            newsitems = new List<NewsItem>();

            // Add temporary news items
            newsitems.Add(new NewsItem(1, "Nederland wint gouden medaille", "Op de olympische spelen in Rio heeft Nederland een gouden medaille gewonnen op de 1000 meter sprint.", "Sport", DateTime.Today.AddDays(-1)));
            newsitems.Add(new NewsItem(2, "Tractor Gestolen", "De tractor van boer Harms is gestolen. Dit gebeurde in de nacht van zaterdag op zondag .", "Regio", DateTime.Now));

            // TODO:
            // Get news items from API

            ShowNewsItems();
        }

        private void ShowNewsItems()
        {
            int y = 55;
            Label[] labelArray = new Label[newsitems.Count];
            

            for (int i = 0; i < newsitems.Count; i++)
            {
                NewsItem currentNewsItem = (NewsItem) newsitems[i];
                labelArray[i] = new Label();
                labelArray[i].Size = new Size(770, 23);
                labelArray[i].Location = new Point(15, y);
                labelArray[i].Text = currentNewsItem.title + " - " + currentNewsItem.category + " - " + currentNewsItem.date.ToShortDateString();

                y += 25;

                this.Controls.Add(labelArray[i]);

            }
        }
    }
}
