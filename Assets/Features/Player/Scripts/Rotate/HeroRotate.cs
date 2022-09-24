using Features.StaticData.Hero.Rotate;
using UnityEngine;

namespace Features.Player.Scripts.Rotate
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

    public void RunRotate(Vector3 to) => 
      Rotate(to, rotateData.RunLerpRotateValue);

    private void Rotate(Vector3 to, float lerp)
    {
      float angle = Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg;
      Quaternion rotation = Quaternion.Euler(0,0,angle);
      Quaternion lerpedRotation = Quaternion.Slerp(hero.rotation, rotation, lerp);
      hero.rotation = lerpedRotation;
    }
  }
}