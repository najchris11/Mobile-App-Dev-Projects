using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace cs {
    public class Student {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public double GPA { set; get; }
        public bool Legacy { set; get; }
        public override string ToString() {
            return string.Format("{0}, {1}{4} ({2}) ({3})", LastName, FirstName, GPA, SSN, Legacy ? "(L)" : "");
        }
    }
    public class Linq2 {
        public static Student[] students = {
            new Student {FirstName = "Bugs", LastName = "Bunny", SSN="111-11-1111", GPA = 3.4, Legacy = false },
            new Student {FirstName = "Marvin", LastName = "Martian", SSN="222-22-2222", GPA = 3.2, Legacy = false },
            new Student {FirstName = "Homer", LastName = "Simpson", SSN="333-33-3333", GPA = 0.4, Legacy = true },
            new Student {FirstName = "Yosemite", LastName = "Sam", SSN="444-44-4444", GPA = 1.4, Legacy = false },
            new Student {FirstName = "Bart", LastName = "Simpson", SSN="555-55-5555", GPA = 0.9, Legacy = true },
            new Student {FirstName = "Jim", LastName = "Kiper", SSN="666-66-6666", GPA = 2.4, Legacy = true },
            new Student {FirstName = "Elmer", LastName = "Fudd", SSN="777-77-7777", GPA = 2.0, Legacy = false },
            new Student {FirstName = "SpongeBob", LastName = "SquarePants", SSN="888-88-8888", GPA = 3.2, Legacy = false },
            new Student {FirstName = "Shaggy", LastName = "Rogers", SSN="999-99-9999", GPA = 2.7, Legacy = false },
            new Student {FirstName = "Bobby", LastName = "Hill", SSN="000-00-0000", GPA = 2.7, Legacy = false },
        };
        public static void Display<T>(IEnumerable<T> items) {
            Console.Write("[");
            foreach (T item in items) {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine("]");
        }
        public static void Linq2Main(String[] args) {
            IEnumerable<Student> resultA = students.Where(s => s.GPA >= 3.0);
            Display(resultA);
            // [Bunny, Bugs (3.4) (111-11-1111) Martian, Marvin (3.2) (222-22-2222) SquarePants, SpongeBob (3.2) (888-88-8888) ]

            var resultB = students.Where(s => s.GPA >= 3.0);
            Display(resultB);
            // [Bunny, Bugs (3.4) (111-11-1111) Martian, Marvin (3.2) (222-22-2222) SquarePants, SpongeBob (3.2) (888-88-8888) ]

            Display(students.Where(s => s.GPA >= 3.0).Select(s => s.LastName));
            // [Bunny Martian SquarePants ]

            Display(students.Where(s => s.GPA >= 3.0).Select(s => new Tuple<string, string>(s.LastName, s.FirstName)));
            // [(Bunny, Bugs) (Martian, Marvin) (SquarePants, SpongeBob) ]

            Display(from s in students
                    where s.GPA >= 3.0
                    select s
            );
            // [Bunny, Bugs (3.4) (111-11-1111) Martian, Marvin (3.2) (222-22-2222) SquarePants, SpongeBob (3.2) (888-88-8888) ]


            Display(from s in students
                    where s.GPA >= 3.0
                    select s.LastName);
            // [Bunny Martian SquarePants ]

            Display(from s in students
                    where s.GPA >= 3.0
                    select new Tuple<string, string>(s.LastName, s.FirstName));
            // [(Bunny, Bugs) (Martian, Marvin) (SquarePants, SpongeBob) ]

            Display(from s in students
                    where s.GPA >= 3.0
                    select new
                    {
                        s.LastName,
                        s.FirstName
                    }
            );
            // [{ LastName = Bunny, FirstName = Bugs } { LastName = Martian, FirstName = Marvin } { LastName = SquarePants, FirstName = SpongeBob } ]

            Display(students.Where(s => s.LastName[0] == 'S' && s.GPA >= 3.0));
            // [SquarePants, SpongeBob (3.2) (888-88-8888) ]

            Display(from s in students
                    orderby s.GPA
                    select s.LastName);
            // [Simpson Simpson Sam Fudd Kiper Rogers Hill Martian SquarePants Bunny]

            Display(students.OrderByDescending(s => s.GPA).Select(s => s.LastName));
            // [Bunny Martian SquarePants Rogers Hill Kiper Fudd Sam Simpson Simpson ]

            Console.WriteLine(students.FirstOrDefault(s => s.LastName.Equals("Simpson")));
            // Simpson, Homer(L)(0.4)(333 - 33 - 3333)

            Console.WriteLine(students.FirstOrDefault(s => s.LastName.Equals("Zmuda")));
            // 

            Console.WriteLine(students.FirstOrDefault(s => s.LastName.Equals("Kiper")));
            // Kiper, Jim(L) (2.4) (666-66-6666)

            var highGPA = from s in students
                          where s.GPA >= 3.0
                          orderby s.GPA descending
                          select s;
            var lowGPA = from s in students
                          where s.GPA < 2.0
                          orderby s.GPA
                          select s;
            var legacy = from s in students
                         where s.Legacy
                         select s;
            Display(highGPA);
            // [Bunny, Bugs (3.4) (111-11-1111) Martian, Marvin (3.2) (222-22-2222) SquarePants, SpongeBob (3.2) (888-88-8888) ]

            Display(lowGPA);
            // [Simpson, Homer(L) (0.4) (333-33-3333) Simpson, Bart(L) (0.9) (555-55-5555) Sam, Yosemite (1.4) (444-44-4444) ]

            Display(legacy);
            // [Simpson, Homer(L) (0.4) (333-33-3333) Simpson, Bart(L) (0.9) (555-55-5555) Kiper, Jim(L) (2.4) (666-66-6666) ]

            Display(lowGPA.Except(legacy));
            // [Sam, Yosemite (1.4) (444-44-4444) ]

            Display(highGPA.Union(lowGPA));
            // [Bunny, Bugs (3.4) (111-11-1111) Martian, Marvin (3.2) (222-22-2222) SquarePants, SpongeBob (3.2) (888-88-8888) 
            //  Simpson, Homer(L) (0.4) (333-33-3333) Simpson, Bart(L) (0.9) (555-55-5555) Sam, Yosemite (1.4) (444-44-4444) 
            Console.WriteLine((from s in students
                               where s.GPA >= 2.0 
                               select s).Count());
            Console.WriteLine((from s in students
                               select s.GPA).Average());
            Console.WriteLine((from s in students
                               select s.LastName).Count());
            Console.WriteLine((from s in students
                               select s.LastName).Distinct().Count());
        }
    }
}
