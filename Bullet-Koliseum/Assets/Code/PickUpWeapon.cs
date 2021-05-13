using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public Transform theGunPosition;
    public void OnMouseDown()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.transform.position = theGunPosition.position;
        this.transform.parent = GameObject.Find("GunPosition").transform;
    }
    public void PutDown()
    {
        Input.GetAxis("C");
        this.transform.parent = null;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}