using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadOniline : MonoBehaviour
{

    ModeData md;
    // Start is called before the first frame update
    void Start()
    {
        md = GameObject.Find("ModeData").GetComponent<ModeData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void Reload()
    {
        Scene loadScene = SceneManager.GetActiveScene();
        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);
    }
    void RestartGame()
    {
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("Reload", PhotonTargets.Others);
    }
    public void ReloadForAll()
    {
        RestartGame();
        Invoke("Reload", 1.0f);
    }
}
