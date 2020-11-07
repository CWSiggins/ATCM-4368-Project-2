using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealPlayEffect", menuName = "CardData/PlayEffect/Heal")]
public class HealPlayEffect : CardPlayEffect
{
    [SerializeField] int _healAmount = 10;
    public override void Activate(ITargetable target)
    {
        //test to see if the target is healable
        IHealable objectToHeal = target as IHealable;
        //if it is, apply damage
        if (objectToHeal != null)
        {
            objectToHeal.Heal(_healAmount);
            Debug.Log("Add health to the target");
        }
        else
        {
            Debug.Log("Target is not healable...");
        }
    }
}
