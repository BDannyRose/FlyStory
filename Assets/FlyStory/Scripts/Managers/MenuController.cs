using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject newPlayerMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject inputField;
    private void Awake()
    {
        newPlayerMenu.SetActive(false);
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
        if (!(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) || Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)))
        {
            Permission.RequestUserPermissions(new string[] { Permission.ExternalStorageRead, Permission.ExternalStorageWrite });
        }
    }

    public void CheckPlayerState()
    {
        if (PlayerPrefs.GetString("playerName", "") == "")
        {
            InitializePlayer();
        }
        else
        {
            LoadPlayer();
        }
    }

    private void InitializePlayer()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        newPlayerMenu.SetActive(true);
    }

    private void LoadPlayer()
    {
        newPlayerMenu.SetActive(false);
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenDrawingScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GetName()
    {
        if (inputField.GetComponent<InputField>().text != "")
        {
            PlayerPrefs.SetString("playerName", inputField.GetComponent<InputField>().text);
            GameController.playerName = PlayerPrefs.GetString("playerName");
            CheckPlayerState();
        }
    }
}
