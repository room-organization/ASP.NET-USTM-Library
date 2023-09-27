﻿namespace USTM_Library.Models
{
    public class bibliography
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string PublishingCompany { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string BookUrl { get; set; }
    }
}
