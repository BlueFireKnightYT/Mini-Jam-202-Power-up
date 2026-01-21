using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialChecker;
    public GameObject normalButtons;
    HpSystem hpSys;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        hpSys = player.GetComponent<HpSystem>();
    }
    public void CheckTutorial()
    {
        tutorialChecker.SetActive(true);
        normalButtons.SetActive(false);
    }    
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void StopGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        hpSys.inPauseMenu = false;
        hpSys.pauseMenu.SetActive(false);
    }
}
