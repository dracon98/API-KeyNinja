using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_KeyNinja.Models
{
    public class Jobs
    {
        public DateTime date_time { get; set; }
        public int rider_id { get; set; }
        public decimal review_score { get; set; }
        public string review_comment { get; set; }
        public string completed_at { get; set; }

        public Jobs() { }

        public Jobs(DateTime date_time, int rider_id,
            decimal review_score, string review_comment, string completed_at)
        {
            this.date_time = date_time;
            this.rider_id = rider_id;
            this.review_score = review_score;
            this.review_comment = review_comment;
            this.completed_at = completed_at;
        }
    }
}