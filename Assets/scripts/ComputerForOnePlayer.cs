using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerForOnePlayer : MonoBehaviour
{
    private bool flag = true;   //プレイヤーが一人のときは、true
    private int turn;
    private int tP;
    private int dP;
    GameObject hand;
    GameObject card;
    Hands hands;
    Click click;
    TurnManager turnManager;
    Distribute distribute;
    public bool which;

    private void get()
    {
        card = GameObject.Find("Card");
        click = card.GetComponent<Click>();
        turnManager = GetComponent<TurnManager>();
        turn = turnManager.turn;
        tP = turnManager.turnPlayer;
        dP = turnManager.drawnPlayer;
        hand = GameObject.Find("Hand"); //Handのクラスを取得
        hands = hand.GetComponent<Hands>();
    }

    private void draw()
    {

    }
    void Start()
    {
        //if(player number != 1)   flag = false　的な感じ？
    }

    void OnGUI()
    {
        if (flag)
        {
            if (GUI.Button(new Rect(300, 10, 100, 20), "CPU turn draw"))
            {
                get();
                if (tP != 0)
                {
                    int index = Random.Range(0, hands.hands[dP].Count);
                    int cardIndex = hands.hands[dP][index];
                    hands.hands[dP].Remove(cardIndex); //引かれる人の手札配列からカードを削除
                    hands.hands[tP].Add(cardIndex); //引いた人の手札配列にカードを追加

                    hands.Delete();
                    hands.ClickUpdate();
                    distribute = hand.GetComponent<Distribute>();
                    distribute.updateField();
                    turnManager.NextTurnPlayer();
                    turnManager.NextDrawnPlayer();
                    turnManager.turnNext();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //get();
        //if (tP == 0) click.enabled = true;
        //else click.enabled = false;
        //↑これでできると思ったんだけど、なぜかできないので、clickを少しだけ動かした。
    }
}
