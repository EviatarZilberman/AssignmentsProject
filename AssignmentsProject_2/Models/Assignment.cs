using AssignmentsProject_2.Controllers;
using MongoDB.Bson;
using Utilities;

namespace AssignmentsProject_2.Models
{
    public class Assignment
    {
        public Guid _id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Started { get; set; } = DateTime.Now;
        public DateTime Finish { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Number = 0;

        public Assignment() { }

        public Assignment(string title, string description, DateTime started, DateTime finish, string status)
        {
            this.Title = title;
            this.Description = description;
            this.Started = started;
            this.Finish = finish;
            this.Status = status;
        }

        public Assignment(Assignment a)
        {
            this.Title = a.Title;
            this.Description = a.Description;
            this.Started = a.Started;
            this.Finish = a.Finish;
            this.Status=a.Status;
        }
    }
}
