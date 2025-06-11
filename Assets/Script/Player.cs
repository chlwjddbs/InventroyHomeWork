using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public PlayerInput playerInput;
    public Interaction interaction;

    public Transform weaponPos;

    public LayerMask interactionMask;
    public LayerMask targetMask;

    public Equipment equipment;

    public void Init()
    {
        playerInput = GetComponent<PlayerInput>();
        interaction = new Interaction(5, interactionMask);
        inventory = new Inventory(this);
        equipment = GetComponent<Equipment>();
        equipment.Init(this);
       
        playerInput.actions["Interaction"].started += OnInteraction;
    }

    private void Update()
    {
        interaction.GetInteraction();
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        interaction.OnInteraction();
    }
}
