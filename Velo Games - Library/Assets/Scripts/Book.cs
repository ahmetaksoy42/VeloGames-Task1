using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Book
{
    public string title;
    public string author;
    public string isbn;
    public int copyCount;
    public int borrowId = 0; // da��t�lan kopyalar� takip edebilmek i�in
    public int borrowedCopies =0;
    //public DateTime borrowedDate = DateTime.MinValue;
    public string borrowedDate; //DateTime t�r� json olarak kaydedilemedi�i i�in string olarak kaydedildi
}
