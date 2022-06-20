using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : EnemyBase
{
    private Stats.KamikazeStats _stats;

    private float MovX = 1;
    private Vector2 direction;
    private float cross;
    private Vector2 velocity;
    private float thrustForce;
    private Vector2 relativeForce;

    public override bool Attack(out float attackCooldown)
    {
        attackCooldown = 0;
        return false;
    }

    public override void Die()
    {
        EnemyManager.enemyDeath.Invoke(_stats.scorePoints);
        EnemyManager.DestroyEnemy(_gameObject);
    }

    public override void Initialize(GameObject gameObjSelf)
    {
        _gameObject = gameObjSelf;
        _transform = _gameObject.transform;
        _rb = _gameObject.GetComponent<Rigidbody2D>();
        _stats = EnemyStats.instance.enemyTypeStats.kamikaze;
        _player = PlaneController.player.transform;
        _playerRB = PlaneController.player.GetComponent<Rigidbody2D>();
        _target = _player;
    }

    public override bool IsAtGoodSpot()
    {
        return false;
    }

    public override void Move()
    {
        direction = _transform.position - _target.position;
        direction.Normalize();
        cross = Vector3.Cross(direction, _transform.right).z;
        _rb.angularVelocity = _stats.rotationControl * cross;

        velocity = _transform.right * MovX * _stats.acceleration;
        _rb.AddForce(velocity);
        thrustForce = Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.down)) * _stats.thrustModifier;
        relativeForce = Vector2.up * thrustForce;
        _rb.AddForce(_rb.GetRelativeVector(relativeForce));

        if (_rb.velocity.magnitude > _stats.speed)
        {
            _rb.velocity = _rb.velocity.normalized * _stats.speed;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _stats.health--;
        if (_stats.health == 0)
        {
            Die();
        }
    }
}
