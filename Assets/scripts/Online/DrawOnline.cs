using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnline : MonoBehaviour
{
    public bool flag = true;   //プレイヤーが一人のときは、true
    public bool moveFlag = false; //処理待機(waitforsecond)中はtrue
    public bool flashFlag = false; //処理待機(waitforsecond)中はtrue
    string cardName;
    private int turn;
    private int tP;
    private int dP;
    private int level;
    private int player;
    public int numOfPlayer;
    public int numOfComs;

    GameObject hand;
    GameObject card;
    ComputerOnline[] coms = new ComputerOnline[4];

    HandsOnline hands;
    TurnManagerOnline turnManager;
    RecordOnline record;
    DistributeForAll distribute;
    public bool which;

    private void get()
    {
        //card = GameObject.Find("Card");
        turnManager = GetComponent<TurnManagerOnline>();
        record = GetComponent<RecordOnline>();
        turn = turnManager.turn;
        tP = turnManager.turnPlayer;
        dP = turnManager.drawnPlayer;
        hand = GameObject.Find("Hand"); //Handのクラスを取得
        hands = hand.GetComponent<HandsOnline>();
        for (int i = 0; i < numOfComs; i++)
        {
            coms[numOfPlayer + i] = GameObject.Find("Com" + (numOfPlayer + i)).GetComponent<ComputerOnline>();
            //Debug.Log("Com" + (i + 1));
        }
    }

    IEnumerator DrawWithAnimation(int drawnPlayer, int cardIndex, int turnPlayer)
    {
        get();
        cardName = "Card" + cardIndex;
        card = GameObject.Find(cardName);
        moveFlag = true;
        yield return new WaitForSeconds(0.5f);  //0.5秒待機
        moveFlag = false;
        int deleted = hands.FindDeletedPair(cardIndex, turnPlayer);
        if (deleted != 100)
        {
            flashFlag = true;   //アニメーション最中のアクション無効化用

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

            record.updateRecordPaired(turn + 1, cardIndex, deleted);    //棋譜操作
            record.updateInfoPaired(cardIndex, deleted);    //プライベート情報操作
            flashFlag = false;
        }
        else
        {
            record.updateRecordUnpaired(turn + 1, cardIndex, turnPlayer);  //棋譜操作
            record.updateInfoUnpaired(turnPlayer, cardIndex);   //プライベート情報操作
        }
        hands.hands[dP].Remove(cardIndex); //引かれる人の手札配列からカードを削除
        hands.hands[tP].Add(cardIndex); //引いた人の手札配列にカードを追加
        hands.DeletePair((cardIndex % 13) + 1, turnPlayer);
        hands.ClickUpdate();
        distribute = hand.GetComponent<DistributeForAll>();
        distribute.updateField();
        turnManager.NextTurnPlayer();
        turnManager.NextDrawnPlayer();
        turnManager.turnNext(cardIndex);
        int deletedUniform;
        if (deleted == 100) deletedUniform = -1;
        else deletedUniform = record.Uniform.IndexOf(deleted);
        for (int cn = 0; cn < numOfComs; cn++)
        {
            coms[cn + numOfPlayer].load(dP, record.Uniform.IndexOf(cardIndex), tP, deletedUniform);
        }
    }


    [PunRPC]
    void SendAction(int[] drawData)
    {
        int drawnPlayer = drawData[0];
        int cardIndex = drawData[1];
        int turnPlayer = drawData[2];
        StartCoroutine(DrawWithAnimation(drawnPlayer, cardIndex, turnPlayer));
        StopCoroutine(DrawWithAnimation(drawnPlayer, cardIndex, turnPlayer));
    }

    void Send(int drawnPlayer, int drawncard, int turnPlayer)
    {
        int[] data = new int[3]{ drawnPlayer, drawncard, turnPlayer};
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("SendAction", PhotonTargets.All, data);
    }

    public void drawWithAnimation(int drawnPlayer, int cardIndex, int turnPlayer)
    {
        Send(drawnPlayer, cardIndex, turnPlayer);
    }


    void Start()
    {
        ModeData modeData = GameObject.Find("ModeData").GetComponent<ModeData>();
        flag = modeData.GetComponent<ModeData>().IsSolo(); //　的な感じ？
        level = modeData.computerLevel;
        player = modeData.player;
        numOfPlayer = modeData.numOfPlayer;
        numOfComs = 4 - numOfPlayer;
    }

    //void OnGUI()
    //{
    //    if (flag)
    //    {
    //        if (GUI.Button(new Rect(300, 10, 100, 20), "CPU turn draw"))
    //        {
    //            get();
    //            if (tP != 0)
    //            {
    //                if (moveFlag || flashFlag) return;  //待機処理中にもう一回押された時に無効化
    //                drawWithAnimation(dP,draw(dP),tP);
    //            }
    //        }
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        if (moveFlag)
        {
            card.transform.Translate(0, Time.deltaTime * 0.6f, 0);
        }

        if (true) //ここは、computer existsの時、とそのうちする
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                get();
                if (tP >= numOfPlayer && player == 0) //master player のみがコンピューター操作できる、
                {
                    if (moveFlag || flashFlag) return;  //待機処理中にもう一回押された時に無効化
                    int drawncard= record.Uniform[coms[tP].draw(dP)];
                    drawWithAnimation(dP, drawncard, tP);
                }
            }
        }
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
