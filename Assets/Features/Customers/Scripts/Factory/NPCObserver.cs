using Features.StaticData.LevelArea;
using Zenject;

namespace Features.Customers.Scripts.Factory
{
  public class NPCObserver
  {
    private readonly NPCFactory factory;
    private readonly NPCSpawnData[] spawnDatas;

    [Inject]
    public NPCObserver(NPCFactory factory, NPCSpawnData[] spawnDatas)
    {
      this.factory = factory;
      this.spawnDatas = spawnDatas;

      for (int i = 0; i < spawnDatas.Length; i++)
      {
        factory.Spawn(spawnDatas[i]);
      }
    }
  }
}