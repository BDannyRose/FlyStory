using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance { get; private set; }
    public GameObject deathScreenUI;

    private void Awake()
    {
        instance = this;
    }

    public void Activate(bool state)
    {
        deathScreenUI.SetActive(state);
    }

    public void Restart()
    {
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
