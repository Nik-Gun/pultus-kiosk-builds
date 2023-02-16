using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FilmCatalogTile : MonoBehaviour
{
    public Text duration_text,age_text,cost_text;
    public string duration,age,cost,sprite_name;
    public Sprite cover_sprite;
    public Image cover;
    public int number;
    public GameObject FilmFull;

    // Start is called before the first frame update
    void Start()
    {
        duration_text.text=duration;
        age_text.text=age;
        cost_text.text=cost;
        LoadSprite(sprite_name);
        
      
       
    }
    public void CreateFullFilm()
    {
         GameObject FilmFullObject=Instantiate(FilmFull, transform.parent.transform.parent.transform.parent);
         FilmFullObject.transform.parent=transform.parent.transform.parent.transform.parent;
         FilmFullObject.GetComponent<FilmDescription>().FilmFull=number;
         FilmFullObject.GetComponent<FilmDescription>().Create();
    }
    public void LoadSprite(string path)
    {
       
     path=Application.persistentDataPath+"/"+path; 
     Debug.Log(path);
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
