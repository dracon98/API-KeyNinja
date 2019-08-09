using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;

namespace API_KeyNinja.Models
{
    public class Rider
    {

        [JsonProperty("firstname")]
        public string firstname { get; set; }
        [JsonProperty("lastname")]
        public string lastname { get; set; }
        [JsonProperty("phoneNumber")]
        public string phone_number { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("startDate")]
        public string start_date { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        public int id { get; set; }

        public Rider() { }

        public Rider(string firstname, string lastname, string phone_number, string email, string start_date, string status)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.phone_number = phone_number;
            this.email = email;
            this.start_date = parseDate(start_date);
            this.status = status;
        }

        public Rider(string status)
        {
            this.status = status;
        }

        public string parseDate(string date)
        {
            DateTime dateNow = Convert.ToDateTime(date);
            return dateNow.ToString("dd/MM/yyyy");
        }
    }
}