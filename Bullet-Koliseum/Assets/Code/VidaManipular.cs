using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaManipular : MonoBehaviour
{
    VidaPlayer playerVida;

    public int cantidad;
    public float damage = 10;

    void Start()
    {
        //playerVida = GameObject.GetComponent<VidaPlayer>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("aquiVaElTag"))
        {
            playerVida.vida = (playerVida.vida - damage);
            Debug.Log("Si colisiona pero no baja vida :c");
        }
    }
}
