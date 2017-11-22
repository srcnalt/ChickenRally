using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text gameOver;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }
    
    public void DisplayGameOverText()
    {
        gameOver.gameObject.SetActive(true);
    }
}
