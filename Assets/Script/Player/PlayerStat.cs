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

    public Dictionary<ItemEffectType, ApplyItemEcffect> applyItemEffectDictionary = new Dictionary<ItemEffectType, ApplyItemEcffect>();

    public void Init(Player player)
    {
        this.player = player;
        applyItemEffectDictionary[ItemEffectType.health] = new ApplyHealEffect(this);
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

    public void ApplayItemEffect(ItemEffect itemEffect)
    {
        if(applyItemEffectDictionary.TryGetValue(itemEffect.itemEffectType, out ApplyItemEcffect value))
        {
            value.ApplyEffect(itemEffect);
        }
    }

    public void  RecoverHealth(int amount)
    {
        Debug.Log($"{amount} 회복");
        currentHealth = Mathf.Min(currentHealth += amount,GetTotalHealth());
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
