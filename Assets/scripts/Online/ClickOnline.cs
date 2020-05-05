using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon;

public class ClickOnline : Photon.MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitH​​andler
{
    private GameObject hand;
    private GameObject gameManager;
    DrawOnline draw;
    private TurnManagerOnline turnManager;
    private HandsOnline hands;
    private Distribute distribute;
    CardModel cardModel;
    int preDrawnPlayer;
    int drawnCard;
    int cardIndex;
    int turnPlayer;
    int drawnPlayer;
    int player;

    void get()
    {
        gameManager = GameObject.Find("GameManager");
        draw = gameManager.GetComponent<DrawOnline>();
        turnManager = gameManager.GetComponent<TurnManagerOnline>();
        turnPlayer = turnManager.turnPlayer;
        drawnPlayer = turnManager.drawnPlayer;
        preDrawnPlayer = turnManager.preDrawnPlayer;
        drawnCard = turnManager.drawnCard;
        hand = GameObject.Find("Hand"); //Handのクラスを取得
        hands = hand.GetComponent<HandsOnline>();
        cardModel = GetComponent<CardModel>();
        cardIndex = cardModel.cardIndex; //カードモデルからcardIndexを取得
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        get();

        if (drawnCard == cardIndex && preDrawnPlayer == player && drawnCard != hands.GetGrave()[-1] && drawnCard != hands.GetGrave()[-2]) //前ひかれたカードだったら
        {
            cardModel.ToggleFace(true);
            Debug.Log("selected");
        }
        if (drawnPlayer == hands.Cardownerreturn(cardIndex))
        {
            var v = new Vector2(0, 0.4f);
            transform.Translate(v);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        get();
        if (drawnCard == cardIndex && preDrawnPlayer == player) //前ひかれたカードだったら
        {
            cardModel.ToggleFace(false);
            Debug.Log("selected");
        }
        if (drawnPlayer == hands.Cardownerreturn(cardIndex))
        {
            var v = new Vector2(0, -0.4f);
            transform.Translate(v);
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 0) //クリック回数>0の時  一応残すけどこのままいくならif文とっても良いはず
        {
            get();

            if (turnPlayer != player) return;
            if (drawnPlayer == hands.Cardownerreturn(cardIndex))
            {
                //hands.hands[drawnPlayer].Remove(cardIndex); //引かれる人の手札配列からカードを削除
                //hands.hands[turnPlayer].Add(cardIndex); //引いた人の手札配列にカードを追加
                //hands.DeletePair((cardIndex % 13) + 1,turnPlayer);
                //hands.ClickUpdate();
                //distribute = hand.GetComponent<Distribute>();
                //distribute.updateField();
                //turnManager.NextTurnPlayer();
                //turnManager.NextDrawnPlayer();
                //turnManager.turnNext();
                if (draw.moveFlag || draw.flashFlag) return;
                draw.drawWithAnimation(drawnPlayer, cardIndex, turnPlayer);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ModeData").GetComponent<ModeData>().player;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
