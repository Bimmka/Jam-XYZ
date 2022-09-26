using System.Collections;
using Features.Customers.Scripts.Alertness;
using Features.Player.Scripts.Markers;
using Features.Services.Coroutine;
using Features.StaticData.Hero.NPCSearching;
using UnityEngine;

namespace Features.Player.Scripts.Steal
{
  public class HeroNPCSearcher
  {
    private readonly Transform startSearchPoint;
    private readonly HeroNPCSearchingStaticData searchingData;
    private readonly ICoroutineRunner coroutineRunner;

    private Coroutine searchCoroutine;

    private WaitForSecondsRealtime searchDelay;

    private int npcHitCount;
    private readonly RaycastHit2D[] npcHits;

    private int obstacleHitCount;
    private readonly RaycastHit2D[] obstacleHits;

    public NPCAlertnessObserver LastHitNPC { get; private set; }

    public HeroNPCSearcher(HeroInteractionSearchMarker startSearchPoint, HeroNPCSearchingStaticData searchingData, ICoroutineRunner coroutineRunner)
    {
      this.startSearchPoint = startSearchPoint.transform;
      this.searchingData = searchingData;
      this.coroutineRunner = coroutineRunner;
      searchDelay = new WaitForSecondsRealtime(searchingData.SearchDelay);
      npcHits = new RaycastHit2D[searchingData.MaxCount];
      obstacleHits = new RaycastHit2D[searchingData.MaxCount];
    }

    public void StartSearch()
    {
      searchCoroutine = coroutineRunner.StartCoroutine(Searching());
    }

    public void DisableSearch()
    {
      if (LastHitNPC != null)
      {
        DisableStealTip();
        ResetLastHitNPC();
      }

      if (searchCoroutine != null)
      {
        coroutineRunner.StopCoroutine(searchCoroutine);
        searchCoroutine = null;
      }
    }

    public bool IsFoundNPC() => 
      LastHitNPC != null;

    private IEnumerator Searching()
    {
      while (true)
      {
        RaycastNPC();
        RaycastObstacles();

        if (IsHitNonBlockedNPC(npcHitCount, obstacleHitCount) && NPCAlertness(out NPCAlertnessObserver npcAlertness) && npcAlertness.IsStealabale)
        {
          if (IsSameNPC(npcAlertness) == false)
          {
            if (LastHitNPC != null)
            {
              DisableStealTip();
              ResetLastHitNPC();
            }

            SetNewNPC(npcAlertness);
            DisplayStealTip();
          }
        }
        else if (LastHitNPC != null)
        {
          DisableStealTip();
          ResetLastHitNPC();
        }

        yield return searchDelay;
      }
    }

    private void SetNewNPC(NPCAlertnessObserver stateMachine) => 
      LastHitNPC = stateMachine;

    private void DisplayStealTip() => 
      LastHitNPC.ShowStealTip();

    private void DisableStealTip() => 
      LastHitNPC.HideStealTip();

    private void ResetLastHitNPC() => 
      LastHitNPC = null;

    private bool IsSameNPC(NPCAlertnessObserver stateMachine) => 
      LastHitNPC == stateMachine;

    private bool IsHitNonBlockedNPC(in int npcHitCount, in int obstacleHitCount)
    {
      if (npcHitCount == 0)
        return false;
      
      if (obstacleHitCount == 0)
        return true;

      return Magnitude(startSearchPoint.position, npcHits[0].point) < Magnitude(startSearchPoint.position, obstacleHits[0].point);
    }

    private float Magnitude(Vector3 at, Vector3 to) => 
      Vector3.Magnitude(to - at);

    private bool NPCAlertness(out NPCAlertnessObserver npcAlertness) => 
      npcHits[0].collider.TryGetComponent(out npcAlertness);

    private void RaycastObstacles() =>
      obstacleHitCount = Physics2D.RaycastNonAlloc(startSearchPoint.position, startSearchPoint.right,
        obstacleHits, searchingData.Distance, searchingData.ObstacleMask);

    private void RaycastNPC() =>
      npcHitCount = Physics2D.RaycastNonAlloc(startSearchPoint.position, startSearchPoint.right, npcHits,
        searchingData.Distance,
        searchingData.NPCMask);
  }
}