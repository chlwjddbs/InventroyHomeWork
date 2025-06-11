using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Player player;
    public ItemInstance item;
    public LayerMask targetMask;

    public void Init(Player player, LayerMask targetMask)
    {
        this.player = player;
        this.targetMask = targetMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetMask)
        {
            Debug.Log("공격");
        }
    }
}
