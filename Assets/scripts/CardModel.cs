using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite[] cardBack;

    public int cardIndex; //faces[cardIndex]
    public int backIndex;  //cardBack[Original]

    public bool debug;  //if true, show the face of all the cards
    public void ToggleFace(bool showFace)
    {
        if (showFace | debug)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack[backIndex];
        }
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
