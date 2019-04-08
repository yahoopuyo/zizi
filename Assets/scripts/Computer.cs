using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    Record record;
    private List<int> uniforms;
    private List<int> info;
    private List<int>[] handUniforms;
    public int playerNumber;
    public int computerLevel;
    public bool zizikakunum = false;
    public bool zizikakuplace = false;
    public bool successflag = false;

    private void get()
    {
        record = GameObject.Find("GameManager").GetComponent<Record>();
        info = record.info[playerNumber];
        handUniforms = record.GetHandUniform();
        uniforms = record.Uniform;
    }

    private int countN(List<int>[] rec,int uniform)
    {
        int cnt = 0;
        foreach (int un in rec[uniform]) if (un == -1) cnt++;
        return cnt;
    }

    private int publicZizikaku(List<int>[] rec)
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
        return zizi;
    }

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
                    if(uniforms[myun] % 13 == info[un] % 13)
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
        int zizikamo = publicZizikaku(record.record);
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
