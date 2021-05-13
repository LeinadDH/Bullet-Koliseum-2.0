using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    [Header("Bullet Config")]
    public float speed;
    public float damage;
    public float despawnDelay;

    [Header("Bullet References")]
    public Rigidbody2D rb2D;
    public GameObject bulletBody;
    public ParticleSystem collisionParticles;

    private void Awake()
    {
        if (collisionParticles != null)
        {
            ParticleSystem.MainModule main = collisionParticles.main;
            main.stopAction = ParticleSystemStopAction.Disable;
            collisionParticles.gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable() 
    {
        rb2D.velocity = transform.right * speed;
        Invoke("Despawn", despawnDelay);
    }

    protected virtual void OnDisable()
    {
        Destroy(gameObject);
    }

    protected virtual void Despawn()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        CancelInvoke("Despawn");

        bulletBody.SetActive(false);
        rb2D.simulated = false;

        collision.gameObject.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);        // Aplicar daño de ser posible al objeto con el que colisiono la bala

        if (collisionParticles != null)
        {
            collisionParticles.gameObject.SetActive(true);                      // Activar sistema de particulas y sistemas adjuntos (ejem. AudioSource)
            yield return new WaitWhile(() => collisionParticles.isPlaying);     // Esperar hasta que el sistema de particulas se detenga.
        }

        Despawn();          // Remover bala de la escena
    }
}
