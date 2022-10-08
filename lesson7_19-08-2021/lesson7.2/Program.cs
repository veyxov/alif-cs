using System;

class Title {
    public string TheTitle { get; set; }
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Title: {TheTitle}"); Console.ResetColor();
    }
}

class Author {
    public string TheAuthor { get; set; }
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Author: {TheAuthor}");
        Console.ResetColor();
    }
}

class Content {
    public string TheContent { get; set; }
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Content: {TheContent}");
        Console.ResetColor();
    }
}

class Book {
    public Title title;
    public Author author;
    public Content content;

    // Default constructor
    public Book() {
        title = new Title();
        author = new Author();
        content = new Content();
    }
}


class Program {
    static void Main() {
        Console.Write("Input book title: ");
        string title = Console.ReadLine();

        Console.Write("Input book author: ");
        string author = Console.ReadLine();

        Console.Write("Input book content (one line): ");
        string content = Console.ReadLine();

        Book book = new Book();
        book.title.TheTitle = title;
        book.author.TheAuthor = author;
        book.content.TheContent = content;

        book.title.Show();
        book.author.Show();
        book.content.Show();
    }
}
