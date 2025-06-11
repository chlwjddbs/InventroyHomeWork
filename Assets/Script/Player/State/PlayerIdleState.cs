using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerIdleState : BaseState
{
    public override void Init(Player player)
    {
        base.Init(player);
        stateType = PlayerStateEnum.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Vector3 input = Vector3.zero;
        input.y = player.Rb.velocity.y;
        player.Rb.velocity = input;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (player.controller.inputDir != Vector3.zero)
        {
            player.controller.ChangeState(PlayerStateEnum.Move);
        }
    }
}
