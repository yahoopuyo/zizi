using System.Collections;
using System.Collections.Generic;
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
    public int computerLevel;
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
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("zizitest");
    }

    public bool IsSolo()
    {
        return Solo;
    }
    public bool IsEasy()
    {
        return Easy;
    }
}
