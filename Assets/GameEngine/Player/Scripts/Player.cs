using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public HealthImages healthImages;
    public Image healthImage;

    public float health = 100;
    private float maxHealth;

    public Image healthBar;

    public SubmergedEffect underwater;
    public bool isSubmerged = false;

    public float drawnTime = 2;
    private float drawnTimeLeft;

    public Image dieImage;

    void Start()
    {
        maxHealth = health;
        dieImage.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (health <= 0)
        {
            dieImage.gameObject.SetActive(true);
        }

        float healthPercent = health / maxHealth;
        healthBar.fillAmount = healthPercent;

        if (isSubmerged)
        {
            if (transform.position.y <= underwater.waterHeight)
            {
                if (health > 0 && drawnTimeLeft >= drawnTime)
                {
                    health -= 25;
                    drawnTimeLeft = 0;
                }

                if (drawnTimeLeft < drawnTime)
                {
                    drawnTimeLeft += Time.deltaTime;
                }
                healthImage.sprite = healthImages.drawning;
            }
        }
        else
        {
            if (healthPercent <= 1 && healthPercent > 0.67)
            {
                healthImage.sprite = healthImages.fullHealth;
            }
            else if (healthPercent <= 0.67 && healthPercent > 0.33)
            {
                healthImage.sprite = healthImages.health67;
            }
            else if (healthPercent <= 0.33 && healthPercent >= 0)
            {
                healthImage.sprite = healthImages.health33;
            }
        }

        if (health < 0)
        {
            dieImage.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Underwater"))
        {
            isSubmerged = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Underwater"))
        {
            isSubmerged = false;
        }
    }

    [System.Serializable]
    public class HealthImages
    {
        public Sprite fullHealth;
        public Sprite health67;
        public Sprite health33;
        public Sprite drawning;
    }
}
