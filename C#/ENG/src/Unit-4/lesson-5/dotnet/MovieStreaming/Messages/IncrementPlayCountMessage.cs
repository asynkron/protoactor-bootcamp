using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreaming.Messages
{
    public class IncrementPlayCountMessage
    {
        public string MovieTitle { get; }

        public IncrementPlayCountMessage(string movieTitle)
        {
            MovieTitle = movieTitle;
        }
    }
}
