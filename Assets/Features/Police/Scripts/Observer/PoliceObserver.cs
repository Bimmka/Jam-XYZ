using System.Collections.Generic;
using Features.Police.Scripts.Factory;
using Features.Police.Scripts.PoliceStates;
using Features.StaticData.LevelArea;

namespace Features.Police.Scripts.Observer
{
  public class PoliceObserver
  {
    private readonly PoliceFactory factory;

    private readonly Dictionary<LevelAreaType, List<PoliceOfficer>> officersByArea;

    public PoliceObserver(PoliceFactory factory)
    {
      this.factory = factory;
      officersByArea = new Dictionary<LevelAreaType, List<PoliceOfficer>>(3);
    }

    public void Spawn(PoliceLevelData[] policeLevelDatas)
    {
      for (int i = 0; i < policeLevelDatas.Length; i++)
      {
        SavePolice(policeLevelDatas[i].Area, factory.Spawn(policeLevelDatas[i]));
      }
    }

    private void SavePolice(LevelAreaType areaType, PoliceOfficer officer)
    {
      if (officersByArea.ContainsKey(areaType) == false)
        officersByArea.Add(areaType, new List<PoliceOfficer>(4));
      
      officersByArea[areaType].Add(officer);
    }
  }
}