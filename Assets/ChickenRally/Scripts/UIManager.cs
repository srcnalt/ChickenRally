using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text gameOver;
    public Text score;
    public Text time;
    public GameObject menuPanel;
    public AudioClip gameMusic;

    private float timer;
    
    public bool menuOn = true;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public void Update()
    {
        if (!menuOn)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

            time.text = "Time: " + timeText;
        }
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
        Cursor.visible = false;
        timer = 0;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = gameMusic;
        GetComponent<AudioSource>().Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
