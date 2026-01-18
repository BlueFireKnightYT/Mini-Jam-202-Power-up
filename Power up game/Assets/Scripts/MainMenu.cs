using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialChecker;
    public GameObject normalButtons;
    public void CheckTutorial()
    {
        tutorialChecker.SetActive(true);
        normalButtons.SetActive(false);
    }    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void StopGame()
    {
        Application.Quit();
    }
}
