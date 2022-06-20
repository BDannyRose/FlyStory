using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    #region Fields
    [SerializeField] private int _score;
    [SerializeField] private TextMesh scoreText;

    //private float _scale;
    #endregion

    #region Lifecycle

    private void Awake()
    {
        //_scale = _score / 10f;
    }
    void Start()
    {
        scoreText.text = _score.ToString();
        //gameObject.transform.localScale = new Vector3(_scale, _scale, 1);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.ChangeScore(_score);
            Destroy(gameObject);
        }
    }

}
