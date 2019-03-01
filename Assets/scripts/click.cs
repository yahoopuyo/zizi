using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour , IPointerClickHandler
{
    private GameObject hand;
    private GameObject gameManager;
    private TurnManager turnManager;
    private Hands hands;
    private Distribute distribute;
    CardModel cardModel;
    int cardIndex;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 1) //クリック回数>1の時
        {
            gameManager = GameObject.Find("GameManager");
            turnManager = gameManager.GetComponent<TurnManager>();
            int turnPlayer = turnManager.turnPlayer;
            int drawnPlayer = turnManager.drawnPlayer;
            hand = GameObject.Find("Hand"); //Handのクラスを取得
            hands = hand.GetComponent<Hands>();

    //        Debug.Log(eventData.clickCount); //クリックされた回数を表示

            cardModel = GetComponent<CardModel>();
            cardIndex = cardModel.cardIndex; //カードモデルからcardIndexを取得

            if(drawnPlayer == hands.Cardownerreturn(cardIndex))
            {
                transform.Rotate(0, 0, 90);  //カードを90度回転
                hands.hands[drawnPlayer].Remove(cardIndex); //引かれる人の手札配列からカードを削除
                hands.hands[turnPlayer].Add(cardIndex); //引いた人の手札配列にカードを追加

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
