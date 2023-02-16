using UnityEngine;
using UnityEngine.UI;
using System;
using System.Drawing;

using System.IO;
using Random = System.Random;


public class Poligon : MonoBehaviour
{
    public Text _text;
    public Session session;

    void Start()
    {
      
    }

    public void runPay()
    {/* 
        PayManager pm = PayManager.getInstance();
        ///pm.createPay("79629001035"); //создаем класс олаты с телефоном 79629001035
        if (!pm.pay(session.basket.getPrice()))
        {
            _text.text = "Не введен номер телефона";
        }
        else
        {
            _text.text = "Начал оплату";
        }
        */
    }

    private SberTool _sberTool = new SberTool();

    public void loadparmtest()
    {
        _sberTool.payViaLoadParm(1);
    }

    public void checkloadparmtest()
    {
        ///_text.text = _sberTool.result;
    }

    public void runAlg()
    {
        session = SessionManager.getInstance().newSession();
        Debug.Log(session.catalog.currentPage + "/" + session.catalog.lastpage);
        session.catalog.nextPage();
        Debug.Log(session.catalog.currentPage + "/" + session.catalog.lastpage);
        //получаем номер по порядку четвертого фильма на странице, он соответсвует индексу FilmsManager.getInstance().getFilms()
        int globalFilmIndex = session.catalog.getPageConent()[3].nn;
        Film film = session.catalog.intoToFilm(globalFilmIndex); //нажимаем на фильм
        Debug.Log("description" +
                  film.descriptions[
                      film.languages[
                          0]]); //получаемописание для первого языка(RU)нжно хранить на фронте или продумать механику

        session.basket.getCurrentGuests().addProduct(film, film.languages[0]); //добавляем в корзину русскую версию
        _text.text = "сумма корзины:" + session.basket.getPrice();
        /*Debug.Log("до промокода:" + session.basket.getPrice());
        session.basket.setDiscount(50);
        Debug.Log("после промокода:" + session.basket.getPrice());
        Debug.Log("пытаемся еще один промокод ввести:" +
                  session.basket.setDiscount(10)); //должен спросить перетереть или старый?
        bool reWriteDiscount = true;
        if (reWriteDiscount)
        {
            session.basket.setDiscount(0); //стираем
            Debug.Log("после отмены предыдущего промокода :" + session.basket.getPrice());
            session.basket.setDiscount(10); //вставляем новый процкент
            Debug.Log("после нового промокода :" + session.basket.getPrice());
        }*/
    }
   
    // Update is called once per frame
    public void check()
    {
        //if (PayManager.getInstance().update())
        {
        }

        ///_text.text = PayManager.getInstance().curPay.payStatus + ":" + PayManager.getInstance().curPay.payStage;
    }

    private KKTDriverTool _kktDriverTool = new KKTDriverTool();

    public void printСheck()
    {
        // KKTDriverTool.printTest(new Random().Next(1, 100));
      new ExcelHelper().parse();
    }

    public void getInfo()
    {
        string text = "сумма корзины:" + session.basket.getPrice() + "\n" + "кол-во зрителей:" +
                      session.basket.getGuests().Count + "\n" + "состояние оплаты:" +
                      ///PayManager.getInstance().curPay.payStage + "\n" + "статус оплаты:" +
                      PayManager.getInstance().curPay.payStatus + "\n";
        _text.text = text;
    }

    void Update()
    {
    }
}