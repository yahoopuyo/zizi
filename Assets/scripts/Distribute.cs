using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hands))]
public class Distribute : MonoBehaviour
{
    Hands hands;
    public float cardOffset;
    public GameObject cardPrefab;
    private Vector3 start0 = new Vector3(-4f, -3.5f);
    private Vector3 start1 = new Vector3(6f, -3.5f);
    private Vector3 start2 = new Vector3(4f, 3.5f);
    private Vector3 start3 = new Vector3(-6f, 3.5f);
    void distribute()
    {
        int cardCount = 0;
        foreach(int i in hands.Gethand0())
        {
            GameObject cardCopy = Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start0 + new Vector3(co, 0f);
            cardModel.backIndex = 0;
            cardModel.cardIndex = i;

            cardCopy.transform.position = temp;
            cardModel.ToggleFace(true);

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand1())
        {
            GameObject cardCopy = Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start1 + new Vector3(0f, co);
            cardModel.backIndex = 1;
            cardModel.cardIndex = i;

            cardCopy.transform.position = temp;
            cardCopy.transform.Rotate(new Vector3(0f, 0f, 90f));
            cardModel.ToggleFace(false);

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand2())
        {
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start2 - new Vector3(co, 0f);
            GameObject cardCopy = PhotonNetwork.Instantiate("Card",temp,Quaternion.identity,0);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.backIndex = 2;
            cardModel.cardIndex = i;

            cardCopy.transform.position = temp;
            cardModel.ToggleFace(false);

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand3())
        {
            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start3 - new Vector3(0f, co);
            cardModel.backIndex = 3;
            cardModel.cardIndex = i;

            cardCopy.transform.position = temp;
            cardCopy.transform.Rotate(new Vector3(0f, 0f, 270f));
            cardModel.ToggleFace(false);

            cardCount++;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(120, 10, 100, 20), "distribute"))
        {
            hands = GetComponent<Hands>();
            distribute();
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