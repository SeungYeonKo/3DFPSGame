using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThorwEvent : MonoBehaviour
{
    private PlayerBombFireAbility _owner;

    private void Start()
    {
        _owner = GetComponentInParent<PlayerBombFireAbility>();
    }

    public void ThrowEvent()
    {
        Debug.Log("어택 이벤트 발생!");
        _owner.ThrowEvent();
    }
}
