using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestPlayerCharacters : MonoBehaviour
{
    void Start()
    {
        foreach (var player in PlayerConfigurationManager.Instance.playerConfigs)
        {
            player.SpawnPlayer(transform.position);
        }
    }
}
