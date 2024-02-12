namespace NewGlobeTest.Classes
{
    internal class TopicsFactory
    {
        private List<string> topicTitles = [
            "Teaching Techniques", 
            "Policy Updates", 
            "Tech Innovations", 
            "Effective Communication", 
            "Team Collaboration", 
            "Organising Parents for Academy Improvements",
            "Teaching Innovations in the Pipeline",
            "Teacher Computer Hacks",
            "Making Your Academy Beautiful",
            "Academy Tech Field Repair",
            "Sync Hard",
            "Unusual Recruiting",
            "Parent Teacher Conferences",
            "Managing Your Dire Allowance",
            "Customer Care",
            "AIMs – 'Managing Up'",
            "Dealing with Problem Teachers",
            "Hiring the Right Cook",
            "Government Policy Changes and New Globe",
            "Adjusting to Relocation",
            "Public Works in Your Community",
            "Talking To Parents About Billing",
            "So They Say You're a Devil Worshipper",
            "Two-Streams or Not Two-Streams",
            "Piped Water"
            ];
        private List<int> topicDurations = [30, 45, 60, 5]; // in minutes
        public List<Topic> topics = new List<Topic>();

        public TopicsFactory(int numOfTopics) {
            topics = GenerateTopics(numOfTopics);
        }

        private List<Topic> GenerateTopics(int n)
        {
            while(topics.Count < n)
            {
                Random rand = new Random();
                int randomTitleIndex = rand.Next(topicTitles.Count);
                int randomDurationIndex = rand.Next(topicDurations.Count);

                string title = topicTitles[randomTitleIndex];
                //topicTitles.RemoveAt(randomTitleIndex); //Could make it so titles don't repeat themselves if I had a bigger "topicTitles" data or a max constraint on the number of topics that can be generated

                int duration = topicDurations[randomDurationIndex];

                Topic topic = new Topic(title, duration);
                topics.Add(topic);
            }

            return topics;
        }
    }
}
