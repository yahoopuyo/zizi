﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn;
    public int turnPlayer;
    public int drawnPlayer;
    private GameObject hand;
    private Hands hands;

    private int CountWinners()
    {
        int winner = 0;
        for(int player = 0; player < 4; player++)
        {
            if (hands.hands[player].Count == 0) winner++;
        }
        return winner;
    }

    public void NextTurnPlayer()
    {
        hand = GameObject.Find("Hand");
        hands = hand.GetComponent<Hands>();
        int nextT = 0;
        switch (CountWinners())
        {
            case 0:
                nextT = (turnPlayer + 1) % 4;
                break;
            case 1:
                if (hands.hands[turnPlayer].Count == 0 || hands.hands[(turnPlayer + 1) % 4].Count == 0) nextT = (turnPlayer + 2) % 4;
                else nextT = (turnPlayer + 1) % 4;
                break;
            case 2:
                if (hands.hands[turnPlayer].Count == 0)
                {
                    if (hands.hands[(turnPlayer + 2) % 4].Count == 0 || hands.hands[(turnPlayer + 1) % 4].Count == 0) nextT = (turnPlayer + 3) % 4;
                    else nextT = (turnPlayer + 2) % 4;
                }
                else
                {
                    if (hands.hands[(turnPlayer + 1) % 4].Count == 0 && hands.hands[(turnPlayer + 2) % 4].Count == 0) nextT = (turnPlayer + 3) % 4;
                    if (hands.hands[(turnPlayer + 1) % 4].Count == 0 && hands.hands[(turnPlayer + 3) % 4].Count == 0) nextT = (turnPlayer + 2) % 4;
                    if (hands.hands[(turnPlayer + 2) % 4].Count == 0 && hands.hands[(turnPlayer + 3) % 4].Count == 0) nextT = (turnPlayer + 1) % 4;
                }
                break;
            default:
                for (int pl = 0; pl < 4; pl++) if (hands.hands[pl].Count != 0) nextT = pl;
                break;
        }
        turnPlayer = nextT;
    }

    public void NextDrawnPlayer()   //常にNextTurnPlayer()が先に呼び出されるようにする必要がある
    {
        int nextD = 0;
        hand = GameObject.Find("Hand");
        hands = hand.GetComponent<Hands>();
        switch (CountWinners())
        {
            case 0:
                nextD = (turnPlayer + 3) % 4;
                break;
            case 1:
                if (hands.hands[(turnPlayer + 3)%4].Count == 0) nextD = (turnPlayer + 2) % 4;
                else nextD = (turnPlayer + 3) % 4;
                break;
            case 2:
                if (hands.hands[(turnPlayer + 3)%4].Count == 0)
                {
                    if(hands.hands[(turnPlayer + 2)%4].Count == 0) nextD = (turnPlayer + 1) % 4;
                    else nextD = (turnPlayer + 2) % 4;
                }
                else nextD = (turnPlayer + 3) % 4;
                break;
            default:
                for (int pl = 0; pl < 4; pl++) if (hands.hands[pl].Count != 0) nextD = pl;
                break;

        }
        drawnPlayer = nextD;
    }

    void Start()
    {
        turn = 0;
        turnPlayer = 0;
        drawnPlayer = 3;
    }

    void Update()
    {

    }
}