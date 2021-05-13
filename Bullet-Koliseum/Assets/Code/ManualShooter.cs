using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShooter : ShooterBehaviour
{
    protected override IEnumerator IdleState()
    {
        while (true)
        {
            yield return null;

            if (Input.GetButtonDown("Fire1"))
            {
                
            }
            if (Input.GetButtonDown("Fire2"))
            {
                reloadingState = ReloadingState.StartReloading;
            }            
        }
    }

    protected virtual void Update()
    {
        Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }


}
