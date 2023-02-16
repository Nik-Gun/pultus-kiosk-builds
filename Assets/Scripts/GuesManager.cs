using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GuesManager : MonoBehaviour
{
    public GameObject[] guest;
    public GameObject[] guestBascet;
    public static int index;
    Transform[] children;
    public GridLayoutGroup gridLayoutGroup;
   
    public Sprite select; 
    public Sprite deselect; 
    void Start()
    {
  
    }
    void Update()
    {
       
    }
    void SelecButton(int indexToChange)
    {
      Button[] buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length-1; i++)
        {
            if (i == indexToChange)
            {
                Image image = buttons[i].GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = select;
                    guestBascet[i].SetActive(true);
                }
            }
            else
            {
                Image image = buttons[i].GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = deselect;
                     guestBascet[i].SetActive(false);
                }
            }
        }
    }
    
    public void SortByActiveState()
    {
        
        int highestIndex = guest.Length - 1;
     
        for (int i = guest.Length-1; i >= 0; i--)
        {
            Transform child = guest[i].transform;
            
            if (child.gameObject.activeSelf)
            {
                child.SetSiblingIndex(0);
            }
        }

        
     
    }
    void ReSort()
    {
        Array.Sort(guest, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        for (int i = 0; i < guest.Length; i++)
        {
            guest[i].transform.SetSiblingIndex(i);
        }
    }
    void ReSortBuscet()
    {
    for (int i = 0; i < guest.Length; i++)
    {
        for (int j = i; j < guestBascet.Length; j++)
        {
            if (guest[i].name == guestBascet[j].name)
            {
                // Если нашли соответствие, меняем местами объекты в массиве guestBascet
                GameObject temp = guestBascet[i];
                guestBascet[i] = guestBascet[j];
                guestBascet[j] = temp;
                break;
            }
        }
    }
    
    }
    public void Add()
    {
        SessionManager.getInstance().currentSession.basket.addGuest(); 
        Debug.Log(SessionManager.getInstance().currentSession.basket.currentGuest);
        guest[SessionManager.getInstance().currentSession.basket.currentGuest].SetActive(true);
        guestBascet[SessionManager.getInstance().currentSession.basket.currentGuest].SetActive(true);
        SelecButton(SessionManager.getInstance().currentSession.basket.currentGuest);
        
    }

    public void Select()
    {
        Debug.Log(index);
        SessionManager.getInstance().currentSession.basket.selectGuest(index);
         SelecButton(index);
         guestBascet[SessionManager.getInstance().currentSession.basket.currentGuest].SetActive(true);
     
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
    public void Remove()
    {    
        RemoveAllChildren(guestBascet[SessionManager.getInstance().currentSession.basket.currentGuest]);
        if(SessionManager.getInstance().currentSession.basket.removeGuest(SessionManager.getInstance().currentSession.basket.currentGuest))
        {

        guest[index].SetActive(false);

         
        SortByActiveState();
        ReSort();
        ReSortBuscet();
        gridLayoutGroup.GetComponent<GridLayoutGroup>().enabled = false;
        gridLayoutGroup.GetComponent<GridLayoutGroup>().enabled = true;
        SelecButton(SessionManager.getInstance().currentSession.basket.currentGuest);
        }

     
    }

    // Update is called once per frame
    
}
