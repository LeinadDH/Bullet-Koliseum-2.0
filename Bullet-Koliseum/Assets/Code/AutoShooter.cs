using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : ShooterBehaviour
{
    public Transform aimTarget;

    protected override IEnumerator IdleState()
    {
        while (true)
        {
            if (bulletRemaining <= 0)
            {
                yield return new WaitWhile(() => !autoReload);

                reloadingState = ReloadingState.StartReloading;
                yield return new WaitUntil(() => bulletRemaining == data.bulletCapacity); // Esperar a que se llene el cargador de balas
                //yield return new WaitUntil(() => bulletRemaning > 0); // Esperar a que se llene el cargador de balas
            }
            else
            {
                shootingState = ShootingState.Shoot;
                yield return new WaitUntil(() => shootingState == ShootingState.Idle);
                //yield return null;
            }
        }
    }

    protected virtual void Update()
    {
        if (aimTarget)
            Aim(aimTarget.position);
    }
}
