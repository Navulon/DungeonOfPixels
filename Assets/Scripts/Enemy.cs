using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
   // public GameObject floatingDamage;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public int Damage;

    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    public AudioSource shotAudio;

    private Player player;
    private Animator anim;
    private AddRoom room;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
        room = GetComponentInParent<AddRoom>();
    }

    private void Update()
    {
        
        if(stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if(health <= 0)
        {
            EnemyDeath();
        }
        if(player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    }
    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
        
    }

    public void EnemyDeath()
    {
        if (gameObject.name == "BossHeavy(Clone)")
            {
                int rand = Random.Range(0, 3);
                if (rand <= 1)
                {
                    player.killHeavy1.Play();
                    SceneManager.LoadScene("Menu");
                }
                else if (rand >= 2)
                {
                    player.killHeavy2.Play();
                    SceneManager.LoadScene("Menu");
                }

                SceneManager.LoadScene("Menu");
            }
            else
            {
                    int rand = Random.Range(1, 16);
                    switch (rand)
                    {
                        case 1:
                            player.killRobot1.Play();
                            break;
                        case 2:
                            player.killRobot2.Play();
                            break;
                        case 3:
                            player.killRobot3.Play();
                            break;
                        case 4:
                            player.killRobot4.Play();
                            break;
                    }
                
                Destroy(gameObject);
                room.enemies.Remove(gameObject);
            }
    }

   

    

    

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(timeBtwAttack <= 0)
            {
                anim.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void enemyMeleeAttack()
    {
        player.ChangeHealth(Damage * -1);
        timeBtwAttack = startTimeBtwAttack;
    }
}
