using NewGlobeTest.Classes;

class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("How many Topics do you want generated?");

        while (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.Clear();
            Console.WriteLine("You entered an invalid input, please use integers only");
            Console.WriteLine("How many Topics do you want generated?");
        }

        TopicsFactory tp = new TopicsFactory(n);
        TracksScheduler ts = new TracksScheduler(tp.topics);

        PrintSchedule();

        void PrintSchedule()
        {
            Console.WriteLine("Conference Tracks Schedule:");
            for (int i = 0; i < ts.tracks.Count; i++)
            {
                Console.WriteLine($"Track {i + 1}:");
                foreach (var topic in ts.tracks[i].topicList)
                {
                    if(topic._duration == 5)
                    {
                        Console.WriteLine($"{topic._startTime.Value.ToString("HH:mm")} | {topic._title} | lightning");
                    } else
                    {
                        Console.WriteLine($"{topic._startTime.Value.ToString("HH:mm")} | {topic._title} | {topic._duration}min");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
