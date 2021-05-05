using System;
namespace BDDExampleDomain
{
    public class Qualification
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Created { get; init; } = DateTime.Now;
        public DateTime Awarded { get; set; }
    }
}
