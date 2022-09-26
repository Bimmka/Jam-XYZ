using Features.Extensions;
using Features.Player.Scripts.Base;
using Features.Player.Scripts.Rotate;
using Features.StaticData.Hero.Move;
using UnityEngine;
using Zenject;

namespace Features.Player.Scripts.Move
{
  public class HeroMove
  {
    private readonly Transform heroTransform;
    private readonly HeroMoveStaticData moveData;
    private readonly HeroRotate rotate;
    private readonly Rigidbody2D heroBody;

    [Inject]
    public HeroMove(Transform heroTransform, HeroMoveStaticData moveData, HeroRotate rotate, Rigidbody2D body)
    {
      this.heroTransform = heroTransform.transform;
      this.moveData = moveData;
      this.rotate = rotate;
      heroBody = body;
    }

    public void Run(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (heroTransform.right.IsEqualMoveDirection(moveDirection) == false)
        rotate.RunRotate(moveDirection);
      
      heroBody.MovePosition(MovePosition(moveDirection, deltaTime));
    }

    private Vector3 MovePosition(Vector2 direction, float deltaTime)
    {
      return heroBody.position + (direction * (moveData.RunSpeed * deltaTime));
    }

    private Vector3 MoveDirection(Vector2 inputDirection)
    {
      Vector3 worldMoveVector = Vector3.zero;
      if (inputDirection.x != 0)
        worldMoveVector.x = Mathf.Sign(inputDirection.x);

      if (inputDirection.y != 0)
        worldMoveVector.y = Mathf.Sign(inputDirection.y);

      worldMoveVector.z = 0;
      return worldMoveVector.normalized;
    }
  }
}