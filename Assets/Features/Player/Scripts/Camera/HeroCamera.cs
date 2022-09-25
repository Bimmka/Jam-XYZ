using Cinemachine;
using DG.Tweening;
using Features.StaticData.Hero.Camera;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Player.Scripts.Camera
{
  public class HeroCamera
  {
    private readonly HeroCameraStaticData cameraData;

    private readonly Transform cameraLookAtPoint;

    private Tweener shiftTweener;
    
    public LevelAreaType CurrentArea { get; private set; }

    public HeroCamera(HeroCameraStaticData cameraData, CinemachineVirtualCamera camera, Transform cameraLookAtPoint)
    {
      this.cameraData = cameraData;
      this.cameraLookAtPoint = cameraLookAtPoint;
      CurrentArea = cameraData.StartArea;

      camera.Follow = cameraLookAtPoint;
      camera.LookAt = cameraLookAtPoint;
    }

    public void SetStartPosition(LevelAreaType area) => 
      SetCameraLookAtPointPosition(Position(area));

    public void MoveTo(LevelAreaType area)
    {
      if (shiftTweener != null && shiftTweener.IsActive() && shiftTweener.IsPlaying())
        shiftTweener.Kill();

      shiftTweener = DOTween
        .To(CameraLookAtPointPosition, SetCameraLookAtPointPosition, Position(area), cameraData.MoveDuration);
      UpdateCurrentArea(area);
    }

    private Vector3 Position(LevelAreaType area) => 
      cameraData.Position(area);

    private void SetCameraLookAtPointPosition(Vector3 position) => 
      cameraLookAtPoint.transform.position = position;

    private Vector3 CameraLookAtPointPosition() => 
      cameraLookAtPoint.transform.position;

    private void UpdateCurrentArea(LevelAreaType newDirection) => 
      CurrentArea = newDirection;
  }
}