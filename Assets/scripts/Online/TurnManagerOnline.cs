using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagerOnline : MonoBehaviour
{
    public int turn;
    public int turnPlayer;
    public int drawnPlayer;
    public int drawnCard;
    public int preDrawnPlayer;
    InitCanvas init;
    private List<int> Wins = new List<int>();
    public List<string> result = new List<string>();
    private GameObject hand;
    private HandsOnline hands;
    private GameObject textyou;
    private GameObject textnpn;
    private GameObject textnnpn;
    private GameObject textnnnpn;

    private int CountWinners()
    {
        int winner = 0;
        for (int player = 0; player < 4; player++)
        {
            if (hands.hands[player].Count == 0) winner++;
        }
        return winner;
    }

    public void NextTurnPlayer()
    {
        hand = GameObject.Find("Hand");
        hands = hand.GetComponent<HandsOnline>();
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
        hands = hand.GetComponent<HandsOnline>();
        switch (CountWinners())
        {
            case 0:
                nextD = (turnPlayer + 3) % 4;
                break;
            case 1:
                if (hands.hands[(turnPlayer + 3) % 4].Count == 0) nextD = (turnPlayer + 2) % 4;
                else nextD = (turnPlayer + 3) % 4;
                break;
            case 2:
                if (hands.hands[(turnPlayer + 3) % 4].Count == 0)
                {
                    if (hands.hands[(turnPlayer + 2) % 4].Count == 0) nextD = (turnPlayer + 1) % 4;
                    else nextD = (turnPlayer + 2) % 4;
                }
                else nextD = (turnPlayer + 3) % 4;
                break;
            default:
                for (int pl = 0; pl < 4; pl++) if (hands.hands[pl].Count != 0) nextD = pl;
                break;

        }
        preDrawnPlayer = drawnPlayer;
        drawnPlayer = nextD;
    }

    public void turnNext(int cardIndex)
    {
        turn++;
        drawnCard = cardIndex;
        if (CountWinners() > Wins.Count)
        {
            for (int pl = 0; pl < 4; pl++)
            {
                if (hands.hands[pl].Count == 0 && !Wins.Contains(pl))
                {
                    result.Add("player" + pl + " was " + CountWinners());
                    Wins.Add(pl);
                }
            }
        }
        if (turnPlayer == drawnPlayer)
        {
            result.Add("player" + turnPlayer + " losed");
            init = GetComponent<InitCanvas>();
            init.gameoverP.SetActive(true);
            string Order;
            Order = result[0] + "st\n\n" + result[1] + "nd\n\n" + result[2] + "rd\n\n" + result[3];
            Text text = GameObject.Find("Results").GetComponent<Text>();
            text.text = Order;
		textyou.SetActive(false);
		textnpn.SetActive(false);
		textnnpn.SetActive(false);
		textnnnpn.SetActive(false);
        }
    }
    void Start()
    {
        turn = 0;
        turnPlayer = 0;
        drawnPlayer = 3;
        drawnCard = 100;
        preDrawnPlayer = 100;
	GameObject textyou = GameObject.Find("yourPlayerNum");	
	GameObject textnpn = GameObject.Find("nextPlayerNum");
	GameObject textnnpn = GameObject.Find("nextnextPlayerNum");
	GameObject textnnnpn = GameObject.Find("nextnextnextPlayerNum");
	textyou.SetActive(true);
	textnpn.SetActive(true);
	textnpn.SetActive(true);
	textnpn.SetActive(true);
    }

    void Update()
    {

    }
}
