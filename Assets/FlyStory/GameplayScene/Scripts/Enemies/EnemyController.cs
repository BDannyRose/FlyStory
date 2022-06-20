using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Fighter,
        Bomber,
        Kamikaze,
    }
    public EnemyType type;
    
    private EnemyBase _enemyClass;

    public GameObject targetLockIndicator;
    public bool isTarget = false;

    private bool canAttack = true;
    private float timer;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (canAttack)
        {
            if (_enemyClass.Attack(out timer))
            {
                StartCoroutine(AttackCooldown(timer));
            }
        }
    }

    private void FixedUpdate()
    {
        _enemyClass.Move();
    }

    IEnumerator AttackCooldown(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
        yield break;
    }


    public void Initialize()
    {
        switch (type)
        {
            case EnemyType.Fighter:
                _enemyClass = new Fighter();
                break;
            case EnemyType.Bomber:
                _enemyClass = new Bomber();
                break;
            case EnemyType.Kamikaze:
                _enemyClass = new Kamikaze();
                break;
        }
        _enemyClass.Initialize(gameObject);
    }

    public void SetAsTarget(bool state)
    {
        isTarget = state;
        targetLockIndicator.SetActive(state);
    }
}
