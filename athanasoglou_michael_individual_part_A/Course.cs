using System;
using System.Collections.Generic;

namespace athanasoglou_michael_individual_part_A
{
    public class Course
    {
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Student> Students = new List<Student>();

        public List<Trainer> Trainers = new List<Trainer>();

        public List<Assignment> Assignments = new List<Assignment>();

        public Course(string title, string stream, string type, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Stream = stream;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
