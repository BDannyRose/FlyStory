using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{
    [SerializeField] private Transform deletionPoint;

    #region Lifecycle
    private void Awake()
    {
        deletionPoint = GameObject.FindWithTag("DeletionPoint").transform;
    }
    void Update()
    {
        if (transform.position.x < deletionPoint.position.x)
        {
            // temporary measure until pooling is implemented
            Destroy(gameObject);
        }
    }
    #endregion
}
