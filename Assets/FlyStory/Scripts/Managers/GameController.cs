using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    public enum mMode { Trigonometrical, /*TrigonometricalTouch,*/ Physical }
    public static mMode movementMode = mMode.Trigonometrical;
    public static bool isDead = false;
    public static string playerName;

    // потом переделать - переключение стадий игры
    private int[] nextStageScoreRequirement = { 10, 50, 400, int.MaxValue };
    public int currentStage = 0;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        isDead = false;
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (File.Exists(Application.persistentDataPath + "/sprites/" + "user.png"))
            {
                Texture2D texture2d = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                byte[] byteArray = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/sprites/" + "user.png");
                Debug.Log(Application.persistentDataPath);
                texture2d.LoadImage(byteArray);
                Sprite sprite = Sprite.Create(texture2d, new Rect(Vector2.zero, new Vector2(texture2d.width, texture2d.height)), new Vector2(0.5f, 0.5f), 50);
                PlaneController.player.GetComponent<SpriteRenderer>().sprite = sprite;
                PlaneController.player.AddComponent<PolygonCollider2D>();
            }
            else
            {
                PlaneController.player.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    void Update()
    {
        if (isDead)
        {
            Die();
        }
        if (ScoreManager.score > nextStageScoreRequirement[currentStage])
        {
            currentStage++;
            GenerationStateManager.SwitchToNextState();
            Debug.Log("Generation switched to state: " + GenerationStateManager.instance.currentState);
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        DeathScreen.instance.Activate(true);
    }
}
