using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiziDeck : MonoBehaviour
{
    private List<int> cards;
    private int zizi;
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
    void Awake()
    {
        Shuffle();
    }
}
