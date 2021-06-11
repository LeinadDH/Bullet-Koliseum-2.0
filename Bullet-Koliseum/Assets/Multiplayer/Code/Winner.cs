using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    [SerializeField] GameObject[] MonaChina;
    [SerializeField] GameObject[] Furro;
    [SerializeField] GameObject[] FireMan;
    [SerializeField] GameObject[] Caballero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayers();

        if(MonaChina.Length == 0 && Furro.Length == 0 && FireMan.Length == 0)
        {
            SceneManager.LoadScene("CaballeroWins");
        }

        if(Furro.Length == 0 && FireMan.Length == 0 && Caballero.Length == 0)
        {
            SceneManager.LoadScene("MonaChinaWins");
        }

        if (FireMan.Length == 0 && Caballero.Length == 0 && MonaChina.Length == 0)
        {
            SceneManager.LoadScene("FurroWins");
        }

        if (Caballero.Length == 0 && MonaChina.Length == 0 && Furro.Length == 0)
        {
            SceneManager.LoadScene("FireManWins");
        }

       
    }

    void FindPlayers()
    {
        MonaChina = GameObject.FindGameObjectsWithTag("LaMonaChina");
        Furro = GameObject.FindGameObjectsWithTag("ElFurro");
        FireMan = GameObject.FindGameObjectsWithTag("ElHombreDeFuego");
        Caballero = GameObject.FindGameObjectsWithTag("ElCaballero");
    }
}
