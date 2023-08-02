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
        public bool IsOpen { get; set; } = true;
        public bool IsCompleted { get; set; } = false;
        public bool InProgress { get; set; } = false;
        public int Number = 0;

        public Assignment () { }

        public Assignment(string title, string description, DateTime started, DateTime finish, bool isOpen, bool isCompleted, bool inProgress)
        {
            this.Title = title;
            this.Description = description;
            this.Started = started;
            this.Finish = finish;
            this.IsOpen = isOpen;
            this.IsCompleted = isCompleted;
            this.InProgress = inProgress;
        }

        public Assignment(Assignment a)
        {
            this.Title = a.Title;
            this.Description = a.Description;
            this.Started = a.Started;
            this.Finish = a.Finish;
            this.IsCompleted = a.IsCompleted;
            this.InProgress = a.InProgress;
            this.IsCompleted = a.IsCompleted;
            this.InProgress = a.InProgress;
        }

        public static bool Compare (Assignment a1, Assignment a2)
        {
            if (a1 == null || a2 == null)
            {
                return false;
            }
            if (
                !a1._id.Equals(a2._id))
            {
                return false;
            }
            if (!a1.Title.Equals(a2.Title)) return false;
                if (!a1.Description.Equals(a2.Description)) return false;
               if (!a1.Started.Equals(a2.Started)) return false;
            if (!a1.Finish.Equals(a2.Finish)) return false;
            if (a1.IsOpen != a2.IsOpen) return false;
                if (a1.InProgress != a2.InProgress) return false;
            if (a1.IsCompleted != a2.IsCompleted) return false;
                
         
          
                return true; 
        }
    }
}
