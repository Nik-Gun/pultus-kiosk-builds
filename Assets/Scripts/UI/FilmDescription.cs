using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FilmDescription : MonoBehaviour
{
    public int FilmFull;
    int cur_numb;
    public Text numberText;
    public Image cover;
    public Text languagesText;
    public Text yearOfProductionText;
    public Text ageRestrictionText;
    public Text nameText;
    public Text priceText;
    public Text durationText;
    public Text descriptText;
    public List<Film> films;
    public string cover_patch,bufspr,bufdes,bufnames;
    public Session session;
    void Start()
    {
        
         films = FilmsManager.getInstance().getFilms();
         Create();
    }
    public void Create()
    {
        
        DisplayDescription(FilmFull,films);
        
    }
    public void AddManagerBascet()
    {
     Manager scriptManager = FindObjectOfType<Manager>();
     scriptManager.Addbasket( cur_numb);
    }
    public void PrevFilm()
    {
        FilmFull--;
        DisplayDescription(SessionManager.getInstance().currentSession.catalog.previousFilm(),films);
        
    }
    public void NexrFilmF()
    {
        
        DisplayDescription(SessionManager.getInstance().currentSession.catalog.nextFilm(),films);
        
    }
    public void Des()
    {
         SessionManager.getInstance().currentSession.catalog.toCatalog();
         Manager scriptManager = FindObjectOfType<Manager>();
         scriptManager.ReLoad();
         Destroy(this.gameObject);
    }
    public void DisplayDescription(int filmNumber, List<Film> films)
    {
       
            

                  Film selectedFilm = SessionManager.getInstance().currentSession.catalog.intoToFilm(filmNumber);
                  cur_numb=filmNumber;
                 string languages = "RU";
                 numberText.text = (filmNumber+1).ToString();
                 languagesText.text =languages;
                 yearOfProductionText.text =selectedFilm.year_of_production;
                 ageRestrictionText.text =selectedFilm.age_restriction;
                 selectedFilm.names.TryGetValue("RU", out bufnames);
                 nameText.text= bufnames;
                 priceText.text =selectedFilm.price.ToString();
                 durationText.text = selectedFilm.duration;
                 selectedFilm.cover_paths.TryGetValue("RU", out bufspr);
                 selectedFilm.descriptions.TryGetValue("RU", out bufdes);
                 descriptText.text=bufdes;
                 cover_patch=bufspr;
                 LoadSprite(bufspr);
           
            
        
        
        
    }
    void Update()
    {
        GameObject Start= GameObject.Find("StartScreen");
        if(Start!=null)
        {
        if(Start.activeSelf==true)
        {
         Destroy(this.gameObject);
        }
        }
    }
    public void LoadSprite(string path)
    {
    path=Application.persistentDataPath+"/"+path; 
      
     if (System.IO.File.Exists(path))
     {
         byte[] bytes = System.IO.File.ReadAllBytes(path);
         Texture2D texture = new Texture2D(256, 64);
         texture.LoadImage(bytes);
         Sprite sprite =
         cover.sprite=Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
     }
    }
}
