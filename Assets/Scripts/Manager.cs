using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject [] page;
    public GameObject Page_object;
    public GameObject FilmCatalogTile_object,Loading_Screen,Start_Screen,Bascet,BascetFilm;
    public GameObject page_father_tranform;
    public Image window;
    public Sprite notPaid,okPaid;
    public GuesManager GuesManager;
    public Session session;
    public Text curpage_text,catalog_price,bascet_price;
    public List<Film> films;
    int nextpage,curpage,prevpage;
    int film_count;
    string buf;
    public void Pay()
    {
       PayManager.getInstance().createPay("77777777",SessionManager.getInstance().currentSession.basket.getPrice());
       Invoke("GetPay",5f);
    } 
    void GetPay()
    {
         Invoke("GetPay",5f);
         if(PayManager.getInstance().update().Contains("wno") && PayManager.getInstance().update().Contains("empty") && PayManager.getInstance().update().Contains("not_enough_money") && PayManager.getInstance().update().Contains("error") &&
         PayManager.getInstance().update().Contains("time_out") && PayManager.getInstance().update().Contains("incorrect_pin") && PayManager.getInstance().update().Contains("not_enough_money")&& PayManager.getInstance().update().Contains("pin_was_blocked"))
         {
              window.sprite=notPaid;
             CancelInvoke();
         }
         
         if(PayManager.getInstance().update().Contains("pay-ok"))
         {
             window.sprite=okPaid;
             CancelInvoke();
         }
         


    }
    void Start()
    {
       
        page =new GameObject[100];
        page[0]=Instantiate(Page_object, transform);
        page[0].transform.parent=page_father_tranform.transform;
        FilmsManager.getInstance().loadFilms();
        films = FilmsManager.getInstance().getFilms();
        session = SessionManager.getInstance().newSession();
        curpage_text.text = session.catalog.currentPage+"/"+session.catalog.lastpage;
        loadFilmTiles();

    }
    // Start is called before the first frame update
     public void loadFilmTiles()
    {
        foreach (Film film in films)
        { 
            
           
            if(film_count==9)
             {
                nextpage++;
                page[nextpage]=Instantiate(Page_object, transform);
                page[nextpage].transform.parent=page_father_tranform.transform;
                page[nextpage].SetActive(false);
                film_count=0; 
             }
             film_count++;
            GameObject filmTile = Instantiate(FilmCatalogTile_object, transform);
            filmTile.transform.parent=page[nextpage].transform;
            FilmCatalogTile filmCatalogTile = filmTile.GetComponent<FilmCatalogTile>();
            filmCatalogTile.number= film.nn;
            Debug.Log(film.nn);
            filmCatalogTile.duration = film.duration;
            filmCatalogTile.age = film.age_restriction;
            filmCatalogTile.cost = film.price.ToString();
            film.cover_paths.TryGetValue("RU", out buf);
            filmCatalogTile.sprite_name=buf;
        }
        Loading_Screen.SetActive(false);
    }
    
    public  void ReLoad()
    {
       Start_Screen.SetActive(true);
       RemoveAllChildren(GuesManager.guestBascet[0]);
       RemoveAllChildren(GuesManager.guestBascet[1]);
       RemoveAllChildren(GuesManager.guestBascet[2]);
       RemoveAllChildren(GuesManager.guestBascet[3]);
       SessionManager.getInstance().currentSession.basket.removeGuest(0);
       SessionManager.getInstance().currentSession.basket.removeGuest(1);
       SessionManager.getInstance().currentSession.basket.removeGuest(2);
       SessionManager.getInstance().currentSession.basket.removeGuest(3);

      
       
    }
    public void toCatalog()
    {
        GameObject fullfilm=GameObject.Find("FilmFull(Clone)");
        fullfilm.SetActive(false);
    }
    public void NextPage()
    {
        
         
        int numberpage=session.catalog.currentPage;
        
        page[numberpage- 1].SetActive(false);
        page[session.catalog.nextPage() - 1].SetActive(true);  
         
        curpage_text.text = session.catalog.currentPage+"/"+session.catalog.lastpage;

       
        
    }
    public static void RemoveAllChildren(GameObject parent)
        {
            Transform transform;
            for(int i = 0;i < parent.transform.childCount; i++)
            {
                transform = parent.transform.GetChild(i);
                GameObject.Destroy(transform.gameObject);
            }

        }
    public void Addbasket(int i)
    {    
        
         Bascet.SetActive(true); 
         Film selectedFilm = SessionManager.getInstance().currentSession.catalog.intoToFilm(i);
         if( session.basket.getCurrentGuests().addProduct(selectedFilm,"RU"))
         {
         
         GameObject filmBascaet= Instantiate(BascetFilm, GuesManager.guestBascet[SessionManager.getInstance().currentSession.basket.currentGuest].transform);
         Debug.Log("GUEST"+SessionManager.getInstance().currentSession.basket.currentGuest);
         filmBascaet.GetComponent<BascetFilm>().price=selectedFilm.price.ToString();
         
         string bufnames;
         selectedFilm.names.TryGetValue("RU", out bufnames);
         filmBascaet.GetComponent<BascetFilm>().film_name=bufnames;
         string bufpatch;
         selectedFilm.cover_paths.TryGetValue("RU", out bufpatch);
         filmBascaet.GetComponent<BascetFilm>().cover_patch=bufpatch;
         }

    
    }
     public void PrevPage()
    {
         int numberpage=session.catalog.currentPage;
        
         page[numberpage- 1].SetActive(false);
         page[session.catalog.previousPage() - 1].SetActive(true);
         curpage_text.text = session.catalog.currentPage+"/"+session.catalog.lastpage;
    }
    // Update is called once per frame
    void Update()
    {
        catalog_price.text=SessionManager.getInstance().currentSession.basket.getPrice().ToString();
         bascet_price.text=SessionManager.getInstance().currentSession.basket.getPrice().ToString();
    }
}
