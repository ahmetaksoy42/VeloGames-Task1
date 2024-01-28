using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private Transform resultsParent;
    [SerializeField] private GameObject bookPanel;
    [SerializeField] private GameObject borrowedBookPanel;

    public void SearchAllButton()
    {
        foreach (Transform child in resultsParent)
        {
            Destroy(child.gameObject);
        }
        IEnumerable<Book> results;
        string searchText = searchInput.text;

        if (string.IsNullOrEmpty(searchText))
        {
            results = Library.Instance.GetAllBooks();
        }
        else
        {
            results = Library.Instance.SearchBook(searchText);
        }
        foreach (Book book in results)
        {
            GameObject newBookPanel = Instantiate(bookPanel, resultsParent);
            BookPanel bookPanelScript = newBookPanel.GetComponent<BookPanel>();
            bookPanelScript.SetBookData(book);
        }
    }
    public void SearchBorrowedButton()
    {
        foreach (Transform child in resultsParent)
        {
            Destroy(child.gameObject);
        }
        IEnumerable<Book> results;
        string searchText = searchInput.text;

        if (string.IsNullOrEmpty(searchText))
        {
            results = Library.Instance.GetAllBorrowedBooks();
        }
        else
        {
            results = Library.Instance.SearchBorrowedBook(searchText);
        }
        foreach (Book book in results)
        {
            GameObject newBookPanel = Instantiate(borrowedBookPanel, resultsParent);
            BookPanel bookPanelScript = newBookPanel.GetComponent<BookPanel>();
            bookPanelScript.SetBorrowedBookData(book);
        }
    }
    public void SearchOverdueButton()
    {
        foreach (Transform child in resultsParent)
        {
            Destroy(child.gameObject);
        }
        Library.Instance.CheckOverdueBooks(); // kitap tarihlerini kontrol edip listeye ekleme

        IEnumerable<Book> results;
        string searchText = searchInput.text;

        if (string.IsNullOrEmpty(searchText))
        {
            results = Library.Instance.GetAllOverdueBooks();
        }
        else
        {
            results = Library.Instance.SearchOverdueBook(searchText);
        }
        foreach (Book book in results)
        {
            GameObject newBookPanel = Instantiate(borrowedBookPanel, resultsParent);
            BookPanel bookPanelScript = newBookPanel.GetComponent<BookPanel>();
            bookPanelScript.SetOverdueBookData(book);
        }
    }
}
