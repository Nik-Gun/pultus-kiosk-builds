using System;
using System.Collections.Generic;

public class Tiket
{
    public Guest guest;
    public int nn;
    private string qrText = "";
    private string qrPath = "";

    public Tiket(int nn, Guest guest)
    {
        this.nn = nn;
        this.guest = guest;
        getQR();
    }

    private void getQR()
    {
        string[] films = new string[4];
        for (int i = 0; i < 4; i++)
        {
            if (guest.getProducts().Count <= i)
            {
                films[i] = "0000";
                continue;
            }

            films[i] = guest.getProducts()[i].film.ids[guest.getProducts()[i].language] + "";
        }

        qrText = encodeQR(films); //CHECKIT
    }

    static string encodeQR(string[] films)
    {
        var rand = new Random();
        String code = "";
        for (int i = 0; i < 4; i++)
        {
            String number = films[i];
            for (int j = 0; j < 4; j++)
            {
                code += number[j];
                code += Convert.ToString(rand.Next(9));
            }

            code += Convert.ToString(rand.Next(10, 99));
        }

        return code;
    }
}