using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class click : MonoBehaviour , IPointerClickHandler
{
    private GameObject hand;
    private Hands hands;
    private Distribute distribute;
    CardModel cardModel;
    int cardIndex;
    int nowowner; //引かれる前のowner
    int turnplayer;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 1) //クリック回数>1の時
        {
            hand = GameObject.Find("Hand"); //Handのクラスを取得
            hands = hand.GetComponent<Hands>();

            Debug.Log(eventData.clickCount); //クリックされた回数を表示
            transform.Rotate(0, 0, 90);  //カードを90度回転

            cardModel = GetComponent<CardModel>();
            cardIndex = cardModel.cardIndex; //カードモデルからcardIndexを取得

            nowowner = hands.Cardownerreturn(cardIndex); //Hand.csのCardownerreturnを使って今の持ち主を取得
            turnplayer = nowowner + 1; //nowownerを使ってturnplayerを取得、本当はゲーム全体でturnplayerを決めておくべきかも
            if (turnplayer == 4) turnplayer = 0;

            hands.hands[nowowner].Remove(cardIndex); //引かれる人の手札配列からカードを削除
            hands.hands[turnplayer].Add(cardIndex); //引いた人の手札配列にカードを追加

            hands.Delete();
            distribute = hand.GetComponent<Distribute>();
            distribute.updateField();
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
