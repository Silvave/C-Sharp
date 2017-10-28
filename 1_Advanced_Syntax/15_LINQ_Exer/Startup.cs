namespace _15_LINQ_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.IO;
    using OfficeOpenXml;

    public class Startup
    {
        public static void Main()
        {
            //List<string[]> studentsInfo = GetStudentsInfo();
            //E1_StudentsByGroups(studentsInfo);
            //E2_StudentByFirstandLastNames(studentsInfo);
            //E3_StudentByAge(studentsInfo);
            //E4_SortStudents(studentsInfo);
            //E5_FilterStudentsByEmailD(studentsInfo);
            //E6_FilterStudentsByPhone(studentsInfo);
            //E7_ExelentStudent(studentsInfo);
            //E8_WeakStudents(studentsInfo);
            //E9_StudentEnrolledInYears(studentsInfo);
            //E10_GroupByGroup(studentsInfo);
            //E11_StudentsJoinedToSpecialties(studentsInfo);
            //E12_LittleJohn();
            //E12_LittleJohnSecondSolution();
            //E13_OfficeStuff();
            E14_ExportToExcel();
            //InsaneTesting();
        }

        public static void InsaneTesting()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.WriteLine($"Ackerman({i},{j}) is: {Ackerman(i,j)}");
                }
            }
        }

        public static int Ackerman(int n, int m)
        {
            int answear;

            if (n == 0) answear = m + 1;
            else if (m == 0) answear = Ackerman(n - 1, 1);
            else answear = Ackerman(n - 1, Ackerman(n, m - 1));

            return answear;
        }

        public static void E14_ExportToExcel()
        {
            FileInfo inputStudentDataFile = new FileInfo("../../StudentData.txt");

            List<string[]> text = new List<string[]>();

            using (StreamReader reader = inputStudentDataFile.OpenText())
            {
                string inputRow;
                while (!string.IsNullOrEmpty(inputRow = reader.ReadLine()))
                {
                    string[] separateRow = inputRow.Split('\t');

                    text.Add(separateRow);
                }
            }

            // Console.WriteLine(string.Join("\n", text.Select(row => string.Join(", ", row))));

            DirectoryInfo outputDir = new DirectoryInfo(@"C:\Users\Silvave91\Downloads\SoftUni\C#\1_Fundametals\1_Advanced\CSharp-exer-solutions\15_LINQ_Exer\");

            string file = outputDir.FullName + "studentSheet.xlsx";

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            FileInfo newFile = new FileInfo(file);

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                var wsData = pck.Workbook.Worksheets.Add("data");


                // TODO: 
            }
        }

        public static void E13_OfficeStuff()
        {
            int numInputRows = int.Parse(Console.ReadLine());

            List<string[]> companiesProducts = new List<string[]>();

            for (int i = 0; i < numInputRows; i++)
            {
                string[] inputRow = Console.ReadLine().Split(new[] { ' ', '-', '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                companiesProducts.Add(inputRow);
            }

            // Blaaaaaaaaaa - DO NOT DO THIS AGAIN!!!!
            companiesProducts
                .GroupBy(arr => arr[0], arr => arr.Skip(1).ToArray())
                .OrderBy(g => g.Key)
                .Select(g => $@"{g.Key}: {g.ToList()
                                            .GroupBy(arr => arr[1], arr => arr[0])
                                            .Select(gg =>
                                                $"{gg.Key}-{gg.ToList().Select(long.Parse).Sum()}")
                                            .Aggregate(new StringBuilder(),
                                                        (sb, s) => sb.Append($"{s}, "))
                                            .ToString()
                                            .TrimEnd(',', ' ')}")
                .ToList()
                .ForEach(Console.WriteLine);
        }

        public static void E12_LittleJohnSecondSolution()
        {
            // Regex priority
            // Bigger matches (arrows) have bigger priority to smaller ones
            Regex arrowsRegex = new Regex(@"(>-{5}>)|(>>-{5}>)|(>>>-{5}>>)");

            long[] arrowsCountList = new long[3];

            for (int i = 0; i < 4; i++)
            {
                string inputStr = Console.ReadLine();

                foreach (Match match in arrowsRegex.Matches(inputStr))
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        if (match.Groups[j].Success)
                        {
                            arrowsCountList[j-1]++;
                        }
                    }
                }
            }

            string str = string.Join("", arrowsCountList);

            string binaryStr = Convert.ToString(long.Parse(str), 2);
            string secretBinaryStr = binaryStr + string.Join("", binaryStr.Reverse());

            Console.WriteLine(Convert.ToInt64(secretBinaryStr, 2));
        }

        public static void E12_LittleJohn()
        {
            Regex smallArrowRegex = new Regex(@">[-]{5}>");
            Regex mediumArrowRegex = new Regex(@">>[-]{5}>");
            Regex largeArrowRegex = new Regex(@">>>[-]{5}>>");

            Dictionary<Regex, long> arrowInfoDict = new Dictionary<Regex, long>()
            {
                { largeArrowRegex, 0 },
                { mediumArrowRegex, 0 },
                { smallArrowRegex, 0 }
            };

            for (int i = 0; i < 4; i++)
            {
                string inputStr = Console.ReadLine();

                arrowInfoDict.Keys.ToList().ForEach(regexKey =>
                {
                    Match match = regexKey.Match(inputStr);

                    while (match.Success)
                    {
                        arrowInfoDict[regexKey]++;
                        inputStr = regexKey.Replace(inputStr, "*", 1);

                        match = regexKey.Match(inputStr);
                    }
                });
            }

            string str = arrowInfoDict
                            .Reverse()
                            .Aggregate(new StringBuilder(),
                                    (sb, kvp) => sb.Append(kvp.Value))
                            .ToString();

            string binaryStr = Convert.ToString(long.Parse(str), 2);
            string secretBinaryStr = binaryStr + string.Join("", binaryStr.Reverse());

            Console.WriteLine(Convert.ToInt64(secretBinaryStr, 2));
        }

        public static void E11_StudentsJoinedToSpecialties(List<string[]> studentsAndSpecialtiesList)
        {
            List<Specialty> specialties = studentsAndSpecialtiesList
                                    .TakeWhile(e => e[0] != "Students:")
                                    .Select(e => new Specialty($"{e[0]} {e[1]}", e[2]))
                                    .ToList();

            //List<Student> students = studentsAndSpecialtiesList
            //                        .SkipWhile(e => e[0] != "Students:")
            //                        .Skip(1)
            //                        .Select(e => new Student($"{e[1]} {e[2]}", e[0]))
            //                        .ToList();

            //students.Join(specialties,
            //              student => student.FacultyNumber,
            //              specialty => specialty.FacultyNumber,
            //              (student, specialty) => new
            //              {
            //                  StudentName = student.Name,
            //                  FacultyNumber = student.FacultyNumber,
            //                  Specialty = specialty.Name
            //              })
            //        .OrderBy(s => s.StudentName)
            //        .ToList()
            //        .ForEach(s => Console.WriteLine($"{s.StudentName} {s.FacultyNumber} {s.Specialty}"));

            studentsAndSpecialtiesList
                        .SkipWhile(e => e[0] != "Students:")
                        .Skip(1)
                        .Join(specialties,
                              stInfo => stInfo[0],
                              specialty => specialty.FacultyNumber,
                              (stInfo, specialty) => new Student($"{stInfo[1]} {stInfo[2]}", specialty))
                        .OrderBy(s => s.Name)
                        .ToList()
                        .ForEach(s => Console.WriteLine($@"{s.Name} {s.Specialty.FacultyNumber} {s.Specialty.Name}"));
        }

        class Specialty
        {
            public Specialty(string name, string number)
            {
                this.Name = name;
                this.FacultyNumber = number;
            }

            public string Name { get; set; }

            public string FacultyNumber { get; set; }
        }

        class Student
        {
            public Student(string name, Specialty specialty)
            {
                this.Name = name;
                this.Specialty = specialty;
            }

            public string Name { get; set; }

            public Specialty Specialty { get; set; }
        }

        public static void E10_GroupByGroup(List<string[]> studentsList)
        {
            studentsList
                .Select(e => new Person($"{e[0]} {e[1]}", int.Parse(e[2])))
                .GroupBy(p => p.Group, p => p.Name)
                .OrderBy(g => g.Key)
                .ToList()
                .ForEach(g => Console.WriteLine($"{g.Key} - {string.Join(", ", g.ToList())}"));
        }

        class Person
        {
            public Person(string name, int group)
            {
                this.Name = name;
                this.Group = group;
            }

            public string Name { get; set; }

            public int Group { get; set; }
        }

        public static void E9_StudentEnrolledInYears(List<string[]> studentsList)
        {
            studentsList
                    .Where(arr => arr[0].EndsWith("14") || arr[0].EndsWith("15"))
                    .Select(arr => arr.Skip(1))
                    .ToList()
                    .ForEach(arr => Console.WriteLine(string.Join(" ", arr)));
        }

        public static void E8_WeakStudents(List<string[]> studentsList)
        {
            studentsList
                    .Where(e => e.Skip(2).Select(int.Parse).Where(n => n <= 3).Count() >= 2)
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E7_ExelentStudent(List<string[]> studentsList)
        {
            studentsList
                    .Where(e => e.Skip(2).Contains("6"))
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E6_FilterStudentsByPhone(List<string[]> studentsList)
        {
            studentsList
                    .Where(e => e[2].StartsWith("02") || e[2].StartsWith("+3592"))
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E5_FilterStudentsByEmailD(List<string[]> studentsList)
        {
            studentsList
                    .Where(e => e[2].Substring(e[2].IndexOf('@') + 1) == "gmail.com")
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E4_SortStudents(List<string[]> studentsList)
        {
            studentsList
                    .OrderBy(s => s[1])
                    .ThenByDescending(s => s[0])
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E3_StudentByAge(List<string[]> studentsList)
        {
            studentsList
                    .Where(s => int.Parse(s[2]) >= 18 && int.Parse(s[2]) <= 24)
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]} {e[2]}"));
        }

        public static void E2_StudentByFirstandLastNames(List<string[]> studentsList)
        {
            studentsList
                    .Where(e => e[0].CompareTo(e[1]) == -1)
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static void E1_StudentsByGroups(List<string[]> studentsList)
        {
            studentsList
                    .Where(s => s[2] == "2")
                    .OrderBy(s => s[0])
                    .ToList()
                    .ForEach(e => Console.WriteLine($"{e[0]} {e[1]}"));
        }

        public static List<string[]> GetStudentsInfo()
        {
            var studentsList = new List<string[]>();

            var studentInput = Console.ReadLine();

            while (studentInput != "END")
            {
                var studenInfo = studentInput.Split();

                studentsList.Add(studenInfo);

                studentInput = Console.ReadLine();
            }

            return studentsList;
        }
    }
}
