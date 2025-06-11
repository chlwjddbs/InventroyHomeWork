using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem;
using System;

public class PlayerController
{
    public Dictionary<PlayerStateEnum, BaseState> registedState = new Dictionary<PlayerStateEnum, BaseState>();

    public BaseState CurrentState => currnetState;
    protected BaseState currnetState;
    protected BaseState previousState;

    public Interaction interaction;
    private float maxInteractionDistance = 5;

    private Player player;

    public Vector3 inputDir;

    private Vector2 mousePos;

    private float cameraRotateAngle;

    private float maxCameraRotate = 90;
    private float minCameraRotate = -50;

    private float rotateSpeed = 10;
    public PlayerController(Player player, BaseState initState)
    {
        this.player = player;
        RegisterState(initState);
        currnetState = initState;
        currnetState.OnEnter();

        Cursor.visible = false;

        interaction = new Interaction(maxInteractionDistance, player.interactionMask);

        this.player.playerInput.actions["Look"].performed += OnLook;
        this.player.playerInput.actions["Interaction"].started += OnInteraction;
    }

    public void OnUpdate(float deltaTime)
    {
        currnetState.OnUpdate(deltaTime);
        interaction.GetInteraction();
    }

    public void OnFixedUpdate()
    {
        currnetState.OnFixedUpdate();
    }

    public virtual void RegisterState(BaseState state)
    {
        state.Init(player);
        registedState[state.GetState()] = state;
    }

    public void ChangeState(PlayerStateEnum newState)
    {
        if (currnetState.GetState() == newState) return;

        currnetState?.OnExit();
        previousState = currnetState;

        currnetState = registedState[newState];
        currnetState.OnEnter();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        LookRotat();
    }

    public void LookRotat()
    {
        player.transform.rotation *= Quaternion.Euler(0, mousePos.x * rotateSpeed * Time.deltaTime, 0);

        cameraRotateAngle += mousePos.y * rotateSpeed * Time.deltaTime;
        cameraRotateAngle = Mathf.Clamp(cameraRotateAngle, minCameraRotate, maxCameraRotate);
        player.cameraContainer.localRotation = Quaternion.Euler(-cameraRotateAngle, 0, 0);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        interaction.OnInteraction();
    }

    public bool IsActionAble()
    {
        switch (currnetState)
        {
            case PlayerIdleState:
            case PlayerMoveState:
                return true;
            default:
                return false;
        }
    }
}
