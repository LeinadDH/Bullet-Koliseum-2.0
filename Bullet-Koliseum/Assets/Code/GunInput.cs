using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lean.Pool;

public class GunInput : MonoBehaviour
{
    Vector3 inverseRotation = new Vector3(0, 180, 0);
    void ReloadInput(InputAction.CallbackContext value)
    {
        reloadingState = ReloadingState.StartReloading;
    }

    void ShootInput(InputAction.CallbackContext value)
    {
        if (bulletRemaining > 0)
        {
            shootingState = ShootingState.Shoot;
        }
        else
        {
            audioSource.PlayOneShot(data.emptyClip);
            if (autoReload)
                reloadingState = ReloadingState.StartReloading;
        }
    }

    public enum ReloadingState { StartReloading, Reloading, Ready, StopReloading }
    public enum ShootingState { Idle, Shoot }

    public GunData data;
    public int bulletRemaining;
    public bool autoReload;
    public SpriteRenderer gunSprite;

    protected ReloadingState reloadingState;
    protected ShootingState shootingState;
    public AudioSource audioSource;

    InputHelper_SideView currentInput;

    private void Awake()
    {
        currentInput = GetComponentInParent<InputHelper_SideView>();
    }

    protected virtual void OnEnable()
    {
        if (currentInput)
        {
            currentInput.onReload += ReloadInput;
            currentInput.onShoot += ShootInput;
        }

        SetNewGunData(data);
        bulletRemaining = data.bulletCapacity;

        StartCoroutine(ShootState());
    }

    private void OnDisable()
    {
        if (currentInput)
        {
            currentInput.onReload -= ReloadInput;
            currentInput.onShoot -= ShootInput;
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
        if (data.shootClip)
            audioSource.PlayOneShot(data.shootClip);
        bulletRemaining--;

        if (gunSprite.flipX == false)
        {
            LeanPool.Spawn(data.bulletPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
            //Instantiate(data.bulletPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
        }
        if (gunSprite.flipX == true)
        {
            LeanPool.Spawn(data.bulletPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(inverseRotation));
            //Instantiate(data.bulletPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(inverseRotation));
        }

    }

    public virtual void SetNewGunData(GunData newData)
    {
        data = newData ?? null;
        if (gunSprite)
            gunSprite.sprite = data?.gunSprite ?? null;
    }

}
