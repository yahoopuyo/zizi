using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWork01 : MonoBehaviour
{
    [SerializeField] Text connectionText;
    [SerializeField] Transform spawnPoint;
    private GameObject hand;
    private bool In = false;
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
            index = 0;
            hand = GameObject.Find("Hand");
            //hand = PhotonNetwork.Instantiate("Hand", new Vector3(0f, 0f, 0f), new Quaternion(), 0);
            //hand.name = "Hand";
            hand.GetComponent<DistributeForAll>().StartGame();
        }
        //else { index = 1; hand = GameObject.Find("Hand"); }
    }

    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
        if(!In && PhotonNetwork.playerList.Length == 2)
        {
            In = true;
            //hand.GetComponent<DistributeForAll>().StartGame();
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
