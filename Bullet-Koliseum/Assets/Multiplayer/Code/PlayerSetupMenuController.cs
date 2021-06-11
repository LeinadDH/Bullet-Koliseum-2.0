using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private UnityEngine.UI.Button readyButton;

    private float IgnoreInpuTime = 1.5f;
    private bool InputEnable;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        IgnoreInpuTime = Time.time + IgnoreInpuTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > IgnoreInpuTime)
        {
            InputEnable = true;
        }
    }

    public void SetPlayer(GameObject prefab)
    {
        if(!InputEnable) { return; }
        PlayerConfigurationManager.Instance.SetPlayerPrefab(PlayerIndex, prefab);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if(!InputEnable) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
