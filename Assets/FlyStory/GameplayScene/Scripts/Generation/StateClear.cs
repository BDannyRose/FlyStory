using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateClear : StateBase
{

    public override void EnterState(GenerationStateManager context)
    {
        Debug.Log("No blocks or enemies");
    }

    public override void GeneratePlatform(GenerationStateManager context, Transform parentFloor)
    {
        
    }

    public override void UpdateState(GenerationStateManager context)
    {
        throw new System.NotImplementedException();
    }
}
