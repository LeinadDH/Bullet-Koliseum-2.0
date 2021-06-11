using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestPlayerCharacters : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i <PlayerConfigurationManager.Instance.playerConfigs.Count; i++)
        {
            PlayerConfigurationManager.Instance.playerConfigs[i].SpawnPlayer(transform.position);
        }
    }
}
