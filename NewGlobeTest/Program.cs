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


        //public void PrintSchedule()
        //{
        //    Console.WriteLine("Elective Training Day Schedule:");
        //    for (int i = 0; i < Tracks.Count; i++)
        //    {
        //        Console.WriteLine($"Track {i + 1}:");
        //        foreach (var talk in Tracks[i].Talks)
        //        {
        //            Console.WriteLine($"- {talk.Title} (Start: {talk.StartTime.ToString("HH:mm")}, Duration: {talk.Duration} minutes)");
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }

    // OLD STUFF
    //static void Main(string[] args)
    //{
    //    // Define available talk titles and lengths
    //    string[] talkTitles = { "Teaching Techniques", "Policy Updates", "Tech Innovations", "Effective Communication", "Team Collaboration" };
    //    int[] talkLengths = { 30, 45, 60, 5 }; // in minutes, including 'lightning' talk

    //    // Create morning and afternoon tracks
    //    Track morningTrack = new Track();
    //    Track afternoonTrack = new Track();

    //    // Schedule morning talks
    //    DateTime morningStartTime = new DateTime(2024, 2, 7, 9, 0, 0); // Start at 9 AM
    //    morningStartTime = ScheduleTalks(morningTrack, talkTitles, talkLengths, morningStartTime);

    //    // Add lunch break
    //    AddLunchBreak(morningTrack, morningStartTime);

    //    // Schedule afternoon talks
    //    DateTime afternoonStartTime = new DateTime(2024, 2, 7, 13, 0, 0); // Start at 1 PM
    //    afternoonStartTime = ScheduleTalks(afternoonTrack, talkTitles, talkLengths, afternoonStartTime);

    //    // Add sharing session
    //    AddSharingSession(afternoonTrack, afternoonStartTime);

    //    // Create schedule and add tracks
    //    Schedule schedule = new Schedule();
    //    schedule.AddTrack(morningTrack);
    //    schedule.AddTrack(afternoonTrack);

    //    // Print schedule
    //    schedule.PrintSchedule();
    //}
}
