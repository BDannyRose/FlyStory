using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance { get; private set; }
    public GameObject deathScreenUI;
    public GameObject highscoreTable;

    private void Awake()
    {
        instance = this;
    }

    public void Activate(bool state)
    {
        deathScreenUI.SetActive(state);
    }

    public void ActivateHighscoreTable()
    {
        highscoreTable.SetActive(true);
    }
    public void CloseHighscoreTable()
    {
        highscoreTable.SetActive(false);
    }

    public void Restart()
    {
        ScoreManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
