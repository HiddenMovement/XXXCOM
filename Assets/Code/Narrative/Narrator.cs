using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public IEnumerable<Book> Books => GetComponentsInChildren<Book>();
    public Book ActiveBook;

    public bool IsReading = true;

    private void Update()
    {
        if (ActiveBook == null || !IsReading)
            return;

        ActiveBook.Read();
    }

    public void CloseBook()
    {
        IsReading = false;
    }

    public void OpenBook(Book book = null)
    {
        if (book != null)
            ActiveBook = book;

        IsReading = true;
    }
}