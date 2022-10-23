using UnityEngine;

namespace Features.Police.Scripts.Path
{
  public class PolicePathObserver
  {
    private readonly PathContainer patrolPath;
    private readonly PathContainer searchingPath;

    public PolicePathObserver(Vector3[] patrolPath, Vector3[] searchingPath)
    {
      this.patrolPath = new PathContainer(patrolPath);
      this.searchingPath = new PathContainer(searchingPath);
    }

    public void RecalculateNearestPatrolPosition(Vector3 policePosition) => 
      patrolPath.RecalculateIndex(policePosition);

    public void RecalculateNearestSearchPosition(Vector3 policePosition) => 
      searchingPath.RecalculateIndex(policePosition);

    public Vector3 PatrolPosition() => 
      patrolPath.Position();

    public Vector3 SearchPosition() =>
      searchingPath.Position();

    public void IncPatrolIndex() => 
      patrolPath.IncIndex();
    
    public void IncSearchIndex() => 
      searchingPath.IncIndex();
  }
}