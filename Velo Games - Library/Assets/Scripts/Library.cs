using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Globalization;

[System.Serializable]
public class SaveData
{
    public List<Book> savedBooks = new List<Book>();
    public List<Book> savedBorrowedBooks = new List<Book>();
    public List<Book> savedOverdueBooks = new List<Book>();
    public int LastBorrowId;
}

public class Library : MonoBehaviour
{
    public int ReturnDays { get; private set; } = 30;
    public static Library Instance { get; private set; }
    public List<Book> borrowedBooks = new();
    public List<Book> overdueBooks = new();
    public List<Book> books = new();
    private int borrowId = 1;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadLists();
    }
    public IEnumerable<Book> GetAllBooks()
    {
        return books;
    }
    public IEnumerable<Book> GetAllBorrowedBooks()
    {
        return borrowedBooks;
    }
    public IEnumerable<Book> GetAllOverdueBooks()
    {
        return overdueBooks;
    }
    public IEnumerable<Book> SearchBook(string query)
    {
        return books.Where(b => b.title.Contains(query) || b.author.Contains(query) || b.isbn.Contains(query));
    }
    public IEnumerable<Book> SearchBorrowedBook(string query)
    {
        return borrowedBooks.Where(b => b.title.Contains(query) || b.author.Contains(query) || b.isbn.Contains(query));
    }
    public IEnumerable<Book> SearchOverdueBook(string query)
    {
        return overdueBooks.Where(b => b.title.Contains(query) || b.author.Contains(query) || b.isbn.Contains(query));
    }
    public void ReturnBook(Book book)
    {
        var originalBook = books.FirstOrDefault(b => b.isbn == book.isbn); // copyCount vs. deðiþtirmek için isbn numarasýný kullanarak orijinal kitabý bul
        originalBook.copyCount++;
        originalBook.borrowedCopies--;
        borrowedBooks.Remove(book);
        SaveLists();
    }
    public void AddBook(Book book)
    {
        books.Add(book);
        SaveLists();
    }
    public void BorrowBook(Book book)
    {
        if (book.copyCount > 0)
        {
            book.borrowedCopies++;
            book.copyCount--;
            Book newBook = new Book(); // bütün kitaplarda ayný deðerler olmamasý için yeni kopya oluþturma
            newBook.title = book.title;
            newBook.author = book.author;
            newBook.isbn = book.isbn;
            newBook.borrowedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // ödünç alýnan zamaný stringe çevir
            newBook.borrowId = borrowId;
            borrowId++; // Sonraki kitaplar için deðeri artýr
            borrowedBooks.Add(newBook);
            SaveLists();
        }
    }
    public void CheckOverdueBooks() // þimdilik kullanýlmýyor
    {
        DateTime now = DateTime.Now;
        overdueBooks.Clear();
        foreach (Book book in borrowedBooks)
        {
            //if (book.borrowedDate != null && (now - book.borrowedDate).TotalDays > ReturnDays)
            //{
            //    overdueBooks.Add(book);
            //    //SaveOverdueBooks();
            //    SaveLists();
            //}
            DateTime borrowedDate = DateTime.ParseExact(book.borrowedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            if (book.borrowedDate != null && (now - borrowedDate).TotalDays > ReturnDays)
            {
                overdueBooks.Add(book);
                SaveLists();
            }
        }
    }
    public bool IsAlreadyAdded(string isbn, string title) // Title ya da isbn daha önce kullanýlmýþ mý kontrol et
    {
        bool isAlreadyUsed = books.Exists(b => b.isbn == isbn || b.title == title);
        return isAlreadyUsed;
    }
    public void SaveLists()
    {
        SaveData saveData = new SaveData
        {
            savedBooks = books,
            savedBorrowedBooks = borrowedBooks,
            savedOverdueBooks = overdueBooks,
            LastBorrowId = borrowId
        };
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.dataPath + "/Saves/library.json", jsonData);
    }
    public void LoadLists()
    {
        string filePath = Application.dataPath + "/Saves/library.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            books = saveData.savedBooks;
            borrowedBooks = saveData.savedBorrowedBooks;
            overdueBooks = saveData.savedOverdueBooks;
            borrowId = saveData.LastBorrowId;
        }
    }
}
