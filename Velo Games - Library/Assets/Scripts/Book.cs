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
    public string borrowedDate; // DateTime � json olarak kaydedemedi�im i�in string kulland�m
}
