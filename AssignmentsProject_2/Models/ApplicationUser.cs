using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AssignmentsProject_2.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        //public int Id { get; set; }
        //  [Required]
       /* public string FirstName { get; set; }
        //  [Required]
        public string LastName { get; set; }
        //   [Required]
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<Assignment> assignments { get; set; } = new List<Assignment>();*/
    }
}
