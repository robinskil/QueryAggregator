using System;

namespace UnitTests
{
    public class Grade
    {
        public Guid GradeId { get; set; }
        public Guid StudentId { get; set; }
        public decimal Points { get; set; }
        public Student Student { get; set; }
    }
}