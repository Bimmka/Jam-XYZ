using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Police.Data
{
  [CreateAssetMenu(fileName = "PolicesContainerSettings", menuName = "StaticData/Police/Create Polices Container Settings", order = 52)]
  public class PolicesContainerSettings : SerializedScriptableObject
  {
    public Dictionary<PoliceType, PoliceSettings> Settings;
  }
}