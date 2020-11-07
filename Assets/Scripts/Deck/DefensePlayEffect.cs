using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefensePlayEffect", menuName = "CardData/PlayEffect/Defense")]
public class DefensePlayEffect : CardPlayEffect
{
    [SerializeField] int _amount = 10;
    public override void Activate(ITargetable target)
    {
        //test to see if the target is healable
        IHealable objectToHeal = target as IHealable;
        //if it is, apply damage
        if (objectToHeal != null)
        {
            objectToHeal.Shield(_amount);
            Debug.Log("Add defense to the target");
        }
        else
        {
            Debug.Log("Target is not healable...");
        }
    }
}
