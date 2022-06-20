using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public static BonusManager instance;

    [SerializeField] private GameObject[] bonuses;

    private void Awake()
    {
        instance = this;
    }

    public static GameObject SpawnBonus(Vector3 position, Transform parent)
    {
        GameObject bonus = Instantiate(instance.bonuses[Random.Range(0, instance.bonuses.Length)], position, Quaternion.identity, parent);
        return bonus;
    }

    public void DestroyBonus(GameObject bonus)
    {
        Destroy(bonus);
    }
}
