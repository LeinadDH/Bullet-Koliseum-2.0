using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;

public class PlayerConfigurationManager : MonoBehaviour
{
    public List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MinPlayers = 2;
    
    [Space]
    public GameObject playerSetupMenuPrefab;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton");

            DestroyImmediate(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
        playerConfigs = new List<PlayerConfiguration>();

        List<Transform> child = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            child.Add(transform.GetChild(i));
        }

        for (int i = 0; i < child.Count; i++)
        {
            Destroy(child[i].gameObject);
        }

        PlayerInputManager.instance.EnableJoining();
    }
    
    public void SetPlayerPrefab(int index, GameObject player)
    {
        playerConfigs[index].playerPrefab = player;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count >= MinPlayers && playerConfigs.TrueForAll(p => p.IsReady))
        {
            PlayerInputManager.instance.DisableJoining();
            SceneManager.LoadScene("MonaChina");
        }
    }

    public void PlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Join" + pi.playerIndex);
        pi.transform.SetParent(transform);
        if(!playerConfigs.Exists(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration( pi));
        }
        CreateSelectionMenu(pi);
    }

    private void CreateSelectionMenu(PlayerInput input)
    {
        Debug.Log(input.name);
        var rootMenu = GameObject.FindGameObjectWithTag("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
        }
    }
}

[System.Serializable]
public class PlayerConfiguration
{
    public PlayerConfiguration() { }
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input;

    public int PlayerIndex;

    public bool IsReady;

    public GameObject playerPrefab;

    public void SpawnPlayer(Vector3 pos)
    {
        InputHelper_SideView inputHelper =playerPrefab.GetComponent<InputHelper_SideView>();
        inputHelper.playerInput = Input;
        GameObject player = Object.Instantiate(playerPrefab, pos, Quaternion.identity);         
    }
}
