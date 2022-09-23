﻿using Features.Extensions;
using Features.Hero.Scripts.Rotate;
using Features.StaticData.Hero.Move;
using UnityEngine;

namespace Features.Hero.Scripts.Move
{
  public class HeroMove
  {
    private readonly Transform heroTransform;
    private readonly HeroMoveStaticData moveData;
    private readonly Transform camera;
    private readonly HeroRotate rotate;
    private readonly CharacterController heroController;

    public HeroMove(Transform heroTransform, HeroMoveStaticData moveData, Transform camera, HeroRotate rotate, CharacterController heroController)
    {
      this.heroTransform = heroTransform;
      this.moveData = moveData;
      this.camera = camera;
      this.rotate = rotate;
      this.heroController = heroController;
    }

    public void Walk(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (heroTransform.forward.IsEqualMoveDirection(moveDirection) == false)
        rotate.WalkRotate(moveDirection);

      heroController.Move(moveDirection * (moveData.WalkSpeed * deltaTime));
    }

    public void Run(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (heroTransform.forward.IsEqualMoveDirection(moveDirection) == false)
        rotate.RunRotate(moveDirection);

      heroController.Move(moveDirection * (moveData.RunSpeed * deltaTime));
    }

    public void WalkWithStair(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (heroTransform.forward.IsEqualMoveDirection(moveDirection) == false)
        rotate.RunRotate(moveDirection);

      heroController.Move(moveDirection * (moveData.WalkWithStairsSpeed * deltaTime));
    }

    private Vector3 MoveDirection(Vector2 inputDirection)
    {
      Vector3 worldMoveVector = Vector3.zero;
      if (inputDirection.x != 0)
        worldMoveVector += camera.transform.right * inputDirection.x;

      if (inputDirection.y != 0)
        worldMoveVector += camera.transform.forward * inputDirection.y;

      worldMoveVector.y = 0;
      return worldMoveVector.normalized;
    }
  }
}