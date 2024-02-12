using NewGlobeTest.Classes;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        int n;
        int useTopicsFactory = 1;
        List<Topic> topicList = new List<Topic>();

        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. Do you want to generate a large amount of topics randonmly");
        Console.WriteLine("2. Do you want to generate a small custom amount of topics");

        do
        {
            Int32.TryParse(Console.ReadLine(), out useTopicsFactory);
            if (useTopicsFactory > 2 || useTopicsFactory < 1)
                Console.WriteLine("Please input a valid option");

        } while (useTopicsFactory > 2 || useTopicsFactory < 1);

        if (useTopicsFactory == 1)
        {
            Console.WriteLine("How many Topics do you want generated?");

            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid input, please use integers only");
                Console.WriteLine("How many Topics do you want generated?");
            }

            TopicsFactory tp = new TopicsFactory(n);
            topicList = tp.topics;
        } else
        {
            Console.WriteLine("How many topics do you want to manually generate");
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid input, please use integers only");
                Console.WriteLine("How many Topics do you want to manually generate?");
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"What is the title of the topic number {i+1}:");
                string topicTitle = Console.ReadLine();

                Console.WriteLine($"What is the duration of the topic number {n}:");
                Console.WriteLine("Please input a number between the following options");
                Console.WriteLine("1. 60 minutes");
                Console.WriteLine("2. 45 minutes");
                Console.WriteLine("3. 30 minutes");
                Console.WriteLine("4. 15 minutes");
                Console.WriteLine("5. Lightning");

                int topicDuration;
                do
                {
                    Int32.TryParse(Console.ReadLine(), out topicDuration);
                    if (topicDuration < 1 || topicDuration > 5)
                        Console.WriteLine("Please input a valid option");

                } while (topicDuration < 1 || topicDuration > 5);

                switch (topicDuration) {
                    case 1:
                        topicDuration = 60;
                        break; 
                    case 2:
                        topicDuration = 45;
                        break; 
                    case 3:
                        topicDuration = 30;
                        break; 
                    case 4:
                        topicDuration = 15;
                        break; 
                    case 5:
                        topicDuration = 5;
                        break;
                }

                topicList.Add(new Topic(topicTitle, topicDuration));
            }
        }

        TracksScheduler ts = new TracksScheduler(topicList);

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
