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
    [SerializeField] private TMP_Text messageText;

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
            messageText.text = "Please fill in all the information !";
            messageText.color = Color.red;
        }
        else
        {
            if (library.IsAlreadyAdded(isbnInput.text, titleInput.text))
            {
                Debug.LogError("A book with this title or ISBN already exists");
                messageText.text = "A book with this title or ISBN already exists!";
                messageText.color = Color.red;
            }
            else if(int.Parse(copyCountInput.text) <= 0)
            {
                messageText.text = "Copy count must be bigger than 0!";
                messageText.color = Color.red;
            }
            else
            {
                Book newBook = new Book();
                newBook.title = titleInput.text;
                newBook.author = authorInput.text;
                newBook.isbn = isbnInput.text;
                newBook.copyCount = int.Parse(copyCountInput.text);
                newBook.borrowedCopies = 0;
                messageText.text = "Book added to the library"; // kitap baþarýyla eklenirse hata mesajý kaybolsun
                messageText.color = Color.green;
                library.AddBook(newBook);
            }
        }
    }
}
