using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour
{
    public bool Respawn = false;

    public void OnDisable()
    {
        Respawn = true;
    }

    public void OnEnable()
    {
        Respawn = false;
    }
}
