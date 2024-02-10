namespace NewGlobeTest.Classes
{
    internal class Topic : ITopic
    {
        public string _title { get; set; }
        public int _duration { get; set; }
        public DateTime? _startTime { get; set; }

        public Topic(string title, int duration)
        {
            _title = title;
            _duration = duration;
        }

        public Topic(string title, int duration, DateTime startTime) {
            _title = title;
            _duration = duration;
            _startTime = startTime;
        }
    }
}
