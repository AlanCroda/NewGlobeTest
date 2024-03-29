﻿namespace NewGlobeTest.Classes
{
    internal class TracksScheduler
    {
        public List<Track> tracks = new List<Track>();
        public List<Topic> topics = new List<Topic>();

        int currentMinutes = 0;
        bool shouldCheckForLunch = true;

        private const int minutesTillLunch = 180;
        private const int minutesTillShareSession = 480;

        private Random rand = new Random();

        public TracksScheduler(List<Topic> topics) {
            this.topics = topics;

            while (topics.Count > 0)
            {
                tracks.Add(ScheduleTrack(topics));
            }
        }

        private Track ScheduleTrack(List<Topic> topics)
        {
            Track track = new Track();
            currentMinutes = 0;
            shouldCheckForLunch = true;

            track.topicList.Add(AddLunchBreak());

            while (currentMinutes < minutesTillShareSession && topics.Count > 0)
            {
                if(topics.Count == 0)
                {
                    track.topicList.Add(AddSharingSession(track));
                    return track;
                }

                if(currentMinutes >= 180 && shouldCheckForLunch)
                {
                    // Skip Lunchtime for scheduling since it's already scheduled
                    currentMinutes += 60;
                    shouldCheckForLunch = false;
                }

                // As long as there is space for it, check if need to insert topics before lunch
                if(currentMinutes + 5 <= minutesTillLunch) // HANDLE PRECISE TOPIC INSERTION WHEN APPROACHING LUNCH
                {
                    if (currentMinutes + 60 > minutesTillLunch && currentMinutes + 45 <= minutesTillLunch)
                    {
                        if((from item in topics where item._duration < 60 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(60));
                        } else
                        {
                            currentMinutes = minutesTillLunch + 60;
                            shouldCheckForLunch = false;
                        }
                    }
                    else if (currentMinutes + 45 > minutesTillLunch && currentMinutes + 30 <= minutesTillLunch)
                    {
                        if((from item in topics where item._duration < 45 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(45));
                        } else
                        {
                            currentMinutes = minutesTillLunch + 60;
                            shouldCheckForLunch = false;
                        }
                    }
                    else if (currentMinutes + 30 > minutesTillLunch && currentMinutes + 15 <= minutesTillLunch)
                    {
                        if ((from item in topics where item._duration < 30 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(30));
                        }
                        else
                        {
                            currentMinutes = minutesTillLunch + 60;
                            shouldCheckForLunch = false;
                        }
                    } else if (currentMinutes + 15 > minutesTillLunch && currentMinutes + 5 <= minutesTillLunch)
                    {
                        if ((from item in topics where item._duration < 15 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(15));
                        }
                        else
                        {
                            currentMinutes = minutesTillLunch + 60;
                            shouldCheckForLunch = false;
                        }
                    } else
                    {
                        // Normal insertion
                        track.topicList.Add(InsertTopic());
                    }
                } else // HANDLE PRECISE TOPIC INSERTION WHEN APPROACHING SHARING SESSION
                {
                    if (currentMinutes + 60 > minutesTillShareSession && currentMinutes + 45 <= minutesTillShareSession)
                    {
                        if ((from item in topics where item._duration < 60 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(60));
                        } else
                        {
                            currentMinutes += 30;
                        }
                    } else if (currentMinutes + 45 > minutesTillShareSession && currentMinutes + 30 <= minutesTillShareSession)
                    {
                        if ((from item in topics where item._duration < 45 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(45));
                        }
                        else
                        {
                            currentMinutes += 45;
                        }
                    }
                    else if (currentMinutes + 30 > minutesTillShareSession && currentMinutes + 15 <= minutesTillShareSession)
                    {
                        if ((from item in topics where item._duration < 30 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(30));
                        } else
                        {
                            currentMinutes += 15;
                        }
                    }
                    else if (currentMinutes + 15 > minutesTillShareSession && currentMinutes + 5 <= minutesTillShareSession)
                    {
                        if ((from item in topics where item._duration < 15 select item).ToList().Count > 0)
                        {
                            track.topicList.Add(InsertLessThanMins(15));
                        } else
                        {
                            currentMinutes += 5;
                        }
                    }
                    else
                    {
                        if(currentMinutes < 480)
                        {
                            // Normal insertion
                            track.topicList.Add(InsertTopic());
                        }
                    }
                }
            }

            track.topicList.Add(AddSharingSession(track));

            // My solution involves adding Lunch as first item in the track, which makes it so that I need to sort the track once it's full, which is a bit inefficient.
            // Could improve this in the future.
            track.topicList = track.topicList.OrderBy(x => x._startTime).ToList();

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
            Topic lastTopicInTrack = track.topicList.MaxBy(t => t._startTime);

            // Check that last activity isn't lunch, otherwise add sharing session after lunch
            if (lastTopicInTrack._startTime.Value.Hour != 12)
            {
                Topic sharingSession = new Topic("Sharing Session", 30, (DateTime)lastTopicInTrack._startTime.Value.AddMinutes(lastTopicInTrack._duration));
                return sharingSession;
            }
            else
            {
                //lastTopicInTrack = lastTopicInTrack._startTime.Value.AddMinutes(60);
                Topic sharingSession = new Topic("Sharing Session", 30, (DateTime)lastTopicInTrack._startTime.Value.AddMinutes(lastTopicInTrack._duration));
                return sharingSession;
            }
        }

        private Topic InsertTopic()
        {
            Topic topic = topics[rand.Next(topics.Count)];
            
            topic._startTime = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            topic._startTime = topic._startTime.Value.AddMinutes(currentMinutes);

            //Increase currentMinutes by topic duration
            currentMinutes += topic._duration;

            //Remove topic from the list of Topics since it's scheduled
            topics.Remove(topic);

            return topic;
        }

        private Topic InsertLessThanMins(int x)
        {
            // Insert only less than x mins Topics to not collide with Lunch session
            var customTopics = (from item in topics where item._duration < x select item).ToList();
            Topic topic = customTopics[rand.Next(customTopics.Count)];

            topic._startTime = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            topic._startTime = topic._startTime.Value.AddMinutes(currentMinutes);

            //Increase currentMinutes by topic duration
            currentMinutes += topic._duration;

            //Remove topic from the list of Topics since it's scheduled
            topics.Remove(topic);

            return topic;
        }
    }
}
