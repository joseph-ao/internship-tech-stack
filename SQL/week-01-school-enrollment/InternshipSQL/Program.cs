using Microsoft.Data.SqlClient;

// Connection string — adjust Server name if needed
string connectionString = "Server=localhost;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;";

while (true)
{
    Console.WriteLine("\n========== School Enrollment Manager ==========");
    Console.WriteLine("1.  Courses by major");
    Console.WriteLine("2.  Students enrolled in a course");
    Console.WriteLine("3.  Students in a major (ordered by name)");
    Console.WriteLine("4.  Students who passed a course");
    Console.WriteLine("5.  Students who failed a course");
    Console.WriteLine("6.  Distinct grades for a course");
    Console.WriteLine("7.  Count of students who passed a course");
    Console.WriteLine("8.  Students with no grade");
    Console.WriteLine("9.  Set grade to 50 for grades between 47-49");
    Console.WriteLine("10. Delete students with grades below 30");
    Console.WriteLine("11. Calculate and save average grades");
    Console.WriteLine("0.  Exit");
    Console.Write("\nChoose an option: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter major name: ");
            string major1 = Console.ReadLine();
            GetCoursesByMajor(connectionString, major1);
            break;
        case "2":
            Console.Write("Enter course name: ");
            string course2 = Console.ReadLine();
            GetStudentsByCourse(connectionString, course2);
            break;
        case "3":
            Console.Write("Enter major name: ");
            string major3 = Console.ReadLine();
            GetStudentsByMajorOrdered(connectionString, major3);
            break;
        case "4":
            Console.Write("Enter course name: ");
            string course4 = Console.ReadLine();
            GetPassedStudents(connectionString, course4);
            break;
        case "5":
            Console.Write("Enter course name: ");
            string course5 = Console.ReadLine();
            GetFailedStudents(connectionString, course5);
            break;
        case "6":
            Console.Write("Enter course name: ");
            string course6 = Console.ReadLine();
            GetDistinctGrades(connectionString, course6);
            break;
        case "7":
            Console.Write("Enter course name: ");
            string course7 = Console.ReadLine();
            CountPassedStudents(connectionString, course7);
            break;
        case "8":
            GetStudentsWithNoGrade(connectionString);
            break;
        case "9":
            SetGradeToFifty(connectionString);
            break;
        case "10":
            DeleteLowGradeStudents(connectionString);
            break;
        case "11":
            SaveAverageGrades(connectionString);
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

// ===================== METHODS =====================

static void GetCoursesByMajor(string connStr, string majorName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT c.CourseName 
                             FROM Courses c
                             INNER JOIN Majors m ON c.MajorID = m.MajorID
                             WHERE m.MajorName = @major";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@major", majorName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nCourses for {majorName}:");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["CourseName"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetStudentsByCourse(string connStr, string courseName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT s.FirstName, s.LastName
                             FROM Students s
                             INNER JOIN Enrollments e ON s.StudentID = e.StudentID
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE c.CourseName = @course";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@course", courseName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nStudents in {courseName}:");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["FirstName"]} {reader["LastName"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetStudentsByMajorOrdered(string connStr, string majorName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT s.FirstName, s.LastName
                             FROM Students s
                             INNER JOIN Majors m ON s.MajorID = m.MajorID
                             WHERE m.MajorName = @major
                             ORDER BY s.LastName ASC, s.FirstName ASC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@major", majorName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nStudents in {majorName} (ordered):");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["FirstName"]} {reader["LastName"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetPassedStudents(string connStr, string courseName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT s.FirstName, s.LastName, e.Grade
                             FROM Students s
                             INNER JOIN Enrollments e ON s.StudentID = e.StudentID
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE c.CourseName = @course AND e.Grade >= 50";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@course", courseName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nStudents who PASSED {courseName}:");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["FirstName"]} {reader["LastName"]} | Grade: {reader["Grade"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetFailedStudents(string connStr, string courseName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT s.FirstName, s.LastName, e.Grade
                             FROM Students s
                             INNER JOIN Enrollments e ON s.StudentID = e.StudentID
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE c.CourseName = @course AND e.Grade < 50";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@course", courseName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nStudents who FAILED {courseName}:");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["FirstName"]} {reader["LastName"]} | Grade: {reader["Grade"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetDistinctGrades(string connStr, string courseName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT DISTINCT e.Grade
                             FROM Enrollments e
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE c.CourseName = @course AND e.Grade IS NOT NULL";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@course", courseName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nDistinct grades for {courseName}:");
                    while (reader.Read())
                        Console.WriteLine($"  - {reader["Grade"]}");
                }
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void CountPassedStudents(string connStr, string courseName)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT COUNT(*) 
                             FROM Enrollments e
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE c.CourseName = @course AND e.Grade >= 50";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@course", courseName);
                int count = (int)cmd.ExecuteScalar();
                Console.WriteLine($"\nStudents who passed {courseName}: {count}");
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void GetStudentsWithNoGrade(string connStr)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT s.FirstName, s.LastName, c.CourseName
                             FROM Students s
                             INNER JOIN Enrollments e ON s.StudentID = e.StudentID
                             INNER JOIN Courses c ON e.CourseID = c.CourseID
                             WHERE e.Grade IS NULL";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("\nStudents with no grade:");
                while (reader.Read())
                    Console.WriteLine($"  - {reader["FirstName"]} {reader["LastName"]} | Course: {reader["CourseName"]}");
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void SetGradeToFifty(string connStr)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = "UPDATE Enrollments SET Grade = 50 WHERE Grade BETWEEN 47 AND 49";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"\nUpdated {rows} student(s) grade to 50.");
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void DeleteLowGradeStudents(string connStr)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string deleteEnrollments = @"DELETE FROM Enrollments
                                         WHERE StudentID IN (
                                             SELECT DISTINCT StudentID 
                                             FROM Enrollments 
                                             WHERE Grade < 30
                                         )";

            using (SqlCommand cmd = new SqlCommand(deleteEnrollments, conn))
            {
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"Deleted {rows} enrollment record(s).");
            }

            string deleteStudents = @"DELETE FROM Students
                                      WHERE StudentID NOT IN (
                                          SELECT DISTINCT StudentID FROM Enrollments
                                      )";

            using (SqlCommand cmd2 = new SqlCommand(deleteStudents, conn))
            {
                int rows = cmd2.ExecuteNonQuery();
                Console.WriteLine($"Deleted {rows} student(s) with grades below 30.");
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

static void SaveAverageGrades(string connStr)
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"UPDATE Students
                             SET AverageGrade = (
                                 SELECT AVG(Grade)
                                 FROM Enrollments
                                 WHERE Enrollments.StudentID = Students.StudentID
                                 AND Grade IS NOT NULL
                             )";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"\nAverage grades calculated and saved for {rows} student(s).");
            }
        }
    }
    catch (SqlException ex) { Console.WriteLine($"Error: {ex.Message}"); }
}