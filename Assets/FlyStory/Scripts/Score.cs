using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    #region Fields
    [SerializeField] private int _score;

    private float _scale;
    private ScoreManager _scoreManager;
    #endregion

    #region Lifecycle
    private void Awake()
    {
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _scale = _score / 10f;
    }
    void Start()
    {
        gameObject.transform.localScale = new Vector3(_scale, _scale, 1);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _scoreManager.ChangeScore(_score);
            Destroy(gameObject);
        }
    }

}
