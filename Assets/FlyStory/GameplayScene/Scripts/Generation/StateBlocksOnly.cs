using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBlocksOnly : StateBase
{
    // ѕотом переделать. преграда, бонус, враг
    public int[] generationWeights = { 80, 20, 0 };

    public override void EnterState(GenerationStateManager context)
    {
        Debug.Log("Only generating blocks");
    }

    public override void GeneratePlatform(GenerationStateManager context, Transform parentFloor)
    {
        int weightsSum = 0;
        foreach (var item in generationWeights)
        {
            weightsSum += item;
        }

        float floorPosX = parentFloor.position.x;
        Vector3 blockPosition;
        int vertical = 10;
        int horizontal = 10;
        int randomNumber;
        while (horizontal < 90)
        {
            while (vertical < PlaneController.player.transform.position.y + 150)
            {
                blockPosition = new Vector3(horizontal + floorPosX + Random.Range(-15, 15), vertical + Random.Range(-10, 17));
                int weightSumModified = EnemyManager.EnemySpawnAllowed() ? weightsSum : weightsSum - generationWeights[2];
                randomNumber = Random.Range(0, weightSumModified);
                switch (Utils.PickRandomItem(randomNumber, generationWeights))
                {
                    case 0:
                        BlockManager.SpawnBlock(blockPosition, parentFloor);
                        break;
                    case 1:
                        BonusManager.SpawnBonus(blockPosition, parentFloor);
                        break;
                    case 2:
                        EnemyManager.SpawnEnemy(blockPosition, context.enemiesParent);
                        break;
                }

                vertical += Random.Range(40, 50);
            }
            vertical = 10;
            horizontal += Random.Range(40, 50);
        }
    }

    public override void UpdateState(GenerationStateManager context)
    {
        throw new System.NotImplementedException();
    }
}
