using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZiziDeck))]
public class Hands : MonoBehaviour
{
    ZiziDeck deck;
    public List<int>[] hands;
    private List<int> grave;
    private int k = 0;
    public List<int> Gethand0()
    {
        return hands[0];
    }
    public List<int> Gethand1()
    {
        return hands[1];
    }
    public List<int> Gethand2()
    {
        return hands[2];
    }
    public List<int> Gethand3()
    {
        return hands[3];
    }

    public List<int> GetGrave()
    {
        return grave;
    }
    public void DeletePair(int num,int player)  //num is 1~13
    {
        int count=0;
        int removed=0;
        int[] numbers = { num - 1, num + 12, num + 25, num + 38 };
        foreach (int card in hands[player])
        {
            if (card % 13 == num - 1) count++;
        }
        foreach(int i in numbers)
        {
            if (removed < 2 && count > 1)
            {
                if (hands[player].Remove(i))
                {
                    grave.Add(i);
                    removed++;
                }
            }
        }
    }

    private void FirstDelete()
    {
        for(int players =0; players < 4; players++)
        {
            for (int a = 1; a < 14; a++) DeletePair(a, players);
            for (int a = 1; a < 14; a++) DeletePair(a, players);
        }
    }
    void CardList()
    {
        if (hands[0] == null) hands[0] = new List<int>();
        else hands[0].Clear();
        if (hands[1] == null) hands[1] = new List<int>();
        else hands[1].Clear();
        if (hands[2] == null) hands[2] = new List<int>();
        else hands[2].Clear();
        if (hands[3] == null) hands[3] = new List<int>();
        else hands[3].Clear();

        foreach (int l in deck.GetCards())
        {
            if (k >= 0 && k < 13) hands[0].Add(l);
            else if (k > 12 && k < 26) hands[1].Add(l);
            else if (k > 25 && k < 39) hands[2].Add(l);
            else hands[3].Add(l);
            k++;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        deck = GetComponent<ZiziDeck>();
        hands = new List<int>[4];
        grave = new List<int>();
        CardList();
        FirstDelete();
    }

    // Update is called once per frame

}

