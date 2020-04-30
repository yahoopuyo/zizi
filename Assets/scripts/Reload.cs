using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void push()
    {
        SceneManager.LoadScene("photontest1");
        Debug.Log("reload");
    }
}

