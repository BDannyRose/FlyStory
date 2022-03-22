using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // потом перенести в отдельный менеджер сцен

public class PlaneController : MonoBehaviour
{
    #region Fields
    public static int health;
    public static float fuel;
    public Upgrades upgrades;
    public Bonuses bonuses;

    [Header("Movement 1")]
    public float maxFuel;
    public int maxHealth;
    public float maxSpeed;
    public float acceleration;
    [SerializeField] private float speed;

    [Header("Movement 2")]
    public float speed2;
    public float acceleration2;
    public float rotationControl;
    float MovY;
    float MovX = 1;

    Touch touch;

    private bool _holdMouse = false;
    private Rigidbody2D _rb;
    #endregion

    #region Lifecycle
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        maxHealth = upgrades.upgradeSystem.hull[upgrades.upgradeSystem.hullLevel].maxHealth;
        maxSpeed = upgrades.upgradeSystem.engine[upgrades.upgradeSystem.engineLevel].maxSpeed;
        acceleration = upgrades.upgradeSystem.engine[upgrades.upgradeSystem.engineLevel].acceleration;
        maxFuel = upgrades.upgradeSystem.fuelTank[upgrades.upgradeSystem.fuelTankLevel].maxFuel;
        speed = maxSpeed / 2;
        health = maxHealth;
        fuel = maxFuel;
        HealthBar.instance.SetMaxHealth(maxHealth);
        HealthBar.instance.SetHealth(health);
        FuelBar.instance.SetMaxFuel(maxFuel);
        FuelBar.instance.SetFuel(fuel);
    }

    void Update()
    {
        // Movement 1
        if (GameController.movementMode == GameController.mMode.Trigonometrical)
        {
            if (speed <= maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
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
        // Movement 2
        else if (GameController.movementMode == GameController.mMode.Physical)
        {
            //if (Input.touchCount > 0)
            //{
            //    touch = Input.GetTouch(0);
            //    if (touch.phase == TouchPhase.Stationary)
            //    {
            //        MovY = Mathf.Clamp(touch.position.y - Screen.height / 3, -1, 1);
            //    }
            //    else
            //    {
            //        MovY = 0;
            //    }
            //    Debug.Log(MovY.ToString());
            //}
            MovY = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        // Movement 1
        if (GameController.movementMode == GameController.mMode.Trigonometrical)
        {
            if (_holdMouse && fuel > 0)
            {
                StartCoroutine(Boost());
            }
            else
            {
                StopCoroutine(Boost());
                if (fuel < maxFuel)
                {
                    fuel += 0.05f * maxFuel * Time.deltaTime;
                }
                _rb.velocity = (Vector2)transform.right * speed;
                _rb.rotation -= _rb.velocity.magnitude * Time.deltaTime * Mathf.Cos((50 - _rb.rotation) * Mathf.PI / 180);
            }
        }
        // Movement 2
        else if (GameController.movementMode == GameController.mMode.Physical)
        {
            Vector2 velocity = transform.right * MovX * acceleration2;
            _rb.AddForce(velocity);

            float direction = Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.right));

            if (acceleration2 > 0)
            {
                if (direction > 0)
                {
                    _rb.rotation += MovY * rotationControl * (_rb.velocity.magnitude / speed2);
                }
                else
                {
                    _rb.rotation -= MovY * rotationControl * (_rb.velocity.magnitude / speed2);
                }
            }

            float thrustForce = Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.down)) * 4f;

            Vector2 relativeForce = Vector2.up * thrustForce;
            _rb.AddForce(_rb.GetRelativeVector(relativeForce));

            if (_rb.velocity.magnitude > speed2)
            {
                _rb.velocity = _rb.velocity.normalized * speed2;
            }
        }
    }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            GameController.isDead = true;
        }
        if (collision.transform.CompareTag("Block"))
        {
            if (bonuses.bonuses.armor.active)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                health--;
            }
            HealthBar.instance.SetHealth(health);
            if (health <= 0)
            {
                GameController.isDead = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bonus")
        {
            int id = collision.GetComponent<BonusID>().bonusID;
            bonuses.Activate(id);
            Destroy(collision.gameObject);
        }
    }


    IEnumerator Boost()
    {
        _rb.velocity = (Vector2)transform.right * speed * 1.5f;
        _rb.rotation += _rb.velocity.magnitude * Time.deltaTime * Mathf.Sin((50 - _rb.rotation) * Mathf.PI / 180);
        fuel -= 0.1f;
        yield return null;
    }
}
