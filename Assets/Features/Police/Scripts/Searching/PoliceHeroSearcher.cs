using System;
using Features.Player.Scripts.Base;
using Features.Police.Data;
using UniRx;
using UnityEngine;

namespace Features.Police.Scripts.Searching
{
  public class PoliceHeroSearcher
  {
    private readonly Transform origin;
    private readonly PoliceSettings settings;
    private readonly CompositeDisposable disposable;
    private readonly RaycastHit2D[] playerHits;
    private readonly RaycastHit2D[] obstacleHits;

    private int playerHitCount;
    private int obstacleHitCount;
    
    public HeroStealObserver PlayerHit { get; private set; }
    public PoliceHeroSearcher(Transform origin, PoliceSettings settings)
    {
      this.origin = origin;
      this.settings = settings;
      disposable = new CompositeDisposable();
      playerHits = new RaycastHit2D[settings.PlayerMaxHitCount];
      obstacleHits = new RaycastHit2D[settings.ObstacleMaxHitCount];
    }

    public void StartSearch()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(settings.SearchDelay))
        .Subscribe(onNext => Search())
        .AddTo(disposable);
    }

    public void StopSearch()
    {
      disposable.Clear();
    }

    private void Search()
    {
      playerHitCount = Physics2D.BoxCastNonAlloc(origin.position, settings.PlayerRaycastBoxSize, 0f, origin.up, playerHits,
        settings.RaycastDistance, settings.PlayerMask);

      if (IsFoundPlayer(playerHitCount) && IsPlayerInViewAngle() && IsInSameArea() && IsHaveObstacleOnViewLine() == false)
      {
        if (IsHaveSavedHit() == false)
          SavePlayerHit();
      }
      else if (IsHaveSavedHit())
        ResetHit();
    }

    private void SavePlayerHit() => 
      PlayerHit = playerHits[0].collider.GetComponent<HeroStealObserver>();

    private void ResetHit() => 
      PlayerHit = null;

    private bool IsFoundPlayer(in int hitCount) => 
      hitCount > 0;

    private bool IsPlayerInViewAngle()
    {
      Vector3 directionToPlayer = playerHits[0].transform.position - origin.position;
      float angle = Vector2.Angle(origin.up, directionToPlayer);
      return angle <= settings.AngleOfView;
    }

    private bool IsHaveObstacleOnViewLine()
    {
      obstacleHitCount = Physics2D.CircleCastNonAlloc(origin.position, settings.ObstacleRaycastSphereRadius,
        origin.up, obstacleHits,
        settings.RaycastDistance, settings.ObstacleMask);
      return obstacleHitCount > 0;
    }

    private bool IsInSameArea()
    {
      Debug.Log("Change me (Police)");
      return true;
    }

    private bool IsHaveSavedHit() => 
      PlayerHit != null;
  }
}