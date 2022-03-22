using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementModeButton : MonoBehaviour
{
    public Text buttonText;
    private void Start()
    {
        buttonText.text = "Movement mode: " + GameController.movementMode.ToString();
    }

    public void OnButtonClick()
    {
        GameController.movementMode = (GameController.mMode)(((int)GameController.movementMode + 1) 
            % System.Enum.GetValues(typeof(GameController.mMode)).Length);
        buttonText.text = "Movement mode: " + GameController.movementMode.ToString();
    }
}
