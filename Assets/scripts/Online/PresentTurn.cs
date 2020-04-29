using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentTurn : MonoBehaviour
{
    public int presentturnnum;
    public int presentturnplayer;
    TurnManagerOnline tmo;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        tmo = GameObject.Find("GameManager").GetComponent<TurnManagerOnline>();

	  
    }

    // Update is called once per frame
    void Update()
    {
        
	  presentturnnum = tmo.turn;
	  presentturnplayer = tmo.turnPlayer;
	  text = GameObject.Find("presentPlayerNum").GetComponent<Text>();
	  text.text = "Turn Player = Player" + presentturnplayer.ToString();
    }
}
