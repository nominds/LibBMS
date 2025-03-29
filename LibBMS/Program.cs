using System;

using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Data.Repository;


class Program
{
    static void Main(string[] args)
    {
        var bookRepository = new BookRepository();
        var bookService = new BookService(bookRepository);

        while (true)
        {
            Console.Clear();
            DisplayMenu();

            var userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput)) continue;

            switch (userInput.ToLower())
            {
                case "1":
                    Console.WriteLine("Calling AddBook");
                    AddBook(bookService);
                    break;

                case "2":
                    Console.WriteLine("View a book");
                    ViewBookById(bookService);
                    break;

                case "3":
                    Console.WriteLine("Calling ViewBook");
                    ViewBooks(bookService);
                    break;

                case "4":
                    Console.WriteLine("Calling UpdateBook");
                    break;

                case "5":
                    Console.WriteLine("Calling DeleteBook");
                    break;

                case "6":
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid selection. Please choose a valid option.");
                    break;
            }

            // Wait for user input before showing the menu again
            Console.WriteLine("\nPress any key to continue...");
            Console.Read();
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Library Management System");
        Console.WriteLine("1. Add a Book");
        Console.WriteLine("2. View a Book");
        Console.WriteLine("3. View All Books");
        Console.WriteLine("4. Update a Book");
        Console.WriteLine("5. Delete a Book");
        Console.WriteLine("6. Exit");

        Console.Write("\nChoose an option: ");
    }


    static void AddBook(BookService bookService)
    {
        Console.WriteLine("\nEnter the book details:");

        Console.Write("Title: ");
        var title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("\nInvalid book title (empty or whitespace):");
            return;
        }

        Console.Write("Author: ");
        var author = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("\nError: Invalid author name. Please try again.");
            return;
        }

        Console.Write("ISBN (both 10 digits - 13 digits): ");
        var isbn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine("\nError: Invalid ISBN. Please try again.");
            return;
        }

        Console.Write("Year Published (YYYY): ");
        var yearPublished = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(yearPublished)
        || Utilities.IsInvalidYear(yearPublished))
        {
            Console.WriteLine("Error: Invalid year error. Please try again.");
            return;
        }

        bookService.AddBook(title, author, isbn, yearPublished);
        Console.WriteLine("Book added successfully.");
    }

    static void ViewBookById(BookService bookService)
    {
        // Console.Write("\nList of books available in the library: ");

        ListBooksById(bookService);

        Console.Write("\nEnter the ID of the book to list details: ");
        if (!int.TryParse(Console.ReadLine(), out var bookId))
        {
            Console.WriteLine("Invalid book ID.");
            return;
        }

        var book = bookService.GetBookById(bookId);
        if (book == null || string.IsNullOrEmpty(book.ISBN))
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} with ISBN: {book.ISBN} published in year ({book.YearPublished})");

    }


    static void ViewBooks(BookService bookService)
    {
        Console.WriteLine("\nList of available books in the library:");

        var books = bookService.GetAllBooks().ToList();
        if (!books.Any())
        {
            Console.WriteLine("No books found in the library.");
        }
        else
        {
            Console.WriteLine($"################################################################");
            Console.WriteLine($"##   Bookid: Book Title by Book Author with isbn published (Year of Publication)  ##");
            Console.WriteLine($"#################################################################");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} with ISBN: {book.ISBN} publshed in year ({book.YearPublished})");
            }
        }
    }

    static void ListBooksById(BookService bookService)
    {
        Console.WriteLine("\nIds of books available in the library:");

        var books = bookService.GetAllBooks().ToList();
        if (!books.Any())
        {
            Console.WriteLine("No books found in the library.");
        }
        else
        {
        
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}");
            }
        }
    }

}


