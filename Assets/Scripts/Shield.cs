using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Schield : MonoBehaviour
{

    public float cooldown;

    [HideInInspector] public bool isCooldown;

    private Image shieldImage;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isCooldown = true;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (shieldImage.fillAmount <= 0)
            {
                ResetTime();
                isCooldown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTime()
    {
        shieldImage.fillAmount = 1;
    }

    public void Activate()
    {
        ResetTime ();
        isCooldown = true;
        gameObject.SetActive(true);
    }

    public void ReduseTime(int damage)
    {
        shieldImage.fillAmount += damage / 10f;
    }
}

