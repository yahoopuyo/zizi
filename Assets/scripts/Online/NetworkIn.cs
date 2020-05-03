using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkIn : MonoBehaviour
{
    ModeData md;
    DrawOnline draw;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.player.NickName);
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        draw = GetComponent<DrawOnline>();
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("on disconnected called");
        String outPlayerName = otherPlayer.NickName;
        Debug.Log(outPlayerName);
        if (md.numOfPlayer == 2 && outPlayerName == "1") draw.computerFlags[2] = true;
        if (md.numOfPlayer != 2 && outPlayerName == "1") draw.computerFlags[1] = true;
        if (outPlayerName == "2") draw.computerFlags[2] = true;
        if (outPlayerName == "3") draw.computerFlags[3] = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
