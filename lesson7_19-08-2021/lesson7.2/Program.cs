using System;

class Title {
    private string title;
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Title: {title}");
        Console.ResetColor();
    }
    public void Set(string title) {
        this.title = title;
    }
}

class Author {
    private string author;
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Author: {author}");
        Console.ResetColor();
    }
    public void Set(string author) {
        this.author = author;
    }
}

class Content {
    private string content;
    public void Show() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Content: {content}");
        Console.ResetColor();
    }
    public void Set(string content) {
        this.content = content;
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
        book.title.Set(title);
        book.author.Set(author);
        book.content.Set(content);

        book.title.Show();
        book.author.Show();
        book.content.Show();
    }
}
