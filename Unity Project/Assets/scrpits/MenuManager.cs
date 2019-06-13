using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour{
    bool playing = false;
    bool won = false;
    GameObject UI;
    [SerializeField]
    GameObject PauseMenu;
    int Lives/* , score*/;
    [SerializeField]
    GameObject GameWon;
    [SerializeField]
    GameObject obj;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
        UI = GameObject.Find("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
        if(playing && Input.GetButtonUp("Cancel") && !won)
        {
            if(Time.timeScale > 0)
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
            else
            {
                Resume();
            }
         
        }
    }


    public void Play()
    {
        UI.SetActive(false);
        playing = true;
        Time.timeScale = 1;
        obj.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameWon.SetActive(true);
            won = true;
            Time.timeScale = 0;
        }

    }
        
}
