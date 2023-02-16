using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BascetFilm : MonoBehaviour
{
    public Image cover;
    public Text film_name_text,price_text,Index;
    public string film_name,price;
    public string cover_patch;
    public Session session;
    
    // Start is called before the first frame update
    void Start()
    {
        film_name_text.text=film_name;
        price_text.text=price;
        Index.text= (transform.GetSiblingIndex()+1).ToString();
        LoadSprite(cover_patch);
        session = SessionManager.getInstance().currentSession;


    }
    public void Des()
    {
        session.basket.getCurrentGuests().removeProduct(transform.GetSiblingIndex());
        Destroy(this.gameObject);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
