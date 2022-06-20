using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBase
{
    protected GameObject _gameObject;
    protected Rigidbody2D _rb;
    protected Transform _transform;
    protected Transform _target;
    protected Transform _player;
    protected Rigidbody2D _playerRB;

    public bool canAttack;
    protected bool isWithinTargetPositionRange;
    protected bool isWithinAttackRange;

    public abstract void Initialize(GameObject gameObjSelf);

    public abstract bool IsAtGoodSpot();
    
    public abstract void Move();

    public abstract bool Attack(out float attackCooldown);

    public abstract void Die();

}
