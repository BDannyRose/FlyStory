using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    [System.Serializable]
    public class UpgradeSystem
    {
        public int hullLevel;
        public Hull[] hull;

        public int fuelTankLevel;
        public FuelTank[] fuelTank;

        public int engineLevel;
        public Engine[] engine;

        public bool isEmpty()
        {
            if (hull.Length == 0 || fuelTank.Length == 0 || engine.Length == 0)
            {
                Debug.LogWarning("Must initialize upgrade system");
                return true;
            }
            return false;
        }
    }
    public class Item
    {
        public string name;
        public string description;
        public int unlockCost;
    }

    [System.Serializable]
    public class Hull: Item
    {
        public int maxHealth;
    }

    [System.Serializable]
    public class Engine: Item
    {
        public float acceleration;
        public float maxSpeed;
    }

    [System.Serializable]
    public class FuelTank: Item
    {
        public int maxFuel;
    }
}

public class Upgrades : MonoBehaviour
{
    public Upgrade.UpgradeSystem upgradeSystem;

    void Awake()
    {
        if (!upgradeSystem.isEmpty())
        {
            upgradeSystem.hullLevel = PlayerPrefs.GetInt(PlayerPrefs.GetString("playerName") + "hullLevel", 0);
            upgradeSystem.fuelTankLevel = PlayerPrefs.GetInt(PlayerPrefs.GetString("playerName") + "fuelTankLevel", 0);
            upgradeSystem.engineLevel = PlayerPrefs.GetInt(PlayerPrefs.GetString("playerName") + "engineLevel", 0);
        }
    }
}
