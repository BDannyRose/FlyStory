using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomeText;

    private void OnEnable()
    {
        welcomeText.GetComponent<Text>().text = "Welcome, " + PlayerPrefs.GetString("playerName") + "!";
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
