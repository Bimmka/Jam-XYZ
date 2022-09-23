using Features.StaticData.Hero.Rotate;
using UnityEngine;

namespace Features.Hero.Scripts.Rotate
{
  public class HeroRotate
  {
    private readonly Transform hero;
    private readonly HeroRotateStaticData rotateData;

    public HeroRotate(Transform hero, HeroRotateStaticData rotateData)
    {
      this.hero = hero;
      this.rotateData = rotateData;
    }

    public void WalkRotate(Vector3 to) => 
      Rotate(to, rotateData.WalkLerpRotateValue);

    public void RunRotate(Vector3 to) => 
      Rotate(to, rotateData.RunLerpRotateValue);

    private void Rotate(Vector3 to, float lerp)
    {
      Quaternion rotation = Quaternion.LookRotation(to);
      Quaternion lerpedRotation = Quaternion.Slerp(hero.rotation, rotation, lerp);
      float magnitude = hero.forward.magnitude;
      Vector3 nextPosition = new Vector3(Mathf.Sin(lerpedRotation.eulerAngles.y * Mathf.Deg2Rad), 0 , Mathf.Cos(lerpedRotation.eulerAngles.y * Mathf.Deg2Rad));
      Debug.DrawLine(hero.position, hero.position + nextPosition, Color.red, 5f);
      hero.rotation = lerpedRotation;
    }
  }
}