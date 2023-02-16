using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session
{
    public Catalog catalog;
    public Basket basket;

    public Session()
    {
        catalog = new Catalog();
        basket = new Basket();
    }   
    
}