using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : MonoBehaviour
{
    public int nn = 0;
    //список поддерживаемых языков
    public string[] languages;

    //language dependence vars
    public Dictionary<string, int> ids = new Dictionary<string, int>(); //<languages,id>
    public Dictionary<string, string> names = new Dictionary<string, string>(); //<languages,film_name>
    public Dictionary<string, string> descriptions = new Dictionary<string, string>(); //<languages,film_description>
    public  Dictionary<string, string> cover_paths = new Dictionary<string, string>(); //<languages,path_to_cover>
    public Dictionary<string, string> trailer_paths = new Dictionary<string, string>(); //<languages,path_to_traler>

    public string year_of_production;
    public string age_restriction;
    public string owner;
    public int price;
    public string duration;

    public Film(int nn,string[] languages, string yearOfProduction, string ageRestriction, string owner, int price, string duration)
    {
        
        this.nn = nn;
        this.languages = languages;
        year_of_production = yearOfProduction;
        age_restriction = ageRestriction;
        this.owner = owner;
        this.price = price;
        this.duration = duration;
    }
    //public Film() { }
}