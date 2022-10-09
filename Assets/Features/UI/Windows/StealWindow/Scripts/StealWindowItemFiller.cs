using System;
using System.Collections.Generic;
using System.Linq;
using Features.StealItems.Scripts;
using Features.UI.Windows.StealWindow.StealFillPatterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class StealWindowItemFiller
  {
    private readonly StealItemFactory factory;
    private readonly int minCoinsInSpawn;
    private readonly int entitiesCount;
    private readonly Dictionary<Vector2Int, Transform> spawnPoints;

    private int currentSpawnedCoins;
    private int currentSpawnedEntities;

    public StealWindowItemFiller(StealItemFactory factory, StealWindowCell[] spawnPoints, int minCoinsInSpawn)
    {
      this.factory = factory;
      this.minCoinsInSpawn = minCoinsInSpawn;
      this.spawnPoints = spawnPoints.ToDictionary(x=>x.Position, x=>x.Transform);
      entitiesCount = Enum.GetNames(typeof(StealItemType)).Length;
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
        {
          (Transform, Transform) bounds = Bounds(cells[i].Position);
          SpawnMovingBlock(SpawnParent(cells[i].Position), bounds.Item1, bounds.Item2);
        }
      }
    }

    private StealItem SpawnEntity(Transform parent, int spawnedEntitiesCount, int totalEntitiesCount)
    {
      if (spawnedEntitiesCount == totalEntitiesCount - 1 && currentSpawnedCoins < minCoinsInSpawn)
        return SpawnMoney(parent);
      else
        return SpawnRandomEntity(parent);
    }

    private StealItem SpawnMoney(Transform parent)
    {
      int random = Random.Range(0, 2);
      IncSpawnedCoinsCount();
      return factory.SpawnEntity(random == 0 ? StealItemType.SilverCoin : StealItemType.GoldCoin, parent);
    }

    private StealItem SpawnRandomEntity(Transform parent)
    {
      StealItemType randomEntity = (StealItemType) Random.Range(0, entitiesCount);

      if (randomEntity == StealItemType.GoldCoin || randomEntity == StealItemType.SilverCoin)
        IncSpawnedCoinsCount();
      return factory.SpawnEntity(randomEntity, parent);
    }

    private void SpawnMovingBlock(Transform parent, Transform leftPoint, Transform rightPoint) => 
      factory.SpawnMovingBlock(parent, leftPoint, rightPoint);

    private Transform SpawnParent(Vector2Int position) => 
      spawnPoints[position];

    private (Transform, Transform) Bounds(Vector2Int position)
    {
      IEnumerable<KeyValuePair<Vector2Int, Transform>> positionsInRow = spawnPoints.Where(x => x.Key.y == position.y);
      int minX = 10;
      int maxX = 0;
      (Transform, Transform) bounds = (null, null);

      foreach (KeyValuePair<Vector2Int,Transform> valuePair in positionsInRow)
      {
        if (minX >= valuePair.Key.x)
        {
          minX = valuePair.Key.x;
          bounds.Item1 = valuePair.Value;
        }

        if (maxX <= valuePair.Key.x)
        {
          maxX = valuePair.Key.x;
          bounds.Item2 = valuePair.Value;
        }
      }
      return bounds;
    }

    private void IncSpawnedCoinsCount() => 
      currentSpawnedCoins++;
  }
}