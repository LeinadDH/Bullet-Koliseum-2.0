using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToFirstScene : MonoBehaviour
{

    void Update()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("PantallaPrincipal");
    }
}
