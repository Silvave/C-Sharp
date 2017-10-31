using BookShopSystem.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BookShopSystem.Models;
using EntityFramework.Extensions;

namespace BookShopSystem
{
    public class BookShopMain
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();

            //context.Database.Initialize(true);

            //BookTitlesByAgeRestriction(context);

            //GoldenBooks(context);

            //BooksByPrice(context);

            //NotReleasedBooks(context);

            //BookTitlesByCategory(context);

            //BookReleasedBeforeDate(context);

            //AuthorsSearch(context);

            //BooksSearch(context);

            //BookTitlesSearch(context);

            //CountBooks(context);

            //TotalBookCopies(context);

            //FindProfit(context);

            //MostRecentBooks(context);

            //IncreaseBookCopies(context);

            //RemoveBooks(context);

            StoredProcedure(context);
        }

        private static void StoredProcedure(BookShopContext context)
        {
            //var queryToCreateProcedure = @"CREATE PROCEDURE usp_getBooksCountByAuthor (@firstName VARCHAR(50), @lastName VARCHAR(50))
            //AS
            //BEGIN
            //	SELECT COUNT(*) FROM Books AS b
            //		INNER JOIN Authors as a
            //			ON b.Author_Id = a.Id
            //		WHERE a.FirstName = @firstName AND a.LastName = @lastName
            //END";
            //context.Database.ExecuteSqlCommand(queryToCreateProcedure);
            //context.SaveChanges();

            var firstName = Console.ReadLine();
            var secondName = Console.ReadLine();

            var paramFirstName = new SqlParameter("@firstName", firstName);
            var paramSecondName = new SqlParameter("@secondName", secondName);

            var booksOfAuthor = context.Database
                .SqlQuery<int>("EXEC dbo.usp_getBooksCountByAuthor @firstName, @secondName",
                    paramFirstName, paramSecondName)
                .First();

            Console.WriteLine(booksOfAuthor);
        }

        private static void RemoveBooks(BookShopContext context)
        {
            var deletedBooks = context.Books.Where(b => b.Copies < 4200).Delete();

            Console.WriteLine($"{deletedBooks} books were deleted");
        }

        private static void IncreaseBookCopies(BookShopContext context)
        {
            
            DateTime date = new DateTime(2013,6,6);

            var affectedBooks = context.Books
                .Update(b => b.ReleaseDate != null && b.ReleaseDate > date,
                             b => new Book() { Copies = b.Copies + 44 });

            Console.WriteLine($"{affectedBooks} books are released after 6 Jun 2013 so total of {affectedBooks*44} book copies were added");

            context.SaveChanges();
        }

        private static void MostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    BooksNumber = c.Books.Count,
                    Top3Books = c.Books
                        .Select(b => new {b.Title, b.ReleaseDate})
                        .OrderByDescending(x => x.ReleaseDate)
                        .ThenBy(x => x.Title)
                        .Take(3)
                })
                 .Where(c => c.BooksNumber > 35)
                 .OrderByDescending(c => c.BooksNumber);

            foreach (var category in categories)
            {
                Console.WriteLine($"--{category.Name}: {category.BooksNumber} books");
                foreach (var book in category.Top3Books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate})");
                }
            }
        }

        private static void FindProfit(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.Books.Select(x => x.Copies * x.Price).Sum()
                })
                .OrderByDescending(b => b.TotalProfit);

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} - ${category.TotalProfit}");
            }
        }

        private static void TotalBookCopies(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(x => x.Copies);

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName} - {author.Copies}");
            }
        }

        private static void CountBooks(BookShopContext context)
        {
            var titleLength = int.Parse(Console.ReadLine());

            var booksCount = context.Books.Where(b => b.Title.Length > titleLength).ToArray().Length;

            Console.WriteLine(booksCount);
        }

        private static void BookTitlesSearch(BookShopContext context)
        {
            var lastNamePiece = Console.ReadLine();

            var books = context.Books
                .Where(b => b.Author.LastName.Substring(0, lastNamePiece.Length) == lastNamePiece)
                .OrderBy(b => b.Id);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
            }
        }

        private static void BooksSearch(BookShopContext context)
        {
            var stringPiece = Console.ReadLine();

            var query = @"SELECT Title FROM dbo.Books WHERE Title LIKE @nameParam";

            var nameParam = new SqlParameter("@nameParam", "%" + stringPiece + "%");

            var books = context.Database.SqlQuery<string>(query, nameParam);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private static void AuthorsSearch(BookShopContext context)
        {
            var firstNameEnding = Console.ReadLine();

            var query =
                @"SELECT CONCAT(FirstName, ' ', LastName) FROM dbo.Authors WHERE FirstName LIKE @nameParam";

            var nameParam = new SqlParameter("@nameParam", "%" + firstNameEnding);

            var authors = context.Database.SqlQuery<string>(query, nameParam);

            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
        }

        private static void BookReleasedBeforeDate(BookShopContext context)
        {
            var date = DateTime.Parse(Console.ReadLine());

            var books = context.Books.Where(b => b.ReleaseDate < date);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }

        }

        private static void BookTitlesByCategory(BookShopContext context)
        {
            var categories = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToArray();

            var books = context.Books
                .Where(b => b.Categories.Any(x => categories.Contains(x.Name.ToLower())));

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void NotReleasedBooks(BookShopContext context)
        {
            var year = Console.ReadLine();

            var releasedDate = new DateTime(int.Parse(year), 1, 1);

            var books = context.Books.Where(b => b.ReleaseDate > releasedDate).OrderBy(b => b.Id);

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void BooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Price < 5 || b.Price > 40).OrderBy(b => b.Id);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - ${book.Price}");
            }
        }

        private static void GoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType.ToString() == "Gold" && b.Copies < 5000)
                .OrderBy(b => b.Id);

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void BookTitlesByAgeRestriction(BookShopContext context)
        {
            var ageCategoryInput = Console.ReadLine().ToLower();

            string ageCategory = ageCategoryInput.Substring(0, 1).ToUpper() + ageCategoryInput.Substring(1);

            var books = context.Books.Where(b => b.AgeRestriction.ToString() == ageCategory);

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}
