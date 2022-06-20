using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class EnemyTypeStats
    {
        public KamikazeStats kamikaze;
        public BomberStats bomber;
        public FighterStats fighter;
        public MissileStats missile;
    }

    public class Stats
    {
        public string name;
        public string description;
        public int scorePoints;
        public int health;
        public float speed;
        public float acceleration;
        public float rotationControl;
        public float thrustModifier;
        public float attackRange;
        public float attackTimer;
        public GameObject weapon;
    }

    [System.Serializable]
    public class KamikazeStats : Stats
    {

    }

    [System.Serializable]
    public class BomberStats : Stats
    {
        
    }

    [System.Serializable]
    public class FighterStats : Stats
    {

    }

    [System.Serializable]
    public class MissileStats : Stats
    {

    }

}

public class EnemyStats : MonoBehaviour
{
    public static EnemyStats instance;

    public Stats.EnemyTypeStats enemyTypeStats;

    private void Awake()
    {
        instance = this;
    }
}
