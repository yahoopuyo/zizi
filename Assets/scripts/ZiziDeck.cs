using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using System.Globalization;

public class ZiziDeck : UnityEngine.MonoBehaviour
{
    private List<int> cards;
    private int zizi;
    private int seed;
    public bool shared=false;
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
        //Random.InitState(seed);
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
    void SendSeed(int num,int numOfPlayer)
    {
        md.numOfPlayer = numOfPlayer;
        Random.InitState(num);
        Shuffle();
        shared = true;
        Debug.Log(num);
        if (numOfPlayer == 2 && md.player == 1) md.player = 2;//仮の処理
    }

    void Start()
    {
        Debug.Log("zizideck called");
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        if (!md.IsSolo() && md.player == 0)
        {
            seed = Random.Range(0, 10000);
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("SendSeed", PhotonTargets.All, seed,md.numOfPlayer);
        }
        else if (md.IsSolo())Shuffle();

    }

    
}
