using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EnemyBase
{
    private Stats.BomberStats _stats;

    private float MovX = 1;
    private Vector2 direction;
    private float cross;
    private Vector2 velocity;
    private float thrustForce;
    private Vector2 relativeForce;

    public override bool Attack(out float attackCooldown)
    {
        if (IsAtGoodSpot())
        {
            GameObject bomb = MonoBehaviour.Instantiate(_stats.weapon, _transform.position + new Vector3(0, -3), Quaternion.identity, null);
            bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, -10), ForceMode2D.Impulse);
            attackCooldown = _stats.attackTimer;
            return true;
        }

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
        _stats = EnemyStats.instance.enemyTypeStats.bomber;
        _player = PlaneController.player.transform;
        _playerRB = PlaneController.player.GetComponent<Rigidbody2D>();
        _target = _player.GetComponent<PlaneController>().bomberTarget.transform;
        _target.position = PlaneController.player.transform.position + new Vector3(50, _stats.attackRange - 10);
    }

    public override void Move()
    {
        direction = _transform.position - _target.position + Vector3.right * 30f;
        direction.Normalize();
        cross = Vector3.Cross(direction, _transform.right).z;
        _rb.angularVelocity = _stats.rotationControl * cross;

        if (_rb.velocity.magnitude > _playerRB.velocity.magnitude * 1.05 && isWithinTargetPositionRange)
        {
            _rb.velocity *= 0.9f;
        }

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

    public override bool IsAtGoodSpot()
    {
        isWithinTargetPositionRange = (_transform.position - _target.position).magnitude < 15;
        isWithinAttackRange = (_transform.position - _player.position).magnitude < _stats.attackRange;

        return isWithinAttackRange && isWithinTargetPositionRange;
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
