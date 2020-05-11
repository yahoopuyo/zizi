using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeData : MonoBehaviour
{
    GameObject menu;
    GameObject mode;
    GameObject cpu;
    GameObject inst;
    private bool Easy;
    private bool Solo;
    public bool isHost;
    public int computerLevel;
    public int player;
    public int numOfPlayer;
    public string roomName;
    public List<int> score;
    public List<int> zzkkscore;
    public string[] playerInfo;
    // Start is called before the first frame update

    void Start()
    {
        DontDestroyOnLoad(this);
        mode = GameObject.Find("ModeSelectForSoloPanel"); 
        mode.SetActive(false);
        cpu = GameObject.Find("computerLevelForSoloPanel");
        cpu.SetActive(false);
        menu = GameObject.Find("MenuPanel");
        menu.SetActive(true);
        inst = GameObject.Find("InstructionPanel");
        inst.SetActive(false);
        score = new List<int>() { 0,0,0,0 };
        zzkkscore = new List<int>() { 0, 0, 0, 0 };
        playerInfo = new string[4] { "player", "Com", "Com", "Com" }; //for solo play
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickNormal()
    {
        Easy = false;
        mode.SetActive(false);
        cpu.SetActive(true);
    }

    public void OnClickEasy()
    {
        Easy = true;
        mode.SetActive(false);
        cpu.SetActive(true);
    }

    public void OnClickSolo()
    {
        Solo = true;
    }

    public void OnClickOnline()
    {
        Solo = false;
        SceneManager.LoadScene("photontest1");
    }

    public void OnClickStart()
    {
        if (computerLevel != 0) SceneManager.LoadScene("zizitest");
    }

    public bool IsSolo()
    {
        return Solo;
    }
    public bool IsEasy()
    {
        return Easy;
    }
    public bool IsHost()
    {
        return isHost;
    }
}
