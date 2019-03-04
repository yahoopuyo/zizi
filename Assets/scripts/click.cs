using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitH​​andler
{
    private GameObject hand;
    private GameObject gameManager;
    private TurnManager turnManager;
    private Hands hands;
    private Distribute distribute;
    CardModel cardModel;
    int cardIndex;
    int turnPlayer;
    int drawnPlayer;

    void get()
    {
        gameManager = GameObject.Find("GameManager");
        turnManager = gameManager.GetComponent<TurnManager>();
        turnPlayer = turnManager.turnPlayer;
        drawnPlayer = turnManager.drawnPlayer;
        hand = GameObject.Find("Hand"); //Handのクラスを取得
        hands = hand.GetComponent<Hands>();
        cardModel = GetComponent<CardModel>();
        cardIndex = cardModel.cardIndex; //カードモデルからcardIndexを取得
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        get();

        if (drawnPlayer == hands.Cardownerreturn(cardIndex))
        {
            var v = new Vector2(0, 0.4f);
            transform.Translate(v);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        get();

        if (drawnPlayer == hands.Cardownerreturn(cardIndex))
        {
            var v = new Vector2(0,-0.4f);
            transform.Translate(v);
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 0) //クリック回数>0の時  一応残すけどこのままいくならif文とっても良いはず
        {
            get();

            if (turnPlayer != 0) return;    //randomCPUzizi用
            if (drawnPlayer == hands.Cardownerreturn(cardIndex))
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
