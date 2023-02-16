using System;
using System.Collections.Generic;
using UnityEditor;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

public class FilmsManager
{
    private static FilmsManager instance;
    public List<Film> films = new List<Film>();
    private List<string> Lang = new List<string>();

    private int price;

    public static FilmsManager getInstance()
    {
        return instance ?? (instance = new FilmsManager());
    }

    FilmsManager()
    {
       
    }
 
     public List<Film> loadFilms()
    {
        new ExcelHelper().parse();

        return films;
    }

    private Film LoadFilm(int number, string[] languages, string year_of_production, string age_restriction,
        string owner, int price, string duration)
    {
        
        Film film = new Film(number, languages, year_of_production, age_restriction, owner, price, duration);


        return film;
    }
  

    //test
    private Film generateRandomFilm(int nn)
    {
        /* */
        Debug.Log(nn);
        System.Random rnd = new System.Random();
        string[] languages = { "RU", "EN" };
        string year_of_production = rnd.Next(2000, 2023) + " г.";
        string age_restriction = rnd.Next(0, 18) + "+";
        string owner = "owner" + rnd.Next(0, 5);

        int price = 1;
        string duration = rnd.Next(1, 3) * 10 + " минут";

        Film film = new Film(nn, languages, year_of_production, age_restriction, owner, price, duration);
        foreach (var lang in languages)
        {
            film.ids.Add(lang, rnd.Next(1000, 9999));
            film.names.Add(lang, "name" + rnd.Next(1000, 9999));
            film.descriptions.Add(lang, "description" + rnd.Next(1000, 9999) + lang);
            film.cover_paths.Add(lang, "c:/123/123/name" + rnd.Next(1000, 9999) + lang + ".png");
            film.trailer_paths.Add(lang, "c:/123/123/name" + rnd.Next(1000, 9999) + lang + ".mp4");
        }

        return film;
    }

    public List<Film> getFilms()
    {
        return films;
    }
}