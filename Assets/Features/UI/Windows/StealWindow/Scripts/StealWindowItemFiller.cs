using System;
using System.Collections.Generic;
using System.Linq;
using Features.UI.Windows.StealWindow.StealFillPatterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class StealWindowItemFiller
  {
    private readonly StealItemFactory factory;
    private readonly int minCoinsInSpawn;
    private readonly Dictionary<Vector2Int, Transform> spawnPoints;

    private int currentSpawnedCoins;
    private int currentSpawnedEntities;

    public StealWindowItemFiller(StealItemFactory factory, StealWindowCell[] spawnPoints, int minCoinsInSpawn)
    {
      this.factory = factory;
      this.minCoinsInSpawn = minCoinsInSpawn;
      this.spawnPoints = spawnPoints.ToDictionary(x=>x.Position, x=>x.Transform);
    }

    public void SpawnObjects(StealWindowFillCell[] cells, int entitiesInPatters)
    {
      for (int i = 0; i < cells.Length; i++)
      {
        if (cells[i].AggregateType == StealCellAggregateType.Entity)
        {
          SpawnEntity(SpawnParent(cells[i].Position), currentSpawnedEntities, entitiesInPatters);
          currentSpawnedEntities++;
        }
        else if (cells[i].AggregateType == StealCellAggregateType.MovingBlock)
          SpawnMovingBlock(SpawnParent(cells[i].Position));
      }
    }

    private void SpawnEntity(Transform parent, int spawnedEntitiesCount, int totalEntitiesCount)
    {
      if (spawnedEntitiesCount == totalEntitiesCount - 1 && currentSpawnedCoins < minCoinsInSpawn)
        SpawnMoney(parent);
      else
        SpawnRandomEntity(parent);
    }

    private void SpawnMoney(Transform parent)
    {
      int random = Random.Range(0, 2);
      factory.SpawnEntity(random == 0 ? StealItemType.SilverCoin : StealItemType.GoldCoin, parent);
      IncSpawnedCoinsCount();
    }

    private void SpawnRandomEntity(Transform parent)
    {
      int entitiesCount = Enum.GetNames(typeof(StealItemType)).Length;
      StealItemType randomEntity = (StealItemType) Random.Range(0, entitiesCount);
      factory.SpawnEntity(randomEntity, parent);

      if (randomEntity == StealItemType.GoldCoin || randomEntity == StealItemType.SilverCoin)
        IncSpawnedCoinsCount();
    }

    private void SpawnMovingBlock(Transform parent) => 
      factory.SpawnMovingBlock(parent);

    private Transform SpawnParent(Vector2Int position) => 
      spawnPoints[position];

    private void IncSpawnedCoinsCount() => 
      currentSpawnedCoins++;
  }
}