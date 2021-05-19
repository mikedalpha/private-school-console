using System;
using System.Collections.Generic;

namespace athanasoglou_michael_individual_part_A
{
    class Program
    {
        public static List<Student> Students { get; set; } = new List<Student>();
        public static List<Course> Courses { get; set; } = new List<Course>();
        public static List<Trainer> Trainers { get; set; } = new List<Trainer>();
        public static List<Assignment> Assignments { get; set; } = new List<Assignment>();

        static void Main(string[] args)
        {
            MainMenu();
        }
        

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Main Menu-----");
            Console.WriteLine("Press the corresponding number to access each submenu, or press 12 to exit.");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Students");
            Console.WriteLine("2. Trainers");
            Console.WriteLine("3. Courses");
            Console.WriteLine("4. Assignments");
            Console.WriteLine("\n5. View students per course");
            Console.WriteLine("6. View trainers per course");
            Console.WriteLine("7. View assignments per course");
            Console.WriteLine("8. View assignments per student");
            Console.WriteLine("9. View students that belong to more than one course");
            Console.WriteLine("10. See impending assignments");
            Console.WriteLine("\n11. Use synthetic data");
            Console.WriteLine("\n12. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    StudentsMenu();
                    break;
                case "2":
                    TrainerMenu();
                    break;
                case "3":
                    CoursesMenu();
                    break;
                case "4":
                    AssignmentMenu();
                    break;
                case "5":
                    ViewStudentsPerCourse();
                    break;
                case "6":
                    ViewTrainersPerCourse();
                    break;
                case "7":
                    ViewAssignmentsPerCourse();
                    break;
                case "8":
                    ViewAssignmentsPerStudent();
                    break;
                case "9":
                    StudentMoreThanOneCourse();
                    break;
                case "10":
                    ImpendingAssignments();
                    break;
                case "11":
                    UseSyntheticData();
                    break;
                case "12":
                    break;
                default:
                    Console.WriteLine("\nInvalid option detected!");
                    MainMenu();
                    break;
            }
        }

        private static void UseSyntheticData()
        {
            Students = new List<Student>
            {
                new Student("Michael", "Athanasoglou", Convert.ToDateTime("22/02/1991"), 100M),
                new Student("Konstantinos", "Apostolou", Convert.ToDateTime("05/07/1986"), 250M),
                new Student("Elpida", "Koulianou", Convert.ToDateTime("4/03/1992"), 300M),
            };

            Trainers = new List<Trainer>
            {
                new Trainer("Konstantinos", "Takakis", "Programming"),
                new Trainer("George", "Pasparakis", "Programming")
            };

            Courses = new List<Course>
            {
                new Course("C#", "Programming", "Part-Time", Convert.ToDateTime("15/02/2021"), Convert.ToDateTime("30/09/2021")),
                new Course("Java", "Programming", "Full-Time", Convert.ToDateTime("15/02/2021"), Convert.ToDateTime("15/05/2021"))
            };

            Assignments = new List<Assignment>
            {
                new Assignment("DecimalΤοBinary", "DecimalsToBinary", Convert.ToDateTime("10/03/2021"), 40, 60),
                new Assignment("PrivateSchoolProject", "RepresentationOfASchool", Convert.ToDateTime("24/03/2021"), 40, 60)
            };

            AssignStudentToCourse(1, 1);
            AssignStudentToCourse(1, 2);
            AssignStudentToCourse(2, 1);
            AssignStudentToCourse(2, 2);
            AssignStudentToCourse(3, 1);

            AssignTrainerToCourse(1, 1);
            AssignTrainerToCourse(2, 1);
            AssignTrainerToCourse(2, 2);

            AssignAssignmentToCourse(1, 1);
            AssignAssignmentToCourse(2, 1);
            AssignAssignmentToCourse(2, 2);

            AssignAssignmentToStudent(1, 1);
            AssignAssignmentToStudent(1, 2);
            AssignAssignmentToStudent(1, 3);
            AssignAssignmentToStudent(2, 1);
            AssignAssignmentToStudent(2, 3);

            Console.WriteLine("\nSynthetic Data generated. You can now browse the options in each category, and also add your own.\nPress any key to return to main menu.");
            Console.ReadKey();

            MainMenu();
        }

        static void StudentsMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Students List-----");
            int i = 1;

            foreach (var student in Students)
            {
                Console.WriteLine($"{i}. {student.FirstName} {student.LastName}");
                i++;
            }

            Console.WriteLine("\n1. Add new student(s)");
            Console.WriteLine("2. Assign student to course");
            Console.WriteLine("3. Back");
            switch (Console.ReadLine())
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    StudentToCourse();
                    break;
                case "3":
                    MainMenu();
                    break;
            }
        }

        static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("Please enter a student in the following format: \"< FirstName > < LastName > < DateOfBirth(dd/mm/yyyy) > <tuition fees>\"");
            Console.WriteLine("In case of multiple entries use a comma to separate each student. Example: \"Michael Athanasoglou 22/02/1991 100,John Doe 22/08/1983 200\"");
            
            string[] students = Console.ReadLine().Split(',');
            foreach (string studentInfo in students)
            {
                string[] tokens = studentInfo.Split(' ');
                if (!DateTime.TryParse(tokens[2], out DateTime dob))
                {
                    Console.WriteLine($"\nInvalid dob detected: {tokens[2]}. Needs to be in dd/mm/yyyy format.\nPress any key to return to previous menu.");
                    Console.ReadKey();
                    AddStudent();
                }
                if (!decimal.TryParse(tokens[3], out decimal fee))
                {
                    Console.WriteLine($"\nInvalid tuition fee detected: {tokens[3]}. Needs to be a number.\nPress any key to return to previous menu.");
                    Console.ReadKey();
                    AddStudent();
                }
                Students.Add(new Student(tokens[0], tokens[1], dob, fee));
            }
            StudentsMenu();
        }

        static void StudentToCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Students Lists-----");
            int i = 1;
            foreach (var student in Students)
            {
                Console.WriteLine($"{i}. {student.FirstName} {student.LastName}");
                i++;
            }

            Console.WriteLine("-----Available Courses-----");
            int j = 1;
            foreach (var course in Courses)
            {
                Console.WriteLine($"{j}. {course.Title}");
                j++;
            }

            Console.WriteLine("Select student to assign to course (press number)");
            int studentId;
            while (!int.TryParse(Console.ReadLine(), out studentId) || (studentId > Students.Count) || studentId <= 0)
            {
                Console.WriteLine("\nStudent selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                StudentToCourse();
            }

            Console.WriteLine("\nTo which course would you like to assign the student? (press number)");
            int courseId;
            while (!int.TryParse(Console.ReadLine(), out courseId) || (courseId > Courses.Count) || courseId <= 0)
            {
                Console.WriteLine("\nCourse selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                StudentToCourse();
            }

            AssignStudentToCourse(studentId, courseId);
            StudentsMenu();
        }

        private static void AssignStudentToCourse(int studentId, int courseId)
        {
            Courses[courseId - 1].Students.Add(Students[studentId - 1]); //add Student to Course List
            Students[studentId - 1].Courses.Add(Courses[courseId - 1]); //add Course to Student List
        }

        static void TrainerMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Trainers List-----");
            int i = 1;
            foreach (var trainer in Trainers)
            {
                Console.WriteLine($"{i}. {trainer.FirstName} {trainer.LastName}");
                i++;
            }
            
            Console.WriteLine("\n1. Add new trainer");
            Console.WriteLine("2. Assign trainer to course");
            Console.WriteLine("3. Back");

            switch (Console.ReadLine())
            {
                case "1":
                    AddTrainer();
                    break;
                case "2":
                    TrainerToCourse();
                    break;
                case "3":
                    MainMenu();
                    break;
            }
        }

        static void AddTrainer()
        {
            Console.Clear();
            Console.WriteLine("Please enter a trainer in the following format: \"< FirstName > < LastName > < Subject >\"");
            Console.WriteLine("In case of multiple entries use a comma to separate each trainer. Example: \"Konstantinos Takakis Programming,John Doe Gymnastics\"");

            string[] trainers = Console.ReadLine().Split(',');
            foreach (string trainerInfo in trainers)
            {
                string[] tokens = trainerInfo.Split(' ');
                Trainers.Add(new Trainer(tokens[0], tokens[1], tokens[2]));
            }

            TrainerMenu();
        }

        static void TrainerToCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Trainers List-----");
            int i = 1;
            foreach (var trainer in Trainers)
            {
                Console.WriteLine($"{i}. {trainer.FirstName} {trainer.LastName}");
                i++;
            }

            Console.WriteLine("-----Available Courses-----");
            int j = 1;
            foreach (var course in Courses)
            {
                Console.WriteLine($"{j}. {course.Title}");
                j++;
            }
            
            Console.WriteLine("\nPlease select trainer to assign (press number)");
            int trainerId;
            while (!int.TryParse(Console.ReadLine(), out trainerId) || trainerId > Trainers.Count || trainerId <= 0)
            {
                Console.WriteLine("\nTrainer selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                TrainerToCourse();
            }

            int courseId;
            Console.WriteLine("\nTo which course would you like to assign the trainer? (press number)");
            while (!int.TryParse(Console.ReadLine(), out courseId) || courseId > Courses.Count || courseId <= 0)
            {
                Console.WriteLine("\nCourse selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                TrainerToCourse();
            }
            AssignTrainerToCourse(trainerId, courseId);
            TrainerMenu();
        }

        static void AssignTrainerToCourse(int trainerId, int courseId)
        {
            Courses[courseId - 1].Trainers.Add(Trainers[trainerId - 1]); //add Trainer to Course List
        }

        static void CoursesMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Courses List-----");
            int i = 1;
            foreach (var course in Courses)
            {
                Console.WriteLine($"{i}. {course.Title}");
                i++;
            }
            Console.WriteLine("\n1. Add new course");
            Console.WriteLine("2. Back");
            switch (Console.ReadLine())
            {
                case "1":
                    AddCourse();
                    break;
                case "2":
                    MainMenu();
                    break;
            }
        }

        static void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("Please enter a course in the following format: \"< Title > < Stream > < Type > < StartDate > < EndDate >\"");
            Console.WriteLine("In case of multiple entries use a comma to separate each trainer. Example: \"C# Programming Part-time 15/02/2021 30/9/2021,Java Programming Full-Time 15/02/2021 6/09/2021\"");

            string[] courses = Console.ReadLine().Split(',');
            foreach (string courseInfo in courses)
            {
                string[] tokens = courseInfo.Split(' ');
                Courses.Add(new Course(tokens[0], tokens[1], tokens[2], Convert.ToDateTime(tokens[3]), Convert.ToDateTime(tokens[4])));
            }
            CoursesMenu();
        }

        static void AssignmentMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Assignments List-----");
            int i = 1;
            foreach (var assignment in Assignments)
            {
                Console.WriteLine($"{i}. {assignment.Title}");
                i++;
            }

            Console.WriteLine("\n1. Add new assignment");
            Console.WriteLine("2. Add assignment to course");
            Console.WriteLine("3. Give assignment to student");
            Console.WriteLine("4. Back");
            switch (Console.ReadLine())
            {
                case "1":
                    AddAssignment();
                    break;
                case "2":
                    AssignmentToCourse();
                    break;
                case "3":
                    AssignmentToStudent();
                    break;
                case "4":
                    MainMenu();
                    break;
            }
        }

        static void AddAssignment()
        {
            Console.Clear();
            Console.WriteLine("Please enter an assignment in the following format: \"< Title > < Description > < SubmissionDate > < OralMark > < PassMark >\"");
            Console.WriteLine("In case of multiple entries use a comma to separate each trainer. Example: \"BinaryDecimal BinaryToDecimal 20/02/2021 40 60,CaesarScript CreateScript 20/02/2021 40 60\"");

            string[] assignments = Console.ReadLine().Split(',');
            foreach (string assignmentInfo in assignments)
            {
                string[] tokens = assignmentInfo.Split(' ');
                Assignments.Add(new Assignment(tokens[0], tokens[1], Convert.ToDateTime(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4])));
            }
            AssignmentMenu();
        }

        static void AssignmentToCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Assignments List-----");
            int i = 1;
            foreach (var assignment in Assignments)
            {
                Console.WriteLine($"{i}. {assignment.Title}");
                i++;
            }

            Console.WriteLine("-----Available Courses-----");
            int j = 1;
            foreach (var course in Courses)
            {
                Console.WriteLine($"{j}. {course.Title}");
                j++;
            }

            Console.WriteLine("\nPlease select assignment to add to course (press number)");
            int assignmentId;
            while (!int.TryParse(Console.ReadLine(), out assignmentId) || assignmentId > Assignments.Count || assignmentId <= 0)
            {
                Console.WriteLine("\nAssignment selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                AssignmentToCourse();
            }

            Console.WriteLine("\nTo which course would you like to add the assignment? (press number)");
            int courseId;
            while (!int.TryParse(Console.ReadLine(), out courseId) || courseId > Courses.Count || courseId <= 0)
            {
                Console.WriteLine("\nCourse selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                AssignmentToCourse();
            }
            AssignAssignmentToCourse(assignmentId, courseId);
            AssignmentMenu();
        }

        private static void AssignAssignmentToCourse(int assignmentId, int courseId)
        {
            Courses[courseId - 1].Assignments.Add(Assignments[assignmentId - 1]); //add Assignment to Course List
        }

        static void AssignmentToStudent()
        {
            Console.Clear();
            Console.WriteLine("-----Available Assignments-----");
            int i = 1;
            foreach (var assignment in Assignments)
            {
                Console.WriteLine($"{i}. {assignment.Title}");
                i++;
            }

            Console.WriteLine("-----Students Lists-----");
            int j = 1;
            foreach (var student in Students)
            {
                Console.WriteLine($"{j}. {student.FirstName} {student.LastName}");
                j++;
            }

            Console.WriteLine("\nPlease select assignment (press number)");
            int assignmentId;
            while (!int.TryParse(Console.ReadLine(), out assignmentId) || assignmentId > Assignments.Count || assignmentId <= 0)
            {
                Console.WriteLine("\nAssignment selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                AssignmentToStudent();
            }

            Console.WriteLine("\nTo which student would you like to give the assignment? (press number)");
            int studentId;
            while (!int.TryParse(Console.ReadLine(), out studentId) || studentId > Students.Count || studentId <= 0)
            {
                Console.WriteLine("\nStudent selection out of range. Press any key to return to the previous menu.");
                Console.ReadKey();
                AssignmentToStudent();
            }
            AssignAssignmentToStudent(assignmentId, studentId);
            AssignmentMenu();
        }

        private static void AssignAssignmentToStudent(int assignmentId, int studentId)
        {
            Students[studentId - 1].Assignments.Add(Assignments[assignmentId - 1]); //add Assignment to Student
        }

        static void ViewStudentsPerCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Courses List-----");
            for (int i = 0; i < Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Courses[i].Title}");
            }

            Console.WriteLine("Select course number to view its enrolled students.");
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine("Enrolled students:");
            for (int i = 0; i < Courses[input - 1].Students.Count; i++)
            {
                Console.WriteLine($"{Courses[input - 1].Students[i].FirstName} {Courses[input - 1].Students[i].LastName}");
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void ViewTrainersPerCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Courses List-----");
            for (int i = 0; i < Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Courses[i].Title}");
            }

            Console.WriteLine("Select course number to view its assigned trainers.");
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine("Assigned trainers:");
            for (int i = 0; i < Courses[input - 1].Trainers.Count; i++)
            {
                Console.WriteLine($"{Courses[input - 1].Trainers[i].FirstName} {Courses[input - 1].Trainers[i].LastName}");
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void ViewAssignmentsPerCourse()
        {
            Console.Clear();
            Console.WriteLine("-----Courses List-----");
            for (int i = 0; i < Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Courses[i].Title}");
            }
            Console.WriteLine("Select course number to view its assignments.");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine("Assignments:");
            for (int i = 0; i < Courses[input - 1].Assignments.Count; i++)
            {
                Console.WriteLine($"{Courses[input - 1].Assignments[i].Title}");
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void ViewAssignmentsPerStudent()
        {
            Console.Clear();
            Console.WriteLine("-----Students List-----");
            for (int i = 0; i < Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Students[i].FirstName} {Students[i].LastName}");
            }
            Console.WriteLine("Select student to view his/her assignments.");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine("Assignments:");
            for (int i = 0; i < Students[input - 1].Assignments.Count; i++)
            {
                Console.WriteLine($"{Students[input - 1].Assignments[i].Title}");
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void StudentMoreThanOneCourse()
        {
            Console.Clear();
            Console.WriteLine("List of students who follow more than one course:");
            
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Courses.Count > 1)
                {
                    Console.WriteLine($"{i + 1}. {Students[i].FirstName} {Students[i].LastName}");
                }
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void ImpendingAssignments()
        {
            Console.Clear();
            Console.WriteLine("Please input a date to check impending assignments: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine($"Invalid date: {date}. Press any key to try again.");
                Console.ReadKey();
                ImpendingAssignments();
            }
            DateTime startOfWeek = date;
            switch (date.DayOfWeek) //mark the start of the week by finding the Monday of the input date's week
            {
                case DayOfWeek.Sunday:
                    startOfWeek = date.AddDays(-6);
                    break;
                case DayOfWeek.Saturday:
                    startOfWeek = date.AddDays(-5);
                    break;
                case DayOfWeek.Friday:
                    startOfWeek = date.AddDays(-4);
                    break;
                case DayOfWeek.Thursday:
                    startOfWeek = date.AddDays(-3);
                    break;
                case DayOfWeek.Wednesday:
                    startOfWeek = date.AddDays(-2);
                    break;
                case DayOfWeek.Tuesday:
                    startOfWeek = date.AddDays(-1);
                    break;
                case DayOfWeek.Monday:
                    startOfWeek = date;
                    break;
            }

            DateTime endOfWeek = startOfWeek.AddDays(6); //end of week will always be Monday + 6.

            foreach (var student in Students)
            {
                foreach (var assignment in student.Assignments)
                {
                    if (assignment.SubmissionDate >= startOfWeek && assignment.SubmissionDate <= endOfWeek) //check if submission date is in input week
                    {
                        Console.WriteLine($"Assignment \"{assignment.Title}\" for {student.FirstName} {student.LastName} due on {assignment.SubmissionDate}");
                    }
                    else
                    {
                        Console.WriteLine($"Assignment \"{assignment.Title}\" for {student.FirstName} {student.LastName} not due on input week.");
                    }
                }
            }
            Console.WriteLine("\nPress any key to return to the previous menu.");
            Console.ReadKey();
            MainMenu();
        }
    }
}
