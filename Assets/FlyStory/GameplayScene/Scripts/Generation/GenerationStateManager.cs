using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationStateManager : MonoBehaviour
{
    public static GenerationStateManager instance;

    public StateBase currentState;

    public StateClear ClearState = new StateClear();
    public StateBlocksOnly BlocksOnlyState = new StateBlocksOnly();
    public StateBlocksEnemies BlocksEnemiesState = new StateBlocksEnemies();

    public Transform enemiesParent;

    [SerializeField] private Transform _generationChecker;
    [SerializeField] private GameObject _floor;
    private GameObject _lastFloor;
    private Vector2 _newPosition;

    private void Awake()
    {
        instance = this;
        currentState = ClearState;
        currentState.EnterState(this);
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (transform.position.x < _generationChecker.position.x)
        {
            _lastFloor = Instantiate(_floor, transform.position, Quaternion.identity);
            _newPosition.y = transform.position.y;
            _newPosition.x = transform.position.x + 100f;
            transform.position = _newPosition;
            currentState.GeneratePlatform(this, _lastFloor.transform);
        }
    }

    public static void SwitchState(StateBase state)
    {
        instance.currentState = state;
        instance.currentState.EnterState(instance);
    }

    public static void SwitchToNextState()
    {
        if (instance.currentState == instance.ClearState)
        {
            SwitchState(instance.BlocksOnlyState);
        }
        else if (instance.currentState == instance.BlocksOnlyState)
        {
            SwitchState(instance.BlocksEnemiesState);
        }
    }
}
