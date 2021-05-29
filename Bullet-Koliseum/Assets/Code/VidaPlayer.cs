using UnityEngine;
using TMPro;
using DG.Tweening;

public class VidaPlayer : MonoBehaviour
{
    public GameObject player;
    public float vida = 100;
    public float damageOne = 5;
    public float damageTwo = 1;

    public Sprite playerFace;
    public PlayerLifeHUD playerLifePrefab;
    PlayerLifeHUD currentPlayerLifePrefab;
    Tween tw;

    private void Awake()
    {
        GameObject lifeCanvas = GameObject.FindGameObjectWithTag("LifeCanvas");
        if (lifeCanvas)
        {
            currentPlayerLifePrefab = Instantiate(playerLifePrefab, lifeCanvas.transform);
            currentPlayerLifePrefab.faceSprite.sprite = playerFace;
        }
    }

    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100);

        if (currentPlayerLifePrefab)
        {
            currentPlayerLifePrefab.numbers.text = vida + "%";
            currentPlayerLifePrefab.lifeBar.fillAmount = (vida / 100f);
            currentPlayerLifePrefab.lifeBar.color = Color.Lerp(Color.red, Color.green, (vida / 100f));
        }

        if(vida == 0)
        {
            Destroy(player);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asalto")|| other.gameObject.CompareTag("tioGaspacho"))
        {
            float damage;

            switch (other.gameObject.tag)
            {
                case "Asalto":
                    damage = damageOne;
                    break;

                case "tioGaspacho":
                    damage = damageTwo;
                    break;

                default:
                    damage = 0;
                    break;
            }

            vida = (vida - damage);

            if (damage != 0 && !tw.IsActive())
                tw = currentPlayerLifePrefab.transform.DOPunchRotation(Vector3.forward, 0.5f, 20, 0.5f);
        }
    }
}
