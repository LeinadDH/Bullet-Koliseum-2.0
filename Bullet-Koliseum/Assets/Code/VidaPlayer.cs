using UnityEngine;
using TMPro;

public class VidaPlayer : MonoBehaviour
{
    public GameObject player;
    public float vida = 100;
    public float damageOne = 5;
    public float damageTwo = 1;
    public TextMeshProUGUI showLife;

    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100);
        showLife.text = vida + "%";
        if(vida == 0)
        {
            Destroy(player);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asalto"))
        {
            vida = (vida - damageOne);
        }
        if (other.gameObject.CompareTag("tioGaspacho"))
        {
            vida = (vida - damageTwo);
        }
    }
}
