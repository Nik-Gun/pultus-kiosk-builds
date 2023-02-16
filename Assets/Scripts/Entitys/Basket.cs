using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Basket
{
    private List<Guest> guests = new List<Guest>();
    public int currentGuest = -1;

    int guestLimit = 4;
    private int discount = 0;

    public Basket()
    {
        addGuest();
    }

    public Guest selectGuest(int index)
    {
        currentGuest = index;
        return getCurrentGuests();
    }

    public bool setDiscount(int discount)
    {
        if ((this.discount != 0 && discount != 0) || this.discount > 100)
            return false;

        this.discount = discount;
        return true;
    }

    public int getPrice()
    {
        //CHECKIT   int price= guests.SelectMany(guest => guest.getProducts()).Sum(product => product.film.price);
        int price = 0;
        foreach (Guest guest in guests)
        {
            foreach (ProductItem product in guest.getProducts())
            {
                price += product.film.price;
            }
        }

        return price - (price * discount) / 100; //checkit
    }

    public bool addGuest()
    {
    if (guests.Count >= guestLimit) return false;
    guests.Add(new Guest());
    currentGuest=guests.Count-1;
    return true;
     }

    public List<Guest> getGuests()
    {
        return guests;
    }

   public bool removeGuest(int index)
   {
    if (guests.Count == 1)
    {
        guests[0].selected_products.Clear();
        return false;
    }

    if (guests.Count <= index) return false;
    currentGuest = 0;
    guests.RemoveAt(index);
    return true;
   }

    public Guest getCurrentGuests()
    {
        return guests[currentGuest];
    }
}