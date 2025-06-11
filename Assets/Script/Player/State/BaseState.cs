using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public BaseState() { }

    protected float elapsedTime;

    protected Player player;
    protected PlayerStateEnum stateType;

    public virtual void Init(Player player)
    {
        this.player = player;
        elapsedTime = 0;
    }

    public virtual void OnEnter()
    {
        //Debug.Log(this.GetType().Name);
        elapsedTime = 0;
    }

    public virtual void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
    }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit()
    {
        elapsedTime = 0;
    }

    public PlayerStateEnum GetState()
    {
        return stateType;
    }
}
