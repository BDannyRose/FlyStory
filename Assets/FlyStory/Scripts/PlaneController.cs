using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    #region Fields
    
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float speed;
    private bool _holdMouse = false;
    private Rigidbody2D _rb;
    #endregion

    #region Lifecycle
    private void Awake()
    {
        speed = maxSpeed / 2;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (speed <= maxSpeed)
        {
            speed += 1 * Time.deltaTime;
        }
        else
        {
            speed -= 2 * Time.deltaTime;
        }
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
        if (_holdMouse)
        {
            _rb.velocity = (Vector2)transform.right * speed * 1.5f;
            _rb.rotation += _rb.velocity.magnitude * Time.deltaTime * Mathf.Sin((50 - _rb.rotation) * Mathf.PI / 180);
        }
        else
        {
            _rb.velocity = (Vector2)transform.right * speed;
            _rb.rotation -= _rb.velocity.magnitude * Time.deltaTime * Mathf.Cos((50 - _rb.rotation) * Mathf.PI / 180);
        }
    }
    #endregion
}
