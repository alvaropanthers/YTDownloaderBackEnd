using System.Security.Cryptography.X509Certificates;

namespace PPFileLibrary
{
    public class PPFile
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Author { get; set; }
        public string UploadDate { get; set; }
        public string FileName { get; set; }
        private string _rawData;

        public PPFile(string rawData)
        {
            _rawData = rawData;
            var result = rawData.Split(";");
            Id = result
                .Where(s => s.ToLower().Contains("id"))
                .First().Split("=")[1];

            Url = result
                .Where(s => s.ToLower().Contains("url"))
                .First().Split("=")[1];

            Title = result
                .Where(s => s.ToLower().Contains("title"))
                .First().Split("=")[1];

            Duration = result
                .Where(s => s.ToLower().Contains("duration"))
                .First().Split("=")[1];

            Author = result
                .Where(s => s.ToLower().Contains("author"))
                .First().Split("=")[1];

            UploadDate = result
                .Where(s => s.ToLower().Contains("uploaddate"))
                .First().Split("=")[1];

            FileName = result
                .Where(s => s.ToLower().Contains("filename"))
                .First().Split("=")[1];
        }
    }
}
