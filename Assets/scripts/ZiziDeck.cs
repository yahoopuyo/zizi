using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ZiziDeck : UnityEngine.MonoBehaviour
{
    private List<int> cards;
    private int zizi;
    private int seed;
    ModeData md;
    public List<int> GetCards()
    {
        return cards;
    }

    public int GetZizi()
    {
        return zizi;
    }

    public void Shuffle()
    {
        Random.InitState(seed);
        if (cards == null)
        {
            cards = new List<int>();
        }
        else
        {
            cards.Clear();
        }

        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }
        int n = cards.Count;
        while (n > 0)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int tmp = cards[k];
            cards[k] = cards[n];
            cards[n] = tmp;

        }
        zizi = cards[51];
        cards.RemoveAt(51);
        
    }

    [PunRPC]
    void SendSeed(int num)
    {
        Random.InitState(num);
    }

    void Start()
    {
        Shuffle();
        seed = Random.Range(0, 10000);
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        if(md.player == 0)
        {
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("SendSeed", PhotonTargets.All, seed);
        }
                        // メソッド名
        
    }

    
}
