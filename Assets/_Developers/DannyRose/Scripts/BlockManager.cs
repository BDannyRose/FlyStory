using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;

    [SerializeField] private GameObject[] blocks;

    private void Awake()
    {
        instance = this;
    }

    public static GameObject SpawnBlock(Vector3 position, Transform parent)
    {
        GameObject bonus = Instantiate(instance.blocks[Random.Range(0, instance.blocks.Length)], position, Quaternion.identity, parent);
        return bonus;
    }

    public void DestroyBlock(GameObject bonus)
    {
        Destroy(bonus);
    }
}
