﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerVer2 : MonoBehaviour
{
    Record record;
    private List<int> info;
    private List<int>[] handUniforms;
    public int playerNumber;
    public int computerLevel;
    public List<int> blankmods;               //ブランクの数字 - 1
    public bool zizikakunum = false;
    public bool zizikakuplace = false;
    public bool successflag = false;
    private int zizinumber = -1;
    private int ziziuniform = -1;
    private List<int> unsuccessful = new List<int>(); //recordとはあまり被らせないつもり

    /*
    〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜〜
    棋譜...record.record -> 正方形,None=-1
    プライベート情報...info -> None=-1
    handuniform...handuniforms[player_num]で、player_numの持ってる背番号
    〜〜〜〜〜〜〜〜〜〜〜〜〜4/10更新〜〜〜〜〜〜〜〜〜〜〜〜〜  
    record.GetDrawnUniform()[player_num]で、player_numの持ってるドローンの背番号
    record.GetOriginalUniform()[player_num]で、player_numの持ってるオリジナルの背番号
    record.opensource()で、opensourceにあるカード番号  
    (この３つは結構使うようならget()に入れてもらってもよい)
    draw()の帰り値は「背番号」に変更！！！！
    〜〜〜〜〜〜〜〜〜〜〜〜4/16更新〜〜〜〜〜〜〜〜〜〜〜〜
    private変数として、previousTurnPlayer,previousDrawnPlayer,previousMovedCardを追加
    中身はそれぞれ一個前のターンで誰が引いたか、誰がひかれたか、何を引いたか
    load()を引いた後に毎回更新、  
    この前はエラーが多くて大変だったので、エラーになりそうだったら日本語で書いてくれると助かります
    */

    private void get()
    {
        record = GameObject.Find("GameManager").GetComponent<Record>();
        info = record.info[playerNumber];
        handUniforms = record.GetHandUniform();
        //uniforms = record.Uniform;
    }

    //更新情報
    private int previousMovedCard;
    private int previousDrawnPlayer;
    private int previousTurnPlayer;
    private int previousDeletedCard;

    public void load(int drawnPlayer, int carduniform, int turnPlayer, int deleted)
    {
        /*
        更新されるタイミングは、誰かがカードを引いて、record諸々更新された後なので、BlankChaserで使われる際は
        一個前のターンで誰が引いたか、誰がひかれたか、何を引いたか
        が格納されるようになっています。      
        */
        get();
        previousMovedCard = carduniform;
        previousDrawnPlayer = drawnPlayer;
        previousTurnPlayer = turnPlayer;
        previousDeletedCard = deleted;
        ownrecord();
        Publiczizikaku(record.record);
        Blankzizikaku();
    }

    private List<int> scoresfordraw; //背番号の数だけ５０点が入った数列を用意するまだ

    private void ownrecord() //publiczizikakuをそのまま使ってprivatezizizkakuを実装した,直接record.recordを更新するのはやめた
    {
        if (previousTurnPlayer == playerNumber) //comの番のとき
        {
            if (previousDeletedCard == -1) //引いて揃わなかった、どこで初期化ー１にした？？
            {
                for (int j = 0; j < info.Count; j++)
                {
                    if (record.record[j][previousMovedCard] == -1 && info[j] != -1 && info[previousMovedCard] % 13 != info[j] % 13) //揃わない場合だけ、infoUsingBlankを使いたいけど複雑化するから保留
                    {
                        int non = previousMovedCard + (100 * j); //2桁ずつで読み取る
                        if (!unsuccessful.Contains(non))
                        {
                            non = j + previousMovedCard * 100;
                            unsuccessful.Add(non); //逆順にしたもの
                            Debug.Log("player" + playerNumber + "はunsuccessfulにペア" + non + "を加えました");
                        }
                    }
                }
            }
        }
    }

    private int countN(List<int>[] rec, int uniform)
    {
        int cnt = 0;
        foreach (int un in rec[uniform])
        {
            int non = un + uniform * 100;
            if (un == -1 && !unsuccessful.Contains(non)) cnt++;
        }
        return cnt;
    }

    //zizikaku関数についてはvoid型にしたほうがメモリの節約になっていいと思う

    private void Publiczizikaku(List<int>[] rec)
    {
        get();
        foreach (int i in record.UniformExists)
        {
            //if (record.UniformExists.Count < 6) Debug.Log("背番号" + i + "の共存してないカードは" + countN(rec, i) + "枚です"); 
            if (countN(rec, i) == 0)
            {
                ziziuniform = i;
                Debug.Log("プレーヤー" + playerNumber + "はレコード利用でziziuniformが" + ziziuniform + "と決定");
            }
        }
        if (ziziuniform != -1)
        {
            zizikakuplace = true;
            if (info[ziziuniform] != -1)
            {
                zizinumber = info[ziziuniform] % 13;
                zizikakunum = true;
            }
        }
    }


    private List<int>[] Blanklister(List<int>[] b0p, int blankindex)  //zizi候補配列を返す
    {
        int blankmod = blankmods[blankindex];

        List<int>[] c = new List<int>[4];
        for (int j = 0; j < 4; j++)
        {
            int d = -1;
            c[j] = new List<int>();
            foreach (int i in b0p[j])   //#a[j]が空ならc[j]={}としたい
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


    //blankzizi用

    private List<int>[] blistpublic = new List<int>[4];
    private List<List<int>[]> blistprivate = new List<List<int>[]>();
    private List<List<List<int>>> blanklist4 = new List<List<List<int>>>();
    private List<List<List<int>>> blanklist3 = new List<List<List<int>>>();
    private List<bool> PairChecked = new List<bool>(); //これはそのまま？

    private void InitBlankChaser(List<int>[] rec, int blankindex)
    {
        get();
        int blankmod = blankmods[blankindex];
        for (int j = 0; j < 4; j++) blistpublic[j] = handUniforms[j];

        blistprivate.Add(Blanklister(blistpublic, blankindex));
        int pos4 = blistprivate[blankindex][0].Count * blistprivate[blankindex][1].Count * blistprivate[blankindex][2].Count * blistprivate[blankindex][3].Count;
        if (pos4 != 0)
        {
            foreach (int i in blistprivate[blankindex][0])
            {
                foreach (int j in blistprivate[blankindex][1])
                {
                    foreach (int k in blistprivate[blankindex][2])
                    {
                        foreach (int l in blistprivate[blankindex][3])
                        {
                            blanklist4[blankindex].Add(new List<int> { i, j, k, l });
                            //if (blanklist4[blankindex].Count<10)
                            //{
                            //Debug.Log(i);
                            //Debug.Log(j);
                            //Debug.Log(k);
                            //Debug.Log(l);
                            //}
                        }
                    }
                }
            }
        }
        //foreach(List<int> a in blanklist4)
        //{
        //    foreach (int b in a) Debug.Log(b);
        //    Debug.Log("\n");
        //}


        if (blanklist4[blankindex].Count == 0) //#blanklist4に含まれる１次元配列の個数カウントできてる？ //#turn0だけ特別に逆
        {
            zizinumber = blankmod;
            zizikakunum = true;
            if (blistprivate[blankindex][playerNumber].Count == 0) //このifは不要
            {
                //以下は仮で１，２，３としている
                List<int> playersTmp = new List<int> { 0, 1, 2, 3 };
                playersTmp.Remove(playerNumber);
                foreach (int j in blistprivate[blankindex][playersTmp[0]])
                {
                    foreach (int k in blistprivate[blankindex][playersTmp[1]])
                    {
                        foreach (int l in blistprivate[blankindex][playersTmp[2]])
                        {
                            blanklist3[blankindex].Add(new List<int> { j, k, l });
                        }
                    }
                }
            }
            //else Debug.Log("error"); //#debug用
        }
    }

    private List<int> nonsuccessturn = new List<int>();

    private void BlankChaser(List<int>[] rec, int blankindex)
    {
        get();
        int blankmod = blankmods[blankindex];
        //Debug.Log(blankmod);
        List<int> knownBlanks = new List<int>();
        List<int> deadBlanks = new List<int>();
        foreach (int card in info) if (card % 13 == blankmod) knownBlanks.Add(info.IndexOf(card));  //opensourceにカードが出ていたら。
        foreach (int card in record.opensource()) if (card % 13 == blankmod) deadBlanks.Add(info.IndexOf(card));

        if (deadBlanks.Count == 4) return;  //全部出てたらいらない←めっちゃいい

        if (blanklist4[blankindex].Count != 0)
        {
            if (deadBlanks.Count == 2 && !PairChecked[blankindex]) //#blankmodがそろった
            {
                List<List<int>> tmplist2 = new List<List<int>>();
                foreach (List<int> kouho in blanklist4[blankindex])
                {
                    if (kouho.Contains(previousMovedCard) && kouho.Contains(previousDeletedCard)) tmplist2.Add(kouho);
                }
                blanklist4[blankindex] = new List<List<int>>(tmplist2);
                PairChecked[blankindex] = true;
            }
            else
            {
                if (previousDeletedCard != -1) //#そろった
                {
                    List<List<int>> tmplist = new List<List<int>>();
                    //Debug.Log(blanklist4[blankindex].Count);
                    foreach (List<int> kouho in blanklist4[blankindex])
                    {
                        if (kouho.Contains(previousMovedCard) || kouho.Contains(previousDeletedCard)) tmplist.Add(kouho);
                    }
                    foreach (List<int> kouho in tmplist) blanklist4[blankindex].Remove(kouho);
                    //Debug.Log(blanklist4[blankindex].Count);
                }
                else //#そろわず、移動したカードをpreviousMovedCardとして扱い移動する直前のhanduniformsをmotomotoとした
                {
                    List<int> motomoto = handUniforms[previousTurnPlayer];
                    motomoto.Remove(previousMovedCard);
                    //Debug.Log("そろわず");
                    if (!nonsuccessturn.Contains(rec[previousMovedCard][motomoto[0]]))
                    {
                        nonsuccessturn.Add(rec[previousMovedCard][motomoto[0]]);
                    }//turn番号の取得方法が分からなかったからとりあえず棋譜から読むことにした

                    if (previousTurnPlayer == playerNumber) /////comの番のとき
                    {
                        if (info[previousMovedCard] % 13 == blankmod) //引いたカードがブランクだったら
                        {
                            List<List<int>> tmplist = new List<List<int>>();
                            foreach (List<int> kouho in blanklist4[blankindex])
                            {
                                if (!kouho.Contains(previousMovedCard)) tmplist.Add(kouho);
                            }
                            foreach (List<int> kouho in tmplist) blanklist4[blankindex].Remove(kouho);
                        }
                        else
                        {
                            List<List<int>> tmplist = new List<List<int>>();
                            foreach (List<int> kouho in blanklist4[blankindex])
                            {
                                if (kouho.Contains(previousMovedCard)) tmplist.Add(kouho);
                            }
                            foreach (List<int> kouho in tmplist) blanklist4[blankindex].Remove(kouho);
                        }
                    }
                    else
                    {
                        List<List<int>> tmplist = new List<List<int>>();
                        //Debug.Log(blanklist4[blankindex].Count);
                        foreach (List<int> kouho in blanklist4[blankindex])
                        {
                            foreach (int uniform in motomoto)
                            {
                                if (kouho.Contains(previousMovedCard) && kouho.Contains(uniform)) tmplist.Add(kouho);
                            }
                        }
                        foreach (List<int> kouho in tmplist) blanklist4[blankindex].Remove(kouho);
                        //Debug.Log(blanklist4[blankindex].Count);
                    }
                }
            }
        }

        if (blanklist4[blankindex].Count == 0)   //ziziかく
        {
            if (zizinumber == -1 || blanklist3[blankindex].Count == 0)  //ziziかくの瞬間、publiczizizkakuしててもこっちに入れるようにした
            {
                string debug = playerNumber + "zizi確した！(数字)";
                Debug.Log(debug);
                zizinumber = blankmod;
                zizikakunum = true;

                blistprivate[blankindex] = Blanklister(blistpublic, blankindex);
                //List<List<int>[]> blistprivate = new List<List<int>[]>();
                //blistprivate.Add(Blanklister(blistpublic, blankindex));

                int zeroPlayer = -1;
                for (int i = 0; i < 4; i++) if (blistprivate[blankindex][i].Count == 0) zeroPlayer = i; //ここちょっと怪しかったけどたぶんあってる
                if (zeroPlayer != -1)
                {
                    Debug.Log("誰かのオリジナルすべて見てブランクなし");
                    List<int> playersTmp = new List<int> { 0, 1, 2, 3 };
                    playersTmp.Remove(zeroPlayer);
                    foreach (int j in blistprivate[blankindex][playersTmp[0]])
                    {
                        foreach (int k in blistprivate[blankindex][playersTmp[1]])
                        {
                            foreach (int l in blistprivate[blankindex][playersTmp[2]])
                            {
                                blanklist3[blankindex].Add(new List<int> { j, k, l });
                            }
                        }
                    }
                    //Debug.Log(blanklist3[blankindex].Count + 1000);
                }
                else
                {
                    //４つから３つをえらばないといけない
                    Debug.Log("誰かのオリジナルをすべて見てはいない");
                    List<List<int>> playersTmp = new List<List<int>>();
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != playerNumber)
                        {
                            List<int> tmp = new List<int> { 0, 1, 2, 3 };
                            tmp.Remove(i);
                            playersTmp.Add(tmp);
                        }
                    }
                    foreach (List<int> vs in playersTmp)
                    {
                        foreach (int j in blistprivate[blankindex][vs[0]])
                        {
                            foreach (int k in blistprivate[blankindex][vs[1]])
                            {
                                foreach (int l in blistprivate[blankindex][vs[2]])
                                {
                                    blanklist3[blankindex].Add(new List<int> { j, k, l });
                                }
                            }
                        }
                    }
                }

                //Debug.Log(nonsuccessturn.Count + 0.1);

                List<List<int>> tmplist = new List<List<int>>();　//以下うまく消せてない模様....?
                foreach (List<int> kouho in blanklist3[blankindex])　//共存したら消去するようにした
                {
                    if (nonsuccessturn.Contains(rec[kouho[0]][kouho[1]])) { tmplist.Add(kouho); continue; }
                    if (nonsuccessturn.Contains(rec[kouho[0]][kouho[2]])) { tmplist.Add(kouho); continue; }
                    if (nonsuccessturn.Contains(rec[kouho[1]][kouho[2]])) { tmplist.Add(kouho); continue; }
                }
                foreach (List<int> kouho in tmplist) blanklist3[blankindex].Remove(kouho);

                //以下は雑に作った、もしかすると不要なものがあるかもしれない
                if (knownBlanks.Count == 1)
                {
                    Debug.Log("knownBlanks.Countが１");
                    List<List<int>> tmplist2 = new List<List<int>>();
                    foreach (List<int> kouho in blanklist3[blankindex])
                    {
                        if (!kouho.Contains(knownBlanks[0])) { tmplist2.Add(kouho); continue; }
                    }
                    foreach (List<int> kouho in tmplist2) blanklist3[blankindex].Remove(kouho);
                }
                if (knownBlanks.Count == 2)
                {
                    Debug.Log("knownBlanks.Countが２");
                    List<List<int>> tmplist2 = new List<List<int>>();
                    foreach (List<int> kouho in blanklist3[blankindex])
                    {
                        if (!kouho.Contains(knownBlanks[0]) || !kouho.Contains(knownBlanks[1])) { tmplist2.Add(kouho); continue; }
                    }
                    foreach (List<int> kouho in tmplist2) blanklist3[blankindex].Remove(kouho);
                }
                if (knownBlanks.Count == 3) //knownblanksで置き換えてしまった
                {
                    Debug.Log("knownBlanks.Countが３");
                    blanklist3[blankindex] = new List<List<int>> { knownBlanks };
                }
            }
            else
            {
                //Debug.Log("すでにzizi確");
                if (deadBlanks.Count == 2 && !PairChecked[blankindex]) //#blankmodがそろった
                {
                    List<List<int>> tmplist2 = new List<List<int>>();
                    foreach (List<int> kouho in blanklist3[blankindex])
                    {
                        if (kouho.Contains(previousMovedCard) && kouho.Contains(previousDeletedCard)) tmplist2.Add(kouho);
                    }
                    blanklist3[blankindex] = new List<List<int>>(tmplist2);
                    PairChecked[blankindex] = true;
                }
                else
                {
                    if (previousDeletedCard != -1) //#そろった
                    {
                        List<List<int>> tmplist3 = new List<List<int>>();
                        //Debug.Log(blanklist3[blankindex].Count);
                        foreach (List<int> kouho in blanklist3[blankindex])
                        {
                            if (kouho.Contains(previousMovedCard) || kouho.Contains(previousDeletedCard)) tmplist3.Add(kouho);
                        }
                        foreach (List<int> kouho in tmplist3) blanklist3[blankindex].Remove(kouho);
                        //Debug.Log(blanklist3[blankindex].Count);
                    }
                    else //#そろわず、移動したカードをpreviousMovedCardとして扱い移動する直前のhanduniformsをmotomotoとした
                    {
                        if (previousTurnPlayer == playerNumber) /////comの番のとき
                        {
                            if (info[previousMovedCard] % 13 == blankmod) //引いたカードがブランクだったら
                            {
                                List<List<int>> tmplist = new List<List<int>>();
                                foreach (List<int> kouho in blanklist3[blankindex])
                                {
                                    if (!kouho.Contains(previousMovedCard)) tmplist.Add(kouho);
                                }
                                foreach (List<int> kouho in tmplist) blanklist3[blankindex].Remove(kouho);
                            }
                            else
                            {
                                List<List<int>> tmplist = new List<List<int>>();
                                foreach (List<int> kouho in blanklist3[blankindex])
                                {
                                    if (kouho.Contains(previousMovedCard)) tmplist.Add(kouho);
                                }
                                foreach (List<int> kouho in tmplist) blanklist3[blankindex].Remove(kouho);
                            }
                        }
                        else
                        {
                            List<int> motomoto = handUniforms[previousTurnPlayer];
                            motomoto.Remove(previousMovedCard);
                            List<List<int>> tmplist3 = new List<List<int>>();
                            //Debug.Log(blanklist3[blankindex].Count);
                            foreach (List<int> kouho in blanklist3[blankindex])
                            {
                                foreach (int uniform in motomoto)
                                {
                                    if (kouho.Contains(previousMovedCard) && kouho.Contains(uniform)) tmplist3.Add(kouho);
                                }
                            }
                            foreach (List<int> kouho in tmplist3) blanklist3[blankindex].Remove(kouho);
                            //Debug.Log(blanklist3.Count);
                        }

                    }
                }
            }
        }

        if (blanklist4[blankindex].Count != 0) Debug.Log("p" + playerNumber + "にとってブランクの数字" + (blankmods[blankindex] + 1) + "の組は" + blanklist4[blankindex].Count + "通り");
        else Debug.Log("p" + playerNumber + "にとってziziであるブランクの数字" + (blankmods[blankindex] + 1) + "の組は" + blanklist3[blankindex].Count + "通り");
    }

    //ここまでブランクziziかく


    private List<int> infoUsingBlank(List<int>[] rec)
    {
        get();
        List<int> infoub = new List<int>(info);
        if (zizinumber != -1)　//もしかすると引くタイミング的に最新ではないかも
        {
            for (int j = 0; j < blankmods.Count; j++)
            {
                //BlankChaser(record.record, j);　　//ここで回しておかないとblanklist4が１ターンまえの情報になってしまう？、けどやっぱやめた

                if (blanklist3[j].Count == 1)
                {
                    //Debug.Log("ブランク3利用で揃うカード特定"); これは付けたくない
                    foreach (int blank in blanklist3[j][0])
                    {
                        infoub[blank] = blankmods[j]; //全部同じ数字にしといた
                    }
                }

                if (blanklist4[j].Count == 1)
                {
                    //Debug.Log("ブランク4利用で揃うカード特定");
                    foreach (int blank in blanklist4[j][0])
                    {
                        infoub[blank] = blankmods[j]; //全部同じ数字にしといた
                    }
                }

                if (blanklist4[j].Count > 1)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        blistprivate[j] = Blanklister(blistpublic, j);
                        if (infoub[blistprivate[j][k][0]] == -1 && blistprivate[j][k].Count == 1)
                        {
                            //Debug.Log("ブリストプライベイト利用でinfo特定");
                            infoub[blistprivate[j][k][0]] = blankmods[j];
                        }
                    }
                }
            }

            foreach (int i in record.UniformExists)
            {
                if (infoub[i] != -1 && countN(rec, i) == 1 && i % 13 != zizinumber)
                {
                    for (int j = 0; j < info.Count; j++)
                    {
                        if (rec[i][j] == -1)
                        {
                            if (infoub[j] == -1) infoub[j] = infoub[i]; //同じ数字にしといた
                            else
                            {
                                if (infoub[i] % 13 != infoub[j] % 13) Debug.Log("infousingblankの中で事件が起きている");
                            }
                        }
                    }
                }
            }
        }
        return infoub;
    }


    private List<int> success(int drawnPlayer) //ここのエラーはなおっていないかも
    {
        get();
        List<int> suc = new List<int>();
        List<int> info2 = infoUsingBlank(record.record);

        foreach (int un in handUniforms[drawnPlayer])
        {
            if (info2[un] != -1)
            {
                foreach (int myun in handUniforms[playerNumber])
                {
                    if (info2[myun] % 13 == info2[un] % 13)
                    {
                        suc.Add(un);
                        successflag = true;
                    }
                }
            }
        }

        return suc;
    }


    private List<int> nonsuccess(int drawnPlayer)
    {
        get();
        List<int> nonsuc = new List<int>();
        List<int> info2 = infoUsingBlank(record.record);

        foreach (int un in handUniforms[drawnPlayer])
        {
            if (info2[un] != -1)
            {
                nonsuc.Add(un); //とりあえず見たカードを全て入れといた
            }
        }

        List<int> nonsuc2 = new List<int>(nonsuc);
        foreach (int un in nonsuc)
        {
            if (success(drawnPlayer).Contains(un)) nonsuc2.Remove(un);
        }

        return nonsuc2;
    }


    private bool dangerousCard(int drawnPlayer, int cardUni) //自分の手札以外にそろうカードの候補がないカード
    {
        List<int> uniexists = new List<int>(record.UniformExists);
        foreach (int un in handUniforms[playerNumber])
        {
            uniexists.Remove(un); //ターンプレーヤー以外の手札を入れてる
        }

        bool judge = true;

        foreach (int un in uniexists)
        {
            int non = un + cardUni * 100;
            if (record.record[cardUni][un] == -1 && !unsuccessful.Contains(non)) judge = false; //共存してないカードが１枚でもあればセーフ
        }

        return judge;
    }


    private void Blankzizikaku()
    {
        if (blankmods.Count == 0) return;  //blankindex付いてたのはミス？  
        int debugcnt = 0; //デバッグ用
        for (int j = 0; j < blankmods.Count; j++)
        {
            BlankChaser(record.record, j);
            if (zizinumber == blankmods[j] && ziziuniform == -1)
            {
                if (blanklist3[j].Count == 1)
                {
                    foreach (int uni in blanklist3[j][0])
                    {
                        Debug.Log(uni + "の背番号がziziの数字");
                        if (record.UniformExists.Contains(uni))
                        {
                            ziziuniform = uni;
                            zizikakuplace = true;
                            debugcnt++;
                            Debug.Log("プレーヤー" + playerNumber + "はブランクじじかくでziziuniformが" + ziziuniform + "と決定");
                        }
                    }
                    if (debugcnt==0) Debug.Log("ここに入るとやばい");
                }
            }
            if (debugcnt > 1) Debug.Log("zizi確してるけど場に３枚ある");  //変な挙動をしてるかも
        }
    }


    public bool ZiziTest(int uni, List<int> uniforms) //場に11枚以下のみ(13枚以上だとtrueを返してしまう)、過半数じじかく含む
    {
        List<int> uniexists9 = new List<int>(uniforms);
        uniexists9.Remove(uni); //uni以外の残り10枚でペアが作れるか
        if (uniexists9.Count == 0) return false;
        int t9 = uniexists9[0]; //先頭と後で比較する
        uniexists9.RemoveAt(0); //残り9枚
        foreach (int u9 in uniexists9)
        {
            int non = t9 + u9 * 100;
            if (record.record[t9][u9] == -1 && !unsuccessful.Contains(non))
            {
                if (uniexists9.Count == 1)
                {
                    //Debug.Log("場に残り3枚で背番号" + uni + "はziziかも");
                    return false;
                }
                List<int> uniexists7 = new List<int>(uniexists9);
                uniexists7.Remove(u9); //残り8枚
                int t7 = uniexists7[0];
                uniexists7.RemoveAt(0); //残り7枚
                foreach (int u7 in uniexists7)
                {
                    non = t7 + u7 * 100;
                    if (record.record[t7][u7] == -1 && !unsuccessful.Contains(non))
                    {
                        if (uniexists7.Count == 1)
                        {
                            //Debug.Log("場に残り5枚で背番号" + uni + "はziziかも");
                            return false;
                        }
                        List<int> uniexists5 = new List<int>(uniexists7);
                        uniexists5.Remove(u7); //残り6枚
                        int t5 = uniexists5[0];
                        uniexists5.RemoveAt(0); //残り5枚
                        foreach (int u5 in uniexists5)
                        {
                            non = t5 + u5 * 100;
                            if (record.record[t5][u5] == -1 && !unsuccessful.Contains(non))
                            {
                                if (uniexists5.Count == 1)
                                {
                                    //Debug.Log("場に残り7枚で背番号" + uni + "はziziかも");
                                    return false;
                                }
                                List<int> uniexists3 = new List<int>(uniexists5);
                                uniexists3.Remove(u5); //残り4枚
                                int t3 = uniexists3[0];
                                uniexists3.RemoveAt(0); //残り3枚
                                foreach (int u3 in uniexists3)
                                {
                                    non = t3 + u3 * 100;
                                    if (record.record[t3][u3] == -1 && !unsuccessful.Contains(non))
                                    {
                                        if (uniexists3.Count == 1)
                                        {
                                            //Debug.Log("場に残り9枚で背番号" + uni + "はziziかも");
                                            return false;
                                        }
                                        List<int> uniexists1 = new List<int>(uniexists3);
                                        uniexists1.Remove(u3); //残り2枚
                                        int t1 = uniexists1[0];
                                        uniexists1.RemoveAt(0); //残り1枚
                                        foreach (int u1 in uniexists1)
                                        {
                                            non = t1 + u1 * 100;
                                            if (record.record[t1][u1] == -1 && !unsuccessful.Contains(non))
                                            {
                                                if (uniexists1.Count == 1)
                                                {
                                                    //Debug.Log("場に残り11枚で背番号" + uni + "はziziかも");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("背番号" + uni + "は、ziziでは絶対ない");
        return true;
    }


    public int draw(int drawnPlayer)
    {
        get();
        //Blankzizikaku();
        Publiczizikaku(record.record);

        int cnt = 0;
        int unikari = -1;
        if (record.UniformExists.Count < 12)
        {
            foreach (int un in record.UniformExists)
            {
                bool kari = ZiziTest(un, record.UniformExists);
                if (!kari)
                {
                    cnt++;
                    unikari = un;
                }
            }
            if (cnt == 0) Debug.Log("ziziかもがないのは重大なエラー");
            if (cnt == 1) Debug.Log("ZiziTestで背番号" + unikari + "がziziかく");
        }

        if (ziziuniform != -1) Debug.Log("プレーヤー" + playerNumber + "にとってziziuniformは" + ziziuniform + "です");

        if (handUniforms[drawnPlayer].Count == 1) return handUniforms[drawnPlayer][0];

        int CardUniform = 100;

        List<int> suc = success(drawnPlayer);

        if (suc.Count != 0)
        {
            CardUniform = suc[0]; //とりあえず揃うカードは第一優先で引く
            if (CardUniform == ziziuniform)
            {
                CardUniform = 100;
                Debug.Log("揃うと思ったカードがziziだという重大なエラー");
            }
            else Debug.Log("揃うのでひきました");
        }
        else
        {
            foreach (int un in handUniforms[drawnPlayer])
            {
                if (dangerousCard(drawnPlayer, un) && ziziuniform != un) //危険カードはいいタイミングで引きたい、ブランクがまだ３枚残ってる超特殊な場合以外は大丈夫
                {
                    if (ziziuniform != -1)
                    {
                        CardUniform = un;
                        break;
                    }
                    if (handUniforms[playerNumber].Count < 3)
                    {
                        CardUniform = un;
                        break;
                    }
                }
            }
        }

        List<int> nonsuc = nonsuccess(drawnPlayer);

        if (CardUniform == 100)
        {
            int j = 0;
            while (true)
            {
                int index = Random.Range(0, handUniforms[drawnPlayer].Count);
                CardUniform = handUniforms[drawnPlayer][index];

                if (ziziuniform != CardUniform)　//ziziじゃない場合
                {
                    if (!nonsuc.Contains(CardUniform) || nonsuc.Count == handUniforms[drawnPlayer].Count)　//揃わないカードはひかない、ただし全部揃わないなら引かざるを得ない
                    {
                        if (!dangerousCard(drawnPlayer, CardUniform))
                        {
                            break;
                        }
                    }
                }

                j++;

                if (j > 99) //危険カードばかりだと上の条件だけでは無限ループに陥ることがある
                {
                    if (CardUniform == ziziuniform)
                    {
                        if (index == 0) CardUniform = handUniforms[drawnPlayer][1];
                        else CardUniform = handUniforms[drawnPlayer][0];
                        Debug.Log("ここに入ると重大なエラー");
                    }
                    break;
                }
            }
        }

        return CardUniform;
    }



    // Start is called before the first frame update
    void Start()
    {
        get();
        List<int> gravenum = new List<int>();
        foreach (int card in record.opensource()) gravenum.Add(card % 13);
        for (int num = 0; num < 13; num++)
        {
            if (!gravenum.Contains(num)) blankmods.Add(num);
        }
        for (int i = 0; i < blankmods.Count; i++)
        {
            PairChecked.Add(false);
            blanklist4.Add(new List<List<int>>());
            blanklist3.Add(new List<List<int>>());
            InitBlankChaser(record.record, i);
        }
        if (zizikakunum) Debug.Log("プレーヤー" + playerNumber + "は初期じじかく" + (zizinumber + 1));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
