using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public PlayerInput playerInput;

    public Transform weaponPos;

    public LayerMask interactionMask;
    public LayerMask targetMask;
    public LayerMask groundMask;

    public Equipment equipment;

    public Transform cameraContainer;
    public Transform groundCheckPoint;

    public Rigidbody Rb;

    public PlayerController controller;

    public PlayerStat stat;

    

    private void Update()
    {
        if (stat.isDeath) return;
        controller.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (stat.isDeath) return;
        controller.OnFixedUpdate();
    }


    public void Init()
    {
        stat = GetComponent<PlayerStat>();
        stat.Init(this);

        playerInput = GetComponent<PlayerInput>();
        RegisterState();
        Rb = GetComponent<Rigidbody>();
        inventory = new Inventory(this);
        equipment = GetComponent<Equipment>();
        equipment.Init(this);
       
    }

    public void RegisterState()
    {
        controller = new PlayerController(this, new PlayerIdleState());
        controller.RegisterState(new PlayerMoveState());
        controller.RegisterState(new PlayerJumpState());
    }
}
public enum PlayerStateEnum
{
    Idle,
    Move,
    Jump,
    Death
}