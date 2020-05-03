using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.player.NickName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
