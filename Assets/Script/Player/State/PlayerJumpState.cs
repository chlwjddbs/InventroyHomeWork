using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem;

public class PlayerJumpState : BaseState
{ 
    private Vector3 jumpVelocity; 
    private float jumpPower;
    private bool isGround;
    private float checkDistance;
    private float jumpElapsed;    
    private float jumpMoveSpeed;

    public override void Init(Player player)
    {
        base.Init(player);
        stateType = PlayerStateEnum.Jump;
        player.playerInput.actions["Jump"].started += OnJump;
        isGround = true;
        jumpPower = 6f;
        checkDistance = 0.1f;
        jumpElapsed = 0.2f;
        jumpMoveSpeed = 5f;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        isGround = false;

        jumpVelocity = player.Rb.velocity;
        jumpVelocity.y = 0;
        player.Rb.velocity = jumpVelocity; 
        player.Rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Vector3 input = ((player.transform.forward * player.controller.inputDir.y) + (player.transform.right * player.controller.inputDir.x)) * jumpMoveSpeed;
        input.y = player.Rb.velocity.y; 
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        RaycastHit hit;
        if (jumpElapsed > elapsedTime) return;

        for (int i = 0; i < player.groundCheckPoint.childCount; i++)
        {
            Transform checkPoint = player.groundCheckPoint.GetChild(i);
            if (Physics.Raycast(checkPoint.position, Vector3.down, out hit, checkDistance, player.groundMask) && !isGround)
            {
                isGround = true;
                player.controller.ChangeState(PlayerStateEnum.Idle);
                break;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (player.controller.IsActionAble() && isGround)
        {
            player.controller.ChangeState(PlayerStateEnum.Jump);
        }
    }
}
