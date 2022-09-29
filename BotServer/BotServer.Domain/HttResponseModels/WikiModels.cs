namespace BotServer.Domain.HttResponseModels
{
    public class WikiModels
    {
        public class Rootobject
        {
            public string batchcomplete { get; set; }
            public Continue _continue { get; set; }
            public Query query { get; set; }
        }

        public class Continue
        {
            public int gsroffset { get; set; }
            public string _continue { get; set; }
        }

        public class Query
        {
            public Page[] pages { get; set; }
        }

        public class Page
        {
            public int pageid { get; set; }
            public int ns { get; set; }
            public string title { get; set; }
            public int index { get; set; }
        }

    }
}
