using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;
    public GameObject door;

    [Header ("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;
    public AudioSource ScoutAppearance1, ScoutAppearance2, ScoutAppearance3;

    [Header("Bosses")]
    public GameObject[] bosses;
    public Transform bossSpawner;
    public AudioSource HeavyAppearance1, HeavyAppearance2, HeavyAppearance3;

    [Header ("Powerups")]
    public GameObject shield;
    public GameObject healthPotion;

    [HideInInspector] public List<GameObject> enemies;

    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed;

    [HideInInspector] public bool isBossRoom;

    private void Awake()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();

    }

    private void Start()
    {
        variants.rooms.Add(gameObject);
        isBossRoom = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !spawned)
        {
            spawned = true;
            if (!isBossRoom)
            {
                foreach (Transform spawner in enemySpawners)
                {
                    int rand = Random.Range(0, 11);
                    if (rand < 9)
                    {
                        int music = Random.Range(1, 3);
                        switch (music)
                        {
                            case 1:
                                ScoutAppearance1.Play();
                                break;
                            case 2:
                                ScoutAppearance2.Play();
                                break;
                            case 3:
                                ScoutAppearance3.Play();
                                break;
                        }
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                        enemy.transform.parent = transform;
                        enemies.Add(enemy);
                    }
                    else if (rand == 9)
                    {
                        Instantiate(healthPotion, spawner.position, Quaternion.identity);
                    }
                    else if (rand == 10)
                    {
                        Instantiate(shield, spawner.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                int music = Random.Range(1, 3);
                switch (music)
                {
                    case 1:
                        HeavyAppearance1.Play();
                        break;
                    case 2:
                        HeavyAppearance2.Play();
                        break;
                    case 3:
                        HeavyAppearance3.Play();
                        break;
                }
                GameObject bossType = bosses[Random.Range(0, bosses.Length)];
                GameObject boss = Instantiate(bossType, bossSpawner.position, Quaternion.identity) as GameObject;
                boss.transform.parent = transform;
                enemies.Add(boss);
                
            }
            StartCoroutine(CheckEnemies());
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil (() => enemies.Count == 0);
        DestroyWalls();
    }

    public void DestroyWalls()
    {
        foreach(GameObject wall in walls)
        {
            if(wall != null && wall.transform.childCount != 0)
            {
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallsDestroyed && other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
        }
    }

}
