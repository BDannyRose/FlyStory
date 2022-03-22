using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void GetName()
    {
        if (inputField.GetComponent<InputField>().text != "")
        {
            PlayerPrefs.SetString("playerName", inputField.GetComponent<InputField>().text);
            CheckPlayerState();
        }
    }
}
