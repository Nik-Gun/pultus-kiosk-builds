using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PromoCodeChecker : MonoBehaviour
{
    private string promoCode = "";
    public Text[] txtpromo;
    int promo_lengtch;
    public Session session;
    public GameObject WrongPromoCodeWindow;
    void Start()
    {
        session = SessionManager.getInstance().currentSession;
    }
    public void  strpromoсlear()
    {
       for ( int i = 0; i < 6; i++ )
        {  
            txtpromo[i].text="";
        }
    }
    
    public void  strpromodel()
    {
       if(promo_lengtch>0)
       {
       promo_lengtch=promo_lengtch-1;
       txtpromo[promo_lengtch].text="";
       promoCode=promoCode.Substring(0, promoCode.Length - 1);
       }
     
    } 
    public void strlpromoAdd(string strt)
    {
       if(promo_lengtch<=5)
        {
        
        txtpromo[promo_lengtch].text=strt;
        promoCode=promoCode+strt; 
        txtpromo[promo_lengtch].text="*";
        promo_lengtch= promo_lengtch+1;
        
       
        Debug.Log(promo_lengtch);

        }
        
    }   
    public void Send()
    {
       StartCoroutine(GetRequest(promoCode));
       Debug.Log(promoCode);
    }

    void Parse(string jsonString)
    {
        JSONData data = JsonConvert.DeserializeObject<JSONData>(jsonString);
        Debug.Log("Code: " + data.code);
        Debug.Log("Value: " + data.value);
        Debug.Log("Status: " + data.status);
        if(data.status==false)
        {
          WrongPromoCodeWindow.SetActive(true);
        }
        else
        {
             session.basket.setDiscount(data.value);
           
             this.gameObject.SetActive(false);
        }
    
    }
    IEnumerator GetRequest(string promoCode)
    {
        string url = "http://ovz20.j90211046.m61kn.vps.myjino.ru:49492/check?promocode=" + promoCode;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                Parse(webRequest.downloadHandler.text);
            }
        }
    } 
[System.Serializable]
public class JSONData
{
    public string code;
    public int value;
    public bool status;
}
    
}