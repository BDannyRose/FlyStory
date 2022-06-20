using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// будет отвечать за спавн и удаление врагов, за отслеживание количества врагов, за правила генерации врагов
public class EnemyManager : MonoBehaviour
{
    public delegate void enemyKill(int points);
    public static enemyKill enemyDeath;

    public static EnemyManager instance;
    public List<GameObject> activeEnemiesList;
    public int enemyLimit = 20;

    public float enemyDespawnDistance;

    [SerializeField] private GameObject[] enemies;

    private void Awake()
    {
        instance = this;
        activeEnemiesList = new List<GameObject>();
    }

    public static GameObject SpawnEnemy(Vector3 position, Transform parent)
    {
        GameObject enemy = Instantiate(instance.enemies[UnityEngine.Random.Range(0, instance.enemies.Length)], position, Quaternion.identity, parent);
        instance.activeEnemiesList.Add(enemy);
        return enemy;
    }

    public static void DestroyEnemy(GameObject enemy)
    {
        instance.activeEnemiesList.Remove(enemy);
        Destroy(enemy);
    }

    // ломает генерацию, пока не используем
    //public static void DespawnEnemies()
    //{
    //    foreach (var enemy in instance.activeEnemiesList)
    //    {
    //        if (enemy != null && PlaneController.player.transform.position.x - enemy.transform.position.x > instance.enemyDespawnDistance)
    //        {
    //            DestroyEnemy(enemy);
    //        }
    //    }
    //}

    public static bool EnemySpawnAllowed()
    {
        return instance.activeEnemiesList.Count < instance.enemyLimit;
    }
}
