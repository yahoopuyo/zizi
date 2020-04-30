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
        string order;
        string points;
        for (int i=0; i < 4; i++)
        {
            score[wins[i]] += scoreToOrder[i];
        }
        order = result[0] + "st\n\n" + result[1] + "nd\n\n" + result[2] + "rd\n\n" + result[3]; //Add points to this string
        points = "\n\nplayer0:" + score[0] + "pt  player1:" + score[1] + "pt\tplayer2:" + score[2] + "pt\tplayer3:" + score[3] + "pt";
        Text text = GameObject.Find("Results").GetComponent<Text>();
        text.text = order + points;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
