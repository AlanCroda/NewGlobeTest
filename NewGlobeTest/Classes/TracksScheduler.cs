using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGlobeTest.Classes
{
    internal class TracksScheduler
    {
        // All minutes per day are calculated without the minutes for needed for Lunch and Sharing session.
        private const int minMinutesPerDay = 360;
        private const int maxMinutesPerDay = 420;
        private int allTopicsDuration = 0;
        public List<Track> tracks = new List<Track>();

        public TracksScheduler(List<Topic> topics) {
            foreach (var topic in topics)
            {
                allTopicsDuration += topic._duration;
            }

            float numOfTracks = allTopicsDuration / maxMinutesPerDay;

            for (int i = 0; i < numOfTracks; i++)
            {
                tracks.Add(ScheduleTrack(topics));
            }
        }

        private Track ScheduleTrack(List<Topic> topics)
        {
            Track track = new Track();
            int currentMinutes = 0;

            track.topicList.Add(AddLunchBreak());

            foreach (var topic in topics)
            {
                // Fill track while no precision is needed
                while (currentMinutes < minMinutesPerDay) {
                    track.topicList.Add(topic);
                    topics.Remove(topic);
                    currentMinutes += topic._duration;
                }

                // Now we need precision to insert Topics into Track
                // TODO: HOW TO INSERT EFFICIENTLY
                while (currentMinutes < maxMinutesPerDay)
                {

                }

                return track;
            }

            track.topicList.Add(AddSharingSession(track));

            return track;
        }

        static Topic AddLunchBreak()
        {
            Topic lunchTalk = new Topic("Lunch", 60, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0));

            return lunchTalk;
        }

        static Topic AddSharingSession(Track track)
        {
            // Find the end time of the last activity before the Sharing Session
            DateTime lastActivityEndTime = track.topicList.Max(t => t._startTime.Value);

            //// Ensure Sharing Session starts after the end of the last activity
            //DateTime sharingSessionStart = lastActivityEndTime.Date.AddHours(16); // 4 PM
            //if (sharingSessionStart < lastActivityEndTime)
            //    sharingSessionStart = lastActivityEndTime;

            Topic sharingSession = new Topic("Sharing Session", 30, lastActivityEndTime);

            return sharingSession;
        }
    }
}
