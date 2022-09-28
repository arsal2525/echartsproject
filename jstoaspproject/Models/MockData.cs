using System;
namespace jstoaspproject.Models
{
    public class MockData
    {
        public List<int> EmailData { get; set; }
        public List<int> VideoAds { get; set; }
        public List<int> SearchEngines { get; set; }
        public List<int> Direct { get; set; }

        public void Build()
        {
            EmailData = new List<int>();
            VideoAds = new List<int>();
            SearchEngines = new List<int>();
            Direct = new List<int>();

            EmailData.Add(24);
            EmailData.Add(26);
            EmailData.Add(25);
            EmailData.Add(23);

            VideoAds.Add(34);
            VideoAds.Add(36);
            VideoAds.Add(35);
            VideoAds.Add(33);

            SearchEngines.Add(44);
            SearchEngines.Add(46);
            SearchEngines.Add(45);
            SearchEngines.Add(43);

            Direct.Add(54);
            Direct.Add(56);
            Direct.Add(55);
            Direct.Add(53);
        }


    }
}

