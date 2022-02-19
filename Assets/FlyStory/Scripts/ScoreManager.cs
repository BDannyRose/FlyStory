using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] private Text scoreText;
    
    public void ChangeScore(int amount)
    {
        _score += amount;
        scoreText.text = "Score: " + amount.ToString();
    }
}
