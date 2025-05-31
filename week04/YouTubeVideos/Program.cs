using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Class to represent a comment on a YouTube video
    public class Comment
    {
        private string _commenterName;
        private string _commentText;

        public Comment(string commenterName, string commentText)
        {
            _commenterName = commenterName;
            _commentText = commentText;
        }

        public string GetCommenterName()
        {
            return _commenterName;
        }

        public string GetCommentText()
        {
            return _commentText;
        }
    }

    // Class to represent a YouTube video
    public class Video
    {
        private string _title;
        private string _author;
        private int _lengthInSeconds;
        private List<Comment> _comments;

        public Video(string title, string author, int lengthInSeconds)
        {
            _title = title;
            _author = author;
            _lengthInSeconds = lengthInSeconds;
            _comments = new List<Comment>();
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetAuthor()
        {
            return _author;
        }

        public int GetLength()
        {
            return _lengthInSeconds;
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return _comments.Count;
        }

        public List<Comment> GetComments()
        {
            return _comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to hold videos
            List<Video> videos = new List<Video>();

            // Create videos and add comments
            Video video1 = new Video("Introduction to C#", "Alice Smith", 300);
            video1.AddComment(new Comment("John Doe", "Great introduction!"));
            video1.AddComment(new Comment("Jane Roe", "Very helpful, thanks."));
            video1.AddComment(new Comment("Bob Brown", "Looking forward to more."));
            videos.Add(video1);

            Video video2 = new Video("Advanced C# Techniques", "David Johnson", 600);
            video2.AddComment(new Comment("Emily White", "Really advanced, but well explained."));
            video2.AddComment(new Comment("Chris Green", "I learned a lot from this video."));
            video2.AddComment(new Comment("Kelly Black", "Can you cover async/await next?"));
            videos.Add(video2);

            Video video3 = new Video("C# Design Patterns", "Michael Brown", 900);
            video3.AddComment(new Comment("Sam Blue", "Design patterns are so useful!"));
            video3.AddComment(new Comment("Pat Yellow", "Thanks for the clear examples."));
            video3.AddComment(new Comment("Lee Gray", "Could you do a series on SOLID principles?"));
            videos.Add(video3);

            Video video4 = new Video("C# and YouTube API (Simulation)", "Laura Green", 450);
            video4.AddComment(new Comment("Alex Red", "Looking forward to real API integration."));
            video4.AddComment(new Comment("Taylor Orange", "This is a good simulation."));
            video4.AddComment(new Comment("Jordan Purple", "Helpful for understanding abstraction."));
            videos.Add(video4);

            // Iterate through the list of videos and display details
            foreach (var video in videos)
            {
                Console.WriteLine("Title: " + video.GetTitle());
                Console.WriteLine("Author: " + video.GetAuthor());
                Console.WriteLine("Length (seconds): " + video.GetLength());
                Console.WriteLine("Number of Comments: " + video.GetCommentCount());
                Console.WriteLine("Comments:");

                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($" - {comment.GetCommenterName()}: {comment.GetCommentText()}");
                }

                Console.WriteLine();
            }
        }
    }
}