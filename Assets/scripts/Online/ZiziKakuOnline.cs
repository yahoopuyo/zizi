using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiziKakuOnline : MonoBehaviour
{
    //private List<GameObject> guessListCard;
    private List<int> guessListIndex;




    public bool UpdateGuessList(int cardIndex) //return true when it deesn't need to ToggleFace
    {
        if (guessListIndex.Contains(cardIndex))
        {
            guessListIndex.Remove(cardIndex);
            return false;
        }
        else
        {
            if (guessListIndex.Count > 5) return true;
            guessListIndex.Add(cardIndex);
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        guessListIndex = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
