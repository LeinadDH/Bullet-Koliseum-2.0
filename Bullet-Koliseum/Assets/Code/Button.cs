using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MusicMenu;
    public void Continue()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
    }
    public void ShowMusic()
    {
        Menu.SetActive(false);
        MusicMenu.SetActive(true);
    }
    public void ExitMusic()
    {
        Menu.SetActive(true);
        MusicMenu.SetActive(false);
    }
}
