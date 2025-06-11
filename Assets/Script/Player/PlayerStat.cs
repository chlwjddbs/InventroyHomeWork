using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public Player player;

    public float baseHealth;
    public float currentHealth;
    public float addHealth;

    public float moveSpeed;
    public float addMoveSpeed;

    public bool isDeath;

    public void Init(Player player)
    {
        this.player = player;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public float GetTotalHealth()
    {
        return baseHealth + addHealth;
    }

    public float GetTotalMoveSpeed()
    {
        return moveSpeed + addMoveSpeed;
    }

    public void ApplayItemEffect(ConsumeEffect consumeEffect)
    {

    }


    public void Death()
    {
        if (!isDeath)
        {
            isDeath = true;
            player.controller.ChangeState(PlayerStateEnum.Death);
        }
    }
}
