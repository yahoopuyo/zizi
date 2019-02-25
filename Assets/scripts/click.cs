using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class click : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 1) //クリック回数>1の時
        {
            Debug.Log(eventData.clickCount); //クリックされた回数を表示
            transform.Rotate(0, 0, 90);  //90度回転、本当はここでカード移動させたいね
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
