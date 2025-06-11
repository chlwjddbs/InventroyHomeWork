using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem;

public class PlayerMoveState : BaseState
{
    private bool isRunInput;
    private float runSpeed;

    public override void Init(Player player)
    {
        base.Init(player);
        stateType = PlayerStateEnum.Move;
        player.playerInput.actions["Move"].performed += OnMove;
        player.playerInput.actions["Move"].canceled += OnMoveStop;
        player.playerInput.actions["Run"].performed += input => isRunInput = true;
        player.playerInput.actions["Run"].canceled += input => isRunInput = false;
        runSpeed = 2f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        player.controller.inputDir = context.ReadValue<Vector2>().normalized;
    }

    public void OnMoveStop(InputAction.CallbackContext context)
    {
        player.controller.inputDir = Vector3.zero;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (player.controller.inputDir == Vector3.zero)
        {
            player.controller.ChangeState(PlayerStateEnum.Idle);
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Vector3 input = ((player.transform.forward * player.controller.inputDir.y) + (player.transform.right * player.controller.inputDir.x)) * UpdateMoveSpeed();
        input.y = player.Rb.velocity.y;
        player.Rb.velocity = input;
    }

    private float UpdateMoveSpeed()
    {
        if (isRunInput)
        {
            return player.stat.GetTotalMoveSpeed() * runSpeed;
        }
        else
        {
            return player.stat.GetTotalMoveSpeed();
        }
    }

}
