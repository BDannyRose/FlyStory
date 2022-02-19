using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float maxSpeedX = 50f;
    [SerializeField] private float maxSpeedY = 10f;

    private bool _holdMouse = false;
    private Rigidbody2D _rb;
    #endregion

    #region Lifecycle
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _holdMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _holdMouse = false;
        }
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.x < maxSpeedX)
        {
            _rb.AddForce(new Vector2(15, 2), ForceMode2D.Force);
        }

        if (_holdMouse && transform.position.y < 100)
        {
            if (_rb.velocity.y < maxSpeedY)
            {
                _rb.AddForce(new Vector2(5, 25), ForceMode2D.Force);
            }
            if (_rb.rotation < 30)
            {
                _rb.MoveRotation(_rb.rotation + 0.5f);
            }
            else if (_rb.rotation > 40)
            {
                _rb.MoveRotation(_rb.rotation - 0.5f);
            }
        }
        else
        {
            if (_rb.rotation > -30)
            {
                _rb.MoveRotation(_rb.rotation - 0.4f);
            }
            else if (_rb.rotation < -40)
            {
                _rb.MoveRotation(_rb.rotation + 0.4f);
            }
        }
    }
    #endregion
}
