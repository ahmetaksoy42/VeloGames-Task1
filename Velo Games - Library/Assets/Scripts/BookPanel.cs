using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class BookPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text authorText;
    [SerializeField] private TMP_Text isbnText;
    [SerializeField] private TMP_Text copyCountText;
    [SerializeField] private TMP_Text borrowedCopyText;
    [SerializeField] private TMP_Text borrowedDaysText;
    [SerializeField] private TMP_Text borrowIdText;
    private Book book;

    public void SetBookData(Book book)
    {
        this.book = book;
        titleText.text = $" Title : {book.title}";
        authorText.text = $" Author : {book.author}";
        isbnText.text = $" ISBN : {book.isbn}";
        copyCountText.text = $"Copy Count : {book.copyCount.ToString()}";
        borrowedCopyText.text = $"Borrow Count : {book.borrowedCopies.ToString()}";
    }
    public void SetBorrowedBookData(Book book)
    {
        this.book = book;
        titleText.text = $" Title : {book.title}";
        authorText.text = $" Author : {book.author}";
        isbnText.text = $" ISBN : {book.isbn}";
        borrowIdText.text = $"Borrow Id: {book.borrowId.ToString()}";

        DateTime borrowedDate = DateTime.ParseExact(book.borrowedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // string to DateTime
        TimeSpan passedDays = DateTime.Now - borrowedDate;
        int days = passedDays.Days; // sadece gün ve saat gözükmesi için
        int hours = passedDays.Hours;
        //borrowedDaysText.text = $"Passed Days :{days}:{hours}";
        borrowedDaysText.text = $"Passed {days} Days {hours} Hours";
        if ((DateTime.Now - borrowedDate).TotalDays >= Library.Instance.ReturnDays) // 30 günü geçtiyse kýrmýzý yazsýn
        {
            borrowedDaysText.color = Color.red;
        }
    }
    public void SetOverdueBookData(Book book)
    {
        this.book = book;
        titleText.text = $" Title : {book.title}";
        authorText.text = $" Author : {book.author}";
        isbnText.text = $" ISBN : {book.isbn}";
        borrowIdText.text = $"Borrow Id: {book.borrowId.ToString()}";
        borrowedDaysText.color = Color.red;

        DateTime borrowedDate = DateTime.ParseExact(book.borrowedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        TimeSpan passedDays = DateTime.Now - borrowedDate;
        int days = passedDays.Days; // sadece gün ve saat gözükmesi için
        int hours = passedDays.Hours;
        //borrowedDaysText.text = $"Passed Days :{days}:{hours}";
        borrowedDaysText.text = $"Passed {days} Days {hours} Hours";
    }
    public void BorrowButton()
    {
        Library.Instance.BorrowBook(book);
        borrowedCopyText.text = $"Borrow Count : {book.borrowedCopies.ToString()}";
        copyCountText.text = $"Copy Count : {book.copyCount.ToString()}";
    }
    public void TakeButton()
    {
        Library.Instance.ReturnBook(book);
        Destroy(gameObject);
    }
    public void IncreaseCopyCountButton()
    {
        book.copyCount++;
        copyCountText.text = $"Copy Count : {book.copyCount.ToString()}";
        Library.Instance.SaveLists(); // Her týklamada json u güncelliyor
    }
    public void DecreaseCopyCountButton()
    {
        if (book.copyCount > 0)
        {
            book.copyCount--;
            copyCountText.text = $"Copy Count : {book.copyCount.ToString()}";
            Library.Instance.SaveLists();
        }
    }
}
