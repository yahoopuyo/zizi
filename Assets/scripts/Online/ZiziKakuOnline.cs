using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiziKakuOnline : MonoBehaviour
{
    //private List<GameObject> guessListCard;
    private List<int> guessListIndex;

    ScoreManagerOnline sm;
    TurnManagerOnline tm;
    ModeData md;

    [PunRPC]
    void SendGuessList()
    {
        guessListIndex.Insert(0, tm.turn + 100); //先頭にturn+100を挿入（100はziziと被らせないため)
        sm.zzkkList.Insert(md.player, guessListIndex);
    }

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
        PhotonView view = GetComponent<PhotonView>(); //GameObject.Find("GameManager")してないのが気になる
        //view.RPC("SendGuessList", PhotonTargets.All);
    }


    // Start is called before the first frame update
    void Start()
    {
        guessListIndex = new List<int>();
        sm = GetComponent<ScoreManagerOnline>();
        tm = GetComponent<TurnManagerOnline>();
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
