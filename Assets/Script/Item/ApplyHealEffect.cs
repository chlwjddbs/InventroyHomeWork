using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHealEffect : ApplyItemEcffect
{
    public ApplyHealEffect(PlayerStat stat)
    {
        this.stat = stat;
    }

    public override void ApplyEffect(ItemEffect useEffect)
    {
        stat.RecoverHealth(useEffect.Amount);
    }
}
