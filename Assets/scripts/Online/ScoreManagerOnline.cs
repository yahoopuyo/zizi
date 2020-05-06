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
    List<int> zzkkscore;
    List<string>[] result = new List<string>[4];
    public List<List<int>[]> zzkkList = new List<List<int>[]>();
    private List<int> zzkkrank = new List<int>() { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
        tm = GetComponent<TurnManagerOnline>();
        score = md.score;
        zzkkscore = md.zzkkscore;
    }

    public void WriteResult(List<int> wins, int zizi)
    {
        List<int> scoreToOrder = new List<int>() { 120, 70, 60, 0};
        List<int> zzkkscoreToOrder = new List<int>() { 120, 90, 60, 20};
        init = GetComponent<InitCanvas>();
        init.gameoverP.SetActive(true);
        string[] playerInfo = md.playerInfo;
        string order;
        string points;
        /*
        for (int i = 0; i < 4; i++)
        {
            if (!zzkkList[i].Contains(zizi)) ziziList[i][0] == 1000;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (zzkkList[i][0] > zzkkList[j][0]) //ターン番号の小さいものがあれば
                {
                    zzkkrank[i] += 1;
                }
            }
            if (zzkkList[i] == 1000) zzkkscore[i] = 0; //じじかくボタン押さなかった場合もこっちに入る
            else zzkkscore[i] += zzkkscoreToOrder[zzkkrank[i]] / (zzkkList[i].Count - 1);
        }
        */
        for (int i=0; i < 4; i++)
        {
            score[wins[i]] += scoreToOrder[i];
            result[i] = (i+1) + "位: " + playerInfo[wins[i]];
        }
        
        order = result[0] + "st\n" + result[1] + "nd\n" + result[2] + "rd\n" + result[3]; //Add points to this string
        points = "\n\n" + playerInfo[0] + ":" + score[0] + " + " + zzkkscore[0] + " pt\t" + playerInfo[1] + ":" + score[1] + " + " + zzkkscore[1] + " pt\t" + playerInfo[2] +":"+ score[2] + " + " + zzkkscore[2] + " pt\t" + playerInfo[3] +":" + score[3] + " + " + zzkkscore[3] + " pt";
        Text text = GameObject.Find("Results").GetComponent<Text>();
        text.text = order + points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
