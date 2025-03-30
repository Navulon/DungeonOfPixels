using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public GunType gunType;
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public AudioSource shotAudio;
    public enum GunType {Default, Enemy}

    private float rotZ;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private Player player;
    private Vector3 difference;
    private bool isShot = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (gunType == GunType.Default)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if (gunType == GunType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }



        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);


        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) || gunType == GunType.Enemy)
            {
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (gameObject.name != "Sasha")
        {
            shotAudio.Play();
        }
        else
        {
            if (!isShot)
            {
                shotAudio.Play();
                isShot = true;
            }
        }
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        timeBtwShots = startTimeBtwShots;
    }

}
