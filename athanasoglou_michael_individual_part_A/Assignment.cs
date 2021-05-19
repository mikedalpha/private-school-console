using System;

namespace athanasoglou_michael_individual_part_A
{
    public class Assignment
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int OralMark { get; set; }
        public int PassMark { get; set; }

        public Assignment(string title, string description, DateTime submissionDate, int oralMark, int passMark)
        {
            Title = title;
            Description = description;
            SubmissionDate = submissionDate;
            OralMark = oralMark;
            PassMark = passMark;
        }
    }
}
