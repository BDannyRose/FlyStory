using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    public enum mMode { Trigonometrical, TrigonometricalTouch, Physical }
    public static mMode movementMode = mMode.Trigonometrical;
    public static bool isDead = false;


    void Start()
    {
        isDead = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        DeathScreen.instance.Activate(true);
    }
}
