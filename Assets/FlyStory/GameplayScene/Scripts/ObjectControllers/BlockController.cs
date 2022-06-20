using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    #region Fields
    public List<GameObject> generationPoints;
    public int minRange = 10;
    public int maxRange = 20;
    public int numOfPoints = 0;
    // временно; когда появятся другие бонусы, поменять на массив возможных бонусов
    public GameObject scoreObject;

    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float speed = 1;

    private float _distance = 0;
    private bool _isVertical;
    private bool _isMoving;
    private bool _isRotating;
    private Vector2 _movePosition;
    private int _range = 0;
    private Rigidbody2D _rb;
    #endregion

    #region Lifecycle
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isVertical = RandomBool();
        _isMoving = RandomBool();
        _isRotating = RandomBool();
    }
    void Start()
    {
        if (_isVertical)
        {
            
            _movePosition = Vector2.up;
        }
        else
        {
            
            _movePosition = Vector2.right;
        }

        if (_isMoving)
        {
            _range = Random.Range(minRange, maxRange);
        }
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.MovePosition(_rb.position + _movePosition * speed);
            _distance = _distance + (_movePosition * speed).magnitude;
            if (_distance >= _range)
            {
                speed *= -1;
                _distance = 0;
            }
        }
        if (_isRotating)
        {
            _rb.MoveRotation(_rb.rotation + rotationSpeed);
        }
    }
    #endregion

    #region Private Methods
    private bool RandomBool()
    {
        if (Random.Range(0, 2) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
