using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject floor;

    [SerializeField] private Transform _generationChecker;
    private GameObject _lastFloor;
    private Vector2 _newPosition;
    #endregion

    #region Lifecycle
    void Update()
    {
        if (transform.position.x < _generationChecker.position.x)
        {
            _lastFloor = Instantiate(floor, transform.position, Quaternion.identity);
            _newPosition.y = transform.position.y;
            _newPosition.x = transform.position.x + 100f;
            transform.position = _newPosition;
            GenerateBlocks(_lastFloor.transform);
        }
    }
    #endregion

    #region Private Methods
    void GenerateBlocks(Transform parentFloor)
    {
        float floorPosX = parentFloor.position.x;
        Vector3 blockPosition;
        int vertical = 10;
        int horizontal = 10;
        while (horizontal < 90)
        {
            while (vertical < 100)
            {
                blockPosition = new Vector3(floorPosX + vertical + Random.Range(-15, 15), horizontal + Random.Range(-10, 17));
                Instantiate(blocks[Random.Range(0, blocks.Length)], blockPosition, Quaternion.identity, parentFloor);
                vertical += Random.Range(40, 50);
            }
            vertical = 10;
            horizontal += Random.Range(40, 50);
        }
    }
    #endregion
}
