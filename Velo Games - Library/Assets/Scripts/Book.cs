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
    public int borrowId = 0; // daðýtýlan kopyalarý takip edebilmek için
    public int borrowedCopies =0;
    public string borrowedDate; // DateTime ý json olarak kaydedemediðim için string kullandým
}
