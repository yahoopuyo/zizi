using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiziKakuOnline : MonoBehaviour
{
    //private List<GameObject> guessListCard;
    private List<int> guessListIndex;
    private bool zizikakued;



    public bool InGuessList(int cardIndex)
    {
        return guessListIndex.Contains(cardIndex);
    }
    public bool UpdateGuessList(int cardIndex) //return true when it deesn't need to ToggleFace
    {
        if (zizikakued) return true;//もうzizi確済みなら無視する
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

    public void RemoveFromGuessList(int cardIndex1, int cardIndex2)
    {
        guessListIndex.Remove(cardIndex1);
        guessListIndex.Remove(cardIndex2);
    }
    
    //じじかくボタン
    public void OnClicked()
    {
        zizikakued = true;
        guessListIndex.Clear();
        PhotonView view = GetComponent<PhotonView>();
        //view.RPC("SendGuessList", PhotonTargets.All, turn,playernum);　 //turn とかplayernumは適当に取得しといて
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
