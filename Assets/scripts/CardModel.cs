using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite[] cardBack;

    public int cardIndex; //faces[cardIndex]
    public int backIndex;  //cardBack[Original]

    public void ToggleFace(bool showFace)
    {
        if (showFace)
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
