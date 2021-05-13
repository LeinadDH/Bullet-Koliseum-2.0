using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShooterBehaviour : MonoBehaviour
{
    public enum ReloadingState { StartReloading, Reloading, Ready, StopReloading}
    public enum ShootingState { Idle, Shoot }

    public GunData data;
    public int bulletRemaining;
    public bool autoReload;
    public SpriteRenderer gunSprite;

    protected ReloadingState reloadingState;
    protected ShootingState shootingState;
    public AudioSource audioSource;

    protected virtual void OnEnable()
    {
        SetNewGunData(data);
        bulletRemaining = data.bulletCapacity;

        StartCoroutine(ShootState());
        StartCoroutine(ReloadState());
        StartCoroutine(IdleState());        
    }

    protected abstract IEnumerator IdleState();

    protected virtual IEnumerator ReloadState()
    {
        while (true)
        {
            yield return new WaitUntil(() => reloadingState==ReloadingState.StartReloading);
            
            while(bulletRemaining < data.bulletCapacity)
            {
                reloadingState = ReloadingState.Reloading;
                float t = 0;                
                yield return new WaitUntil(
                    () =>
                    {
                        t += Time.deltaTime;
                        if (t >= data.bulletReloadTime / data.bulletCapacity)
                        {
                            audioSource.PlayOneShot(data.reloadClip);
                            bulletRemaining++;
                            return true;
                        }
                        else if (reloadingState == ReloadingState.StopReloading)
                            return true;
                        else 
                            return false;
                    });
                if (reloadingState == ReloadingState.StopReloading) break;
            }
            reloadingState = ReloadingState.Ready;
        }
    }

    protected virtual IEnumerator ShootState()
    {
        while (true)
        {
            yield return new WaitWhile(() => bulletRemaining <= 0);

            yield return new WaitUntil(() => shootingState == ShootingState.Shoot);

            if (reloadingState == ReloadingState.Reloading)
            {
                reloadingState = ReloadingState.StopReloading;          // Detiene la recarga de balas
                shootingState = ShootingState.Idle;
                continue;
                //yield return new WaitUntil(() => reloadingState == ReloadingState.Ready);
            }

            Shoot();            

            yield return new WaitForSeconds(data.bulletShotTime);

            shootingState = ShootingState.Idle;
        }
    }

    public virtual void Shoot()
    {
        audioSource.PlayOneShot(data.shootClip);
        bulletRemaining--;

        Instantiate(data.bulletPrefab, transform.position, transform.rotation);
    }

    public virtual void Aim(Vector2 targetPos)
    {
        transform.eulerAngles = Vector3.forward * Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
    }

    public virtual void SetNewGunData(GunData newData)
    {
        data = newData ?? null;
        if (gunSprite)
            gunSprite.sprite = data?.gunSprite ?? null;
    }    
}
