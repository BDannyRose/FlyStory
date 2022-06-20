using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{

    public abstract void EnterState(GenerationStateManager context);

    public abstract void UpdateState(GenerationStateManager context);

    public abstract void GeneratePlatform(GenerationStateManager context, Transform parentFloor);
}
