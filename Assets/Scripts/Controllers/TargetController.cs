using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    //TODO build a more structured connection
    public static ITargetable CurrentTarget;
    //interfaces don't serialize, so need class reference
    [SerializeField] Creature _enemy = null;
    [SerializeField] PlayerStats _player = null;

    [SerializeField] Button _targetEnemy;
    [SerializeField] Button _targetSelf;

    public bool targetSelected;

    private void Start()
    {
        _targetEnemy.onClick.AddListener(TargetEnemy);
        _targetSelf.onClick.AddListener(TargetPlayer);
    }

    public void TargetEnemy()
    {
        //target the object if it is targetable
        ITargetable possibleTarget = _enemy.GetComponent<ITargetable>();
        if (possibleTarget != null)
        {
            Debug.Log("New target acquired!");
            CurrentTarget = possibleTarget;
            _enemy.Target();
            targetSelected = true;
        }
    }

    public void TargetPlayer()
    {
        //target the object if it is targetable
        ITargetable possibleTarget = _player.GetComponent<ITargetable>();
        if (possibleTarget != null)
        {
            Debug.Log("New target acquired!");
            CurrentTarget = possibleTarget;
            _player.Target();
            targetSelected = true;
        }
    }
}
