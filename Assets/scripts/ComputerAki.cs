using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    Record record;
    private List<int> uniforms;
    private List<int> info;
    private List<int>[] handUniforms;
    //private List<int>[] originalUniforms;
    //private List<int>[] drawnUniform;
    //private List<int> opensource;
    public int playerNumber;
    public int computerLevel;
    public int blankmod;               //ブランクの数字で１３なら０としておく
    public bool zizikakunum = false;
    public bool zizikakuplace = false;
    public bool successflag = false;

    /*
    ちょくちょく更新するので読んで！
    〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜
    棋譜...record.record -> 正方形,None=-1
    プライベート情報...info -> None=-1
    handuniform...handuniform[player_num]で、player_numの持ってる背番号
    〜〜〜〜〜〜〜〜〜〜〜〜〜4/10更新〜〜〜〜〜〜〜〜〜〜〜〜〜  
    record.GetDrawnUniform()[player_num]で、player_numの持ってるドローンの背番号
    record.GetOriginalUniform()[player_num]で、player_numの持ってるオリジナルの背番号
    record.opensource()で、opensourceにあるカード番号  
    (この３つは結構使うようならget()に入れてもらってもよい)
    draw()の帰り値は「背番号」に変更！！！！
    */

    private void get()
    {
        record = GameObject.Find("GameManager").GetComponent<Record>();
        info = record.info[playerNumber];
        handUniforms = record.GetHandUniform();
        uniforms = record.Uniform;
    }

    private List<int> scoresfordraw; //背番号の数だけ５０点が入った数列を用意するまだ

    private int countN(List<int>[] rec,int uniform)
    {
        int cnt = 0;
        foreach (int un in rec[uniform]) if (un == -1) cnt++;
        return cnt;
    }

    //zizikaku関数についてはvoid型にしたほうがメモリの節約になっていいと思う

    private int Publiczizikaku(List<int>[] rec)
    {
        get();
        int zizi = -1;
        foreach (int i in record.UniformExists)
        {
            if (countN(rec, i) == 0) zizi = i;
        }
        if (zizi != -1)
        {
            zizikakunum = true;
            zizikakuplace = true;
        }
        return zizi; //ziziの背番号を返す。なかったら-1。
    }


    private List<int>[] Blanklister(List<int>[] a)  
    {
        List<int>[] c = new List<int>[4];    
        for (int j=0; j<4; j++)
        {
            int d = -1;
            foreach (int i in a[j])   //#a[j]が空ならc[j]={}としたい
            {
                if (info[i] % 13 == blankmod) d = i;
                else
                {
                    if (info[i] == -1) c[j].Add(i);
                }
            }
            if (d != -1) c[j] = new List<int> { d };
        }
        return c;
    }


    private int zizinumber = -1;
    private List<int>[] blistpublic;
    private List<int>[] blistprivate;
    private int[,] blanklist4;
    private int[,] blanklist3;


    private void InitBlankChaser(List<int>[] rec)
    {
        get();

        blistpublic = new List<int>[4]; //#b0pのこと
        for (int j = 0; j < 4; j++) blistpublic[j] = handUniforms[playerNumber];

        blistprivate = Blanklister(blistpublic);

        int pos4 = strlen(blistprivate[0]) * strlen(blistprivate[1]) * strlen(blistprivate[2]) * strlen(blistprivate[3]);
        blanklist4 = new int[4, pos4]; //#ziziかくしていてposが0ならblanklist4={ {} }としたい
        foreach (int i in blistprivate[0])
        {
            foreach (int j in blistprivate[1])
            {
                foreach (int k in blistprivate[2])
                {
                    foreach (int l in blistprivate[3])
                    {
                        int[] m = new int[4] { i, j, k, l };
                        for (int n = 0; n < pos4; n++)
                        {
                            if (blanklist4[n] == null)  //1回newにしないといけない？
                            {
                                blanklist4[n] = m;
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        if (strlen(blanklist4) == 0) //#blanklist4に含まれる１次元配列の個数カウントできてる？ //#turn0だけ特別に逆
        {
            zizinumber = blankmod;
            if (strlen(blistprivate) == 3)
            {
                int pos3 = ; //3つの積                         
                blanklist3 = new int[3, pos3];
                //以下は仮で１，２，３としている

                foreach (int j in blistprivate[1])
                {
                    foreach (int k in blistprivate[2])
                    {
                        foreach (int l in blistprivate[3])
                        {
                            int[] m = new int[3] { j, k, l };
                            for (int n = 0; n < pos3; n++)
                            {
                                if (blanklist3[n] == null)  //1回newにしないといけない？
                                {
                                    blanklist3[n] = m;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else print("error"); //#debug用
        }
    }

    private List<int>[] BlankChaser(List<int>[] rec)
    {
        int match1 = draw();  //drawはどこのクラスに入ってるの？
        int match2 = -1;
        if (   )  //そろった
        {
            match2 = ;  //相方の背番号
        }
        
        if (strlen(blanklist4) != 0)
        {
            if (   ) //#blankmodがそろった（背番号match1とmatch2がそろった）
            {
                for (int j = 0; j < strlen(blanklist4); j++) 
                {
                    int a = 0;
                    int b = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (blanklist4[j][i] == match1)
                        {
                            a = 1; 
                        }
                        if (blanklist4[j][i] == match2)
                        {
                            b = 1;
                        }
                    }
                    if (a * b != 1)　//#match1とmatch2が共存して無ければ
                    {
                        blanklist4[j] = null; //本当は消したい
                    }
                }
            }
            else
            {
                if (    ) //#そろった
                {
                    for (int j = 0; j < strlen(blanklist4); j++)
                    {
                        int a = 0;
                        int b = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (blanklist4[j][i] == match1)
                            {
                                a = 1;
                            }
                            if (blanklist4[j][i] == match2)
                            {
                                b = 1;
                            }
                        }
                        if (a * b == 1) //#match1とmatch2のいずれか一方があれば
                        {
                            blanklist4[j] = null; //本当は消したい
                        }
                    }
                }
                else　//#そろわず、移動したカードをmatch1として扱いmatch1が移動する直前のhanduniformsをmotomotとした
                {
                    List<int> motomoto = handUniforms[引くplayerNumber];　//おはぎ
                    for (int j = 0; j < strlen(blanklist4); j++)
                    {
                        int a = 0;
                        int b = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (blanklist4[j][i] == match1)
                            {
                                a = 1;
                            }
                            foreach (int k in motomoto)
                            {
                                if (blanklist4[j][i] == k)
                                {
                                    b = 1;
                                }                     
                            }
                        }
                        if (a * b == 1) //#match1とkがともにあれば
                        {
                            blanklist4[j] = null; //本当は消したい
                        }
                    }
                }
            }
        }
        else   //#ziziかく
        {
            if (zizinumber == -1)  //#ziziかくの瞬間
            {
                zizinumber = blankmod;
                blistprivate = Blanklister(blistpublic); 
                int pos3;
                if (strlen(blistprivate) == 3)
                {
                    pos3 = ; //3つの積                         
                    blanklist3 = new int[3, pos3];  
                    //以下は仮で１，２，３としている

                    foreach (int j in blistprivate[1])
                    {
                        foreach (int k in blistprivate[2])
                        {
                            foreach (int l in blistprivate[3])
                            {
                                int[] m = new int[3] { j, k, l };
                                for (int n = 0; n < pos3; n++)
                                {
                                    if (blanklist3[n] == null)  //1回newにしないといけない？
                                    {
                                        blanklist3[n] = m;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    pos3 = ; //3つの積                         
                    blanklist3 = new int[3, pos3];
                    //４つから３つをえらばないといけない仮で１，２，３としている、、かなり面倒

                    foreach (int j in blistprivate[1])
                    {
                        foreach (int k in blistprivate[2])
                        {
                            foreach (int l in blistprivate[3])
                            {
                                int[] m = new int[3] { j, k, l };
                                for (int n = 0; n < pos3; n++)
                                {
                                    if (blanklist3[n] == null)  //1回newにしないといけない？
                                    {
                                        blanklist3[n] = m;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < pos3; j++) //#共存情報から消去
                {
                    for (int i=0; i < 3; i++)
                    {
                        if (rec[j][i] != -1)
                        {
                            blanklist3[j] = null; //本当は消したい
                        }                      
                    }
                }
            }
            else　//#以前にziziかくしてた場合
            {
                if (    ) //#blankmodがそろった（背番号match1とmatch2がそろった）
                {
                    for (int j = 0; j < strlen(blanklist3); j++)
                    {
                        int a = 0;
                        int b = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            if (blanklist3[j][i] == match1)
                            {
                                a = 1;
                            }
                            if (blanklist3[j][i] == match2)
                            {
                                b = 1;
                            }
                        }
                        if (a * b != 1) //#match1とmatch2が共存して無ければ
                        {
                            blanklist3[j] = null; //本当は消したい
                        }
                    }
                }
                else
                {
                    if (     ) //#そろった
                    {
                        for (int j = 0; j < strlen(blanklist3); j++)
                        {
                            int a = 0;
                            int b = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                if (blanklist3[j][i] == match1)
                                {
                                    a = 1;
                                }
                                if (blanklist3[j][i] == match2)
                                {
                                    b = 1;
                                }
                            }
                            if (a * b == 1) //#match1とmatch2のどちらか一方があれば
                            {
                                blanklist3[j] = null; //本当は消したい
                            }
                        }
                    }
                    else　//#そろわず、移動したカードをmatch1として扱いmatch1が移動する直前のhanduniformsをmotomotoとした
                    {
                        for (int j = 0; j < strlen(blanklist3); j++)
                        {
                            int a = 0;
                            int b = 0;
                            for (int i = 0; i < 3; i++)
                            {
                                if (blanklist3[j][i] == match1)
                                {
                                    a = 1;
                                }
                                foreach (int k in motomoto)
                                {
                                    if (blanklist3[j][i] == k)
                                    {
                                        b = 1;
                                    }
                                }
                            }
                            if (a * b == 1) //#match1とkがともにあれば
                            {
                                blanklist3[j] = null; //本当は消したい
                            }
                        }
                    }
                }
            }  
        }

        return; //どうする？
    }

    private int strlen(List<int>[] blistprivate)  //以下３つは自動的に生成された
    {
        throw new NotImplementedException();
    }

    private int strlen(int[,] blanklist4)
    {
        throw new NotImplementedException();
    }

    private int strlen(List<int> list)
    {
        throw new NotImplementedException();
    }

    //ここまでブランクziziかく

    private List<int> success(int drawnPlayer)
    {
        get();
        List<int> suc = new List<int>();
        foreach(int un in handUniforms[drawnPlayer])
        {
            if(info[un] != -1)
            {
                foreach(int myun in handUniforms[playerNumber])
                {
                    if(info[myun] % 13 == info[un] % 13)
                    {
                        suc.Add(un);
                        successflag = true;
                    }
                }
            }
        }

        return suc;
    }

    public int draw(int drawnPlayer)
    {
        get();
        int zizikamo = Publiczizikaku(record.record); 
        int CardUniform = 100;

        if (handUniforms[drawnPlayer].Count == 1) return uniforms[handUniforms[drawnPlayer][0]];

        //if (handUniforms[drawnPlayer].Contains(zizikamo)) handUniforms[drawnPlayer].Remove(zizikamo);

        List<int> suc = success(drawnPlayer);
        if (suc.Count != 0) CardUniform = suc[0];
        else
        {
            while (true)
            {
                int index = Random.Range(0, handUniforms[drawnPlayer].Count);
                CardUniform = handUniforms[drawnPlayer][index];
                if (zizikamo != CardUniform) break;
            }
        }

        return uniforms[CardUniform];
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
