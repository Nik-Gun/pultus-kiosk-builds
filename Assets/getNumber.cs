using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class getNumber : MonoBehaviour
{
    
    public Text count,index_t;
    public GameObject Content;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Click()
    {
        GuesManager.index=transform.GetSiblingIndex();
        Debug.Log(transform.GetSiblingIndex());
    }
    // Update is called once per frame
    void Update()
    {
               count.text=Content.transform.childCount.ToString();
               index_t.text=""+transform.GetSiblingIndex().ToString();
    }
}
