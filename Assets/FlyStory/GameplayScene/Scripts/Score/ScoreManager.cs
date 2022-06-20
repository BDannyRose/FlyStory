using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public static ScoreManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
        EnemyManager.enemyDeath += ChangeScore;
    }

    private float timePerPoint = 1f;
    private float timer = 0;

    [SerializeField] private Text scoreText;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timePerPoint)
        {
            ChangeScore(1);
            timer = 0;
        }
    }

    public static void ChangeScore(int amount)
    {
        score += amount;
        instance.scoreText.text = "Score: " + score.ToString();
    }
}
