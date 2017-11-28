using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text gameOver;
    public Text score;
    public GameObject menuPanel;

    public bool menuOn = true;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public void UpdateScore(int s)
    {
        score.text = "Score: " + s;
    }

    public void DisplayGameOverText()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        menuOn = false;
        menuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
