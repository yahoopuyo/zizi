﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NetWork01 : MonoBehaviour
{
    [SerializeField] Text connectionText;
    [SerializeField] Transform spawnPoint;
    private GameObject hand;
    private bool In = false;
    private bool Loaded=false;
    public int player;
    ModeData md;
    int index;
    //void Start()
    //{
    //    PhotonNetwork.logLevel = PhotonLogLevel.Full;//情報を全部ください
    //    // Photonに接続する(引数でゲームのバージョンを指定できる)
    //    PhotonNetwork.ConnectUsingSettings(null);
    //}

    // ロビーに入ると呼ばれる
    //void OnJoinedLobby()
    //{
    //    Debug.Log("ロビーに入りました。");
    //    RoomOptions ro = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };//maxPlayerは人数の上限
    //    // ルームに入室する
    //    PhotonNetwork.JoinOrCreateRoom("myRoom", ro, TypedLobby.Default);
    //}

    //// ルームに入室すると呼ばれる
    //void OnJoinedRoom()
    //{
    //hand = GameObject.Find("Hand");
    //Debug.Log("ルームへ入室しました。");

    //}

    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings(null);
        md = GameObject.Find("ModeData").GetComponent<ModeData>();

    }

    // ロビーに入ると呼ばれる
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました。");
        RoomOptions ro = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };//maxPlayerは人数の上限
        // ルームに入室する
        PhotonNetwork.JoinOrCreateRoom("myRoom", ro, TypedLobby.Default);
    }

    // ルームに入室すると呼ばれる
    void OnJoinedRoom()
    {
        Debug.Log("ルームへ入室しました。");
        //GameObject player = PhotonNetwork.Instantiate("Card", spawnPoint.position, spawnPoint.rotation, 0);
        if (PhotonNetwork.playerList.Length == 1)
        {
            md.player = 0;
        }

        else if (PhotonNetwork.playerList.Length == 2) md.player = 1;
        else if (PhotonNetwork.playerList.Length == 3) md.player = 2;
        else md.player = 3;
    }

    void OnClickStart()
    {
        
    }

    [PunRPC]
    void Load(int a)
    {
        md.numOfPlayer = PhotonNetwork.playerList.Length; //仮にこうしている
        SceneManager.LoadScene("photon_in");
    }

    void LoadScene()
    {
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("Load", PhotonTargets.All,1);
    }


    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();

        if(!In && PhotonNetwork.playerList.Length == md.numOfPlayer)
        {
            In = true;
            //numOfPlayer = PhotonNetwork.playerList.Length; //仮にこうしている
        }

        if(!Loaded && In)
        {
            Loaded = true;
            SceneManager.LoadScene("photon_in");
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (md.player == 0)
            {
                
            }
        }
    }
    // ルームの入室に失敗すると呼ばれる
    //void OnPhotonRandomJoinFailed()
    //{
    //    Debug.Log("ルームの入室に失敗しました。");

    //    // ルームがないと入室に失敗するため、その時は自分で作る
    //    // 引数でルーム名を指定できる
    //    PhotonNetwork.CreateRoom("myRoomName");
    //}
}
