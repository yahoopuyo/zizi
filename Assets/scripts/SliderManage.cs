using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManage : MonoBehaviour
{
    Slider slider;
    int value;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSlideCPULevel()
    {
        ModeData modeData = GameObject.Find("ModeData").GetComponent<ModeData>();
        Text text = GameObject.Find("SliderText").GetComponent<Text>();
        value = (int)slider.value;
        modeData.computerLevel = value;
        text.text = "cpu level ... " + value;

    }
}
