using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NetWork01 : MonoBehaviour
{
    [SerializeField] Text connectionText;
    [SerializeField] Transform spawnPoint;
    private GameObject hand;
    private bool In;
    public int player;
    ModeData md;

    Text roomhost;
    RoomName push1;
    RoomMake push2;
    public int players;
    Text numplayers;
    Dropdown dd;
    Text dlabel;
    RoomInfo[] rooms;
    Guest push3;
    BackButton push4;
    BackToMenu push5;
    private bool bmenu;


    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        if (PhotonNetwork.connected) PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings(null);
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        roomhost = GameObject.Find("InputFieldText").GetComponent<Text>();
        push1 = GameObject.Find("OkButton").GetComponent<RoomName>();
        push2 = GameObject.Find("StartButton").GetComponent<RoomMake>();
        numplayers = GameObject.Find("NumOfPlayers").GetComponent<Text>();
        dd = GameObject.Find("RoomDropdown").GetComponent<Dropdown>();
        dlabel = GameObject.Find("DropdownLabel").GetComponent<Text>();
        push3 = GameObject.Find("RoomDropdown").GetComponent<Guest>();
        push4 = GameObject.Find("BackButton").GetComponent<BackButton>();
        push5 = GameObject.Find("BackToMenuButton").GetComponent<BackToMenu>();
        DontDestroyOnLoad(this);
    }

    // ロビーに入ると呼ばれる
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました。");
    }

    //ルームリストが更新されると呼ばれる
    void OnReceivedRoomListUpdate()
    {
        //dd.optionsをリセット
        dd.options = new List<Dropdown.OptionData>();
        dd.options.Add(new Dropdown.OptionData { text = "Select Room" });
        dlabel.text = "Select Room";

        rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            Debug.Log("Roomがありません");
        }

        else
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                dd.options.Add(new Dropdown.OptionData { text = rooms[i].name });
            }
        }
    }

    // ルームに入室すると呼ばれる
    void OnJoinedRoom()
    {
        In = true;
        Debug.Log("ルームへ入室しました。");
    }

    //ルームの入室に失敗したら呼ばれる、ロビーに戻す
    void OnPhotonRandomJoinFailed()
    {
        SceneManager.LoadScene("photontest1");
        Debug.Log("ルームの入室に失敗しました。");
    }

    //接続が切れたらよばれる
    void OnDisconnectedFromPhoton()
    {
        Debug.Log("接続が切れました");
        if (bmenu)
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(this);
        }
        else SceneManager.LoadScene("photontest1");
    }

    void OnClickStart()
    {

    }

    /*
    [PunRPC] //ここはphoton_inに書くことになるかも、実験するためにはビルド必要
    void Load()
    {
        PhotonNetwork.Disconnect();
    }

    void LoadScene()
    {
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("Load", PhotonTargets.Others);
    }
    */


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "photontest1") connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();

        // ルーム内のプレーヤー数
        players = PhotonNetwork.playerList.Length;
        numplayers.text = "Players are  " + players + "/4";

        if (push1.hostpush1) //ホストがルーム作った瞬間、入室
        {
            push1.hostpush1 = false;
            md.roomName = roomhost.text;
            RoomOptions ro = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };//maxPlayerは人数の上限                                                      
            PhotonNetwork.JoinOrCreateRoom(md.roomName, ro, TypedLobby.Default);
        }

        if (push2.hostpush2 && In) //ホストが決定した
        {
            push2.hostpush2 = false;
            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.IsVisible = false;
            md.isHost = true;
            md.player = 0;
            md.numOfPlayer = players;
            SceneManager.LoadScene("photon_in");
        }

        if (push3.guestpush) //ゲストがルームに入る瞬間
        {
            push3.guestpush = false;
            md.roomName = dlabel.text;
            PhotonNetwork.JoinRoom(md.roomName);
            md.player = players - 1;
            SceneManager.LoadScene("photon_in");
        }

        if (push4.backbutton) //ホストが部屋作成後、戻る場合
        {
            //LoadScene();
            push4.backbutton = false;
            PhotonNetwork.Disconnect();
        }

        if (push5.backmenu) //BackToMenuが押された
        {
            push5.backmenu = false;
            bmenu = true;
            PhotonNetwork.Disconnect();
        }
    }
}

