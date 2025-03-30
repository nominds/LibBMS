using System;
using Serilog;

using LibBMS.Common;
using LibBMS.Logger;

using LibBMS.Services;
using LibBMS.Data.Repository;

using LibBMS.AddBookWorker;
using LibBMS.ViewBookWorker;
using LibBMS.DeleteBookWorker;
using LibBMS.UpdateBookWorker;



class Program
{
    static void Main(string[] args)
    {

        LibBMSLogger.Instance.Information("Welcome to the Library Book Management System!");

        var bookRepository = new BookRepository();
        var bookService = new BookService(bookRepository);

        var addBookWorker = new BookServiceAddBookWorker();
        var viewBookWorker = new BookServiceViewBookWorker();
        var deleteBookWorker = new BookServiceDeleteBookWorker();
        var updateBookWorker = new BookServiceUpdateBookWorker();


        try
        {


            while (true)
            {
                Console.Clear();
                DisplayMenu();

                var userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput)) continue;

                switch (userInput.ToLower())
                {
                    case "1":
                        LibBMSLogger.Instance.Information("Calling AddBook");
                        addBookWorker.AddBook(bookService);
                        break;

                    case "2":
                        LibBMSLogger.Instance.Information("View a book");
                        viewBookWorker.ViewBookById(bookService);
                        break;

                    case "3":
                        LibBMSLogger.Instance.Information("Calling ViewBook");
                        viewBookWorker.ViewBooks(bookService);
                        break;

                    case "4":
                        LibBMSLogger.Instance.Information("Calling UpdateBook");
                        try
                        {
                            updateBookWorker.UpdateBook(bookService);
                        }
                        catch (Exception ex)
                        {
                            LibBMSLogger.Instance.Error("Failed to update the book with id {@bookId}", ex.Message);
                        }
                        break;

                    case "5":
                        LibBMSLogger.Instance.Information("Calling DeleteBook");

                        try
                        {
                            deleteBookWorker.DeleteBook(bookService);
                        }
                        catch (Exception ex)
                        {
                            LibBMSLogger.Instance.Error("Failed to delete the book with id {@bookId}", ex.Message);
                        }

                        break;

                    case "6":
                        LibBMSLogger.Instance.Information("Exiting... Goodbye!");
                        return;
                    default:
                        Log.Information("Not a valid option selection. Please select a valid option.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.Read();
            }
        }
        finally
        {
            LibBMSLogger.CloseAndFlush();
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Book Management System");
        Console.WriteLine("1. Add a Book");
        Console.WriteLine("2. View a Book");
        Console.WriteLine("3. View All Books");
        Console.WriteLine("4. Update a Book");
        Console.WriteLine("5. Delete a Book");
        Console.WriteLine("6. Exit");

        Console.Write("\nChoose an option: ");
    }




}


