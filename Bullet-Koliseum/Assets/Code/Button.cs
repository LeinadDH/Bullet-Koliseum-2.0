using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Menu;
    public void Continue()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
    }
}
