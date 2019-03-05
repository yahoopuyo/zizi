using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerForOnePlayer : MonoBehaviour
{
    private bool flag = true;   //プレイヤーが一人のときは、true
    bool moveFlag = false; //処理待機(waitforsecond)中はtrue
    bool flashFlag = false; //処理待機(waitforsecond)中はtrue
    string cardName;
    private int turn;
    private int tP;
    private int dP;
    private int cardIndex;
    GameObject hand;
    GameObject card;
    Hands hands;
    TurnManager turnManager;
    Distribute distribute;
    public bool which;

    private void get()
    {
        //card = GameObject.Find("Card");
        turnManager = GetComponent<TurnManager>();
        turn = turnManager.turn;
        tP = turnManager.turnPlayer;
        dP = turnManager.drawnPlayer;
        hand = GameObject.Find("Hand"); //Handのクラスを取得
        hands = hand.GetComponent<Hands>();
    }

    IEnumerator DrawWithAnimation(int drawnPlayer)
    {
        cardIndex = draw(dP);
        cardName = "Card" + cardIndex;
        card = GameObject.Find(cardName);
        moveFlag = true;
        yield return new WaitForSeconds(0.5f);  //0.5秒待機
        moveFlag = false;
        if (hands.FindDeletedPair(cardIndex, tP) != 100)
        {
            flashFlag = true;
            int deleted = hands.FindDeletedPair(cardIndex, tP);
            cardName = "Card" + deleted;
            card = GameObject.Find(cardName);
            var color = card.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            card.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.2f);
            color.a = 1f;
            card.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.2f);
            color.a = 0;
            card.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.2f);
            color.a = 1f;
            card.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(1f);
            flashFlag = false;
        }
        hands.hands[dP].Remove(cardIndex); //引かれる人の手札配列からカードを削除
        hands.hands[tP].Add(cardIndex); //引いた人の手札配列にカードを追加
        hands.DeletePair((cardIndex % 13) + 1, tP);
        hands.ClickUpdate();
        distribute = hand.GetComponent<Distribute>();
        distribute.updateField();
        turnManager.NextTurnPlayer();
        turnManager.NextDrawnPlayer();
        turnManager.turnNext();
    }

    private int draw(int drawnPlayer)
    {
        int index = Random.Range(0, hands.hands[dP].Count);
        int cI = hands.hands[dP][index];

        return cI;
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
                    if (moveFlag || flashFlag) return;  //待機処理中にもう一回押された時に無効化
                    StartCoroutine(DrawWithAnimation(dP));
                    StopCoroutine(DrawWithAnimation(dP));
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (moveFlag)
        {
            card.transform.Translate(0, Time.deltaTime * 0.6f, 0);
        }

        //if (tP == 0)
        //{
        //    card.GetComponent<Click>().enabled = true;
        //    which = true;
        //}
        //else
        //{
        //card.GetComponent<Click>().enabled = false;
        //which = false;
        //}
        //↑これでできると思ったんだけど、なぜかできないので、clickを少しだけ動かした。
    }
}
