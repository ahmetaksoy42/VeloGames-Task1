using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddBookPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField titleInput;
    [SerializeField] private TMP_InputField authorInput;
    [SerializeField] private TMP_InputField isbnInput;
    [SerializeField] private TMP_InputField copyCountInput;
    [SerializeField] private TMP_Text errorText;

    [SerializeField] private Library library;
    private void Start()
    {
        library = Library.Instance;
    }
    public void AddBookButton()
    {
        if (titleInput.text == "" || authorInput.text == "" || isbnInput.text == "" || copyCountInput.text == "")
        {
            Debug.LogError("Please fill in all the information ");
            errorText.text = "Please fill in all the information !";
        }
        else
        {
            if (library.IsAlreadyAdded(isbnInput.text, titleInput.text))
            {
                Debug.LogError("A book with this title or ISBN already exists");
                errorText.text = "A book with this title or ISBN already exists !";
            }
            else
            {
                Book newBook = new Book();
                newBook.title = titleInput.text;
                newBook.author = authorInput.text;
                newBook.isbn = isbnInput.text;
                newBook.copyCount = int.Parse(copyCountInput.text);
                newBook.borrowedCopies = 0;
                errorText.text = ""; // kitap baþarýyla eklenirse hata mesajý kaybolsun
                library.AddBook(newBook);
            }
        }
    }
}
