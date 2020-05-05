using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNum : MonoBehaviour
{
    public int playernum;
    public int nextplayernum;
    public int nextnextplayernum;
    public int nextnextnextplayernum;
    private string[] playerInfo;
    ModeData md;
    Text text0;
    Text text1;
    Text text2;
    Text text3;
    	

    // Start is called before the first frame update
    void Start()
    {
        // playernum
	    //playernum = 0;
	    md = GameObject.Find("ModeData").GetComponent<ModeData>();
	    playernum = md.player;
	    nextplayernum = (playernum +1) % 4;
	    nextnextplayernum = (playernum + 2) % 4;
	    nextnextnextplayernum = (playernum + 3) % 4;
	    text0 = GameObject.Find("yourPlayerNum").GetComponent<Text>();
	    text1 = GameObject.Find("nextPlayerNum").GetComponent<Text>();
	    text2 = GameObject.Find("nextnextPlayerNum").GetComponent<Text>();
	    text3 = GameObject.Find("nextnextnextPlayerNum").GetComponent<Text>();
        

        /*
        text0.text = "Player" + playernum.ToString();
        text1.text = "Player" + nextplayernum.ToString();
        text2.text = "Player" + nextnextplayernum.ToString();
        text3.text = "Player" + nextnextnextplayernum.ToString();
        */
    }

        // Update is called once per frame
    void Update()
    {
        playerInfo = md.playerInfo;
        playernum = md.player;
        nextplayernum = (playernum + 1) % 4;
        nextnextplayernum = (playernum + 2) % 4;
        nextnextnextplayernum = (playernum + 3) % 4;
        text0.text = "Player" + playerInfo[playernum];
        text1.text = "Player" + playerInfo[nextplayernum];
        text2.text = "Player" + playerInfo[nextnextplayernum];
        text3.text = "Player" + playerInfo[nextnextnextplayernum];
    }

}
