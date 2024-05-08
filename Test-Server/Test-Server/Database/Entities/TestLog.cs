using System.ComponentModel.DataAnnotations;

namespace Test_Server.Database.Entities
{
    public class TestLog
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
