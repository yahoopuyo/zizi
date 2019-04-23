using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerDrawAnimation : MonoBehaviour
{
    CardModel cardModel;
    CardMover mover;

    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        cardModel = card.GetComponent<CardModel>();
        mover = card.GetComponent<CardMover>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(400, 10, 100, 20), "movecard"))
        {
            mover.moveCard();
        }
    }
   
}
