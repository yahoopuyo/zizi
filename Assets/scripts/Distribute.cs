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
    private Vector3 start1 = new Vector3(8, -3.5f);
    private Vector3 start2 = new Vector3(4f, 3.5f);
    private Vector3 start3 = new Vector3(-8f, 3.5f);
    private List<GameObject> sources;
    void distribute()
    {
        int cardCount = 0;
        foreach(int i in hands.Gethand0())
        {
            GameObject cardCopy = Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start0 + new Vector3(co, 0f);
            cardModel.backIndex = 0;
            cardModel.cardIndex = i;

            sources.Add(cardCopy);

            cardCopy.transform.position = temp;
            cardModel.ToggleFace(true);

            spriteRenderer.sortingOrder = cardCount;

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand1())
        {
            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start1 + new Vector3(0f, co);
            cardModel.backIndex = 1;
            cardModel.cardIndex = i;

            sources.Add(cardCopy);

            cardCopy.transform.position = temp;
            cardCopy.transform.Rotate(new Vector3(0f, 0f, 90f));
            cardModel.ToggleFace(false);

            spriteRenderer.sortingOrder = cardCount;

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand2())
        {
            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start2 - new Vector3(co, 0f);
            cardModel.backIndex = 2;
            cardModel.cardIndex = i;

            sources.Add(cardCopy);

            cardCopy.transform.position = temp;
            cardModel.ToggleFace(false);

            spriteRenderer.sortingOrder = cardCount;

            cardCount++;
        }
        cardCount = 0;
        foreach (int i in hands.Gethand3())
        {
            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            Vector3 temp;
            float co = cardOffset * cardCount;

            temp = start3 - new Vector3(0f, co);
            cardModel.backIndex = 3;
            cardModel.cardIndex = i;

            sources.Add(cardCopy);

            cardCopy.transform.position = temp;
            cardCopy.transform.Rotate(new Vector3(0f, 0f, 270f));
            cardModel.ToggleFace(true);

            spriteRenderer.sortingOrder = cardCount;

            cardCount++;
        }
    }

    private void initSources()
    {
        if (sources == null) sources = new List<GameObject>();
        else
        {
            foreach (GameObject source in sources)
            {
                Destroy(source); //今表示しているgameobjectのを消す。
            }
            sources.Clear();

        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(120, 10, 100, 20), "distribute"))
        {
            initSources();
            hands = GetComponent<Hands>();
            distribute();
        }
    }
    public void updateField()
    {
        initSources();
        hands = GetComponent<Hands>();
        distribute();
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
