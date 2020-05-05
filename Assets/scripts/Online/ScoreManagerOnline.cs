using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManagerOnline : MonoBehaviour
{
    ModeData md;
    TurnManagerOnline tm;
    InitCanvas init;
    List<int> score;
    
    // Start is called before the first frame update
    void Start()
    {
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        tm = GetComponent<TurnManagerOnline>();
        score = md.score;
    }
    public void WriteResult(List<int> wins,List<string> result)
    {
        List<int> scoreToOrder = new List<int>() {100,50,40,0 };
        init = GetComponent<InitCanvas>();
        init.gameoverP.SetActive(true);
        string[] playerInfo = md.playerInfo;
        string order;
        string points;
        for (int i=0; i < 4; i++)
        {
            score[wins[i]] += scoreToOrder[i];
            result[i] = "i位:" + playerInfo[wins[i]];
        }
        
        order = result[0] + "st\n" + result[1] + "nd\n" + result[2] + "rd\n" + result[3]; //Add points to this string
        points = "\n\n" + playerInfo[0] + ":" + score[0] + "pt\t" + playerInfo[1] + ":" + score[1] + "pt\t" + playerInfo[2] +":"+ score[2] + "pt\t" + playerInfo[3] +":" + score[3] + "pt";
        Text text = GameObject.Find("Results").GetComponent<Text>();
        text.text = order + points;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
