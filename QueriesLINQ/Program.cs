using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new CourseContext();
            Extentions(context);
            Console.Read();
            //Learn both ways but preffer Extention
        }
        static void LINQ(CourseContext context)
        {
            //LINQ Query
            var courses = from val in context.Courses
                          where val.Name.Contains("C#")
                          orderby val.Name
                          select val;
            var where = from val in context.Courses
                        where val.Level == 1 && val.AuthorId == 1
                        select val;
            var ordering = from val in context.Courses
                           where val.AuthorId == 1
                           orderby val.Level descending, val.Name ascending
                           select val;
            var selectNavigation = from val in context.Courses
                                   where val.AuthorId == 1
                                   orderby val.Level descending, val.Name ascending
                                   select new { Name = val.Name,ID=val.Author.Id, Author = val.Author.Name };
            var grouping = from val in context.Courses
                           group val by val.Level into grp
                           select grp; //   Select by count and key in foearch looping
            var join = from val in context.Courses
                       join aut in context.Authors 
                            on val.AuthorId equals aut.Id
                       select new { CouseName = val.Name, AuthorName = aut.Name };
            var joinGroup = from aut in context.Authors
                            join cur in context.Courses
                                on aut.Id equals cur.AuthorId
                            into mapping
                            select new {AuthorName = aut.Name, Coureses = mapping};
            var crossJoin = from aut in context.Authors
                            from cur in context.Courses
                            select new { AuthorName = aut.Name, CourseName = cur.Name }; 
            foreach (var value in joinGroup)
            {
                var price = (float) 0;
                foreach (var val in value.Coureses)
                {
                    price += val.FullPrice;
                }
                Console.WriteLine($"{value.AuthorName} has revenue {price}");
            }
        }
        static void Extentions(CourseContext context)
        {
            //Extentions Query
            var courses= context.Courses
                                .Where(val => val.Name.Contains("C#"))
                                .OrderBy(val => val.Name)
                                .Select(val => val);
            //Where Order Projection Selection
            var select = context.Courses.Where(val => val.Level == 1)
                                .OrderBy(val => val.Name)
                                .ThenByDescending(val => val.Level)
                                .Select(val => new {Name = val.Name, Author = val.Author.Name});
            var distinct = context.Courses.Where(val => val.Level == 1)
                    .OrderBy(val => val.Name)
                    .ThenByDescending(val => val.Level)
                    //.Select(val => val.Tags); need to loop iterate nest loop
                    .SelectMany(val => val.Tags)
                    .Distinct();   //Don't need iterate nest loop
            var groups = context.Courses.GroupBy(value => value.Level);
            var joining = context.Courses.Join(context.Authors, 
                c => c.AuthorId,
                a => a.Id,
                (course, author) => new
                {
                    CourseName = course.Name,
                    AuthorName = author.Name
                });
            var joiningGroup = context.Authors.GroupJoin(context.Courses, 
                a => a.Id, 
                c => c.AuthorId,
                (author,course) => new
                {
                    AuthorName = author.Name,
                    Course = course
                });
            var joiningCross = context.Authors.SelectMany(a => context.Courses, 
                (author, course) => new
            {
                    AuthorName = author.Name,
                    Course = course
            });
            //Additional Extentions method
            var partitioning = context.Courses.Skip(2).Take(2);
            var elementOperator = context.Courses.OrderBy(c => c.Level)
                .Where(c => c.Level == 1)
                .FirstOrDefault();
            
            foreach (var item in joiningGroup)
            {
                var price = (float) 0;
                foreach (var itm in item.Course)
                {
                    //Console.WriteLine("\t" + itm.Name + $" - has price {itm.FullPrice.ToString("C3", CultureInfo.CurrentCulture)}");
                    price += itm.FullPrice;
                }
                //Console.WriteLine($"{item.AuthorName} has revenue of sell book - {price.ToString("C3", CultureInfo.CurrentCulture)} \n");
            }
        }
    }
}
