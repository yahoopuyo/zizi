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
    ModeData md;
    	

    // Start is called before the first frame update
    void Start()
    {
        // playernum
	//playernum = 0;
	md = GameObject.Find("ModeData").GetComponent<ModeData>()
	playernum = md.player;
	nextplayernum = (playernum +1) % 4;
	nextnextplayernum = (playernum + 2) % 4;
	nextnextnextplayernum = (playernum + 3) % 4;
	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
