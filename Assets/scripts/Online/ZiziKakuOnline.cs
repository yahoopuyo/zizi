using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiziKakuOnline : MonoBehaviour
{
    //private List<GameObject> guessListCard;
    private List<int> guessListIndex;
    ScoreManagerOnline sm;

    [PunRPC]
    void SendGuessList(int turn, int playernum)
    {
        guessListIndex.Insert(0, turn + 100); //先頭にturn+100を挿入（100はziziと被らせないため)
        sm.zzkkList.Insert(playernum, guessListIndex);
    }

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

    public void RemoveFromGuessList(int cardIndex)
    {
        guessListIndex.Remove(cardIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        guessListIndex = new List<int>();
        sm = GetComponent<ScoreManagerOnline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
