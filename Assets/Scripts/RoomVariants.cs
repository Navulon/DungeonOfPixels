using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] bossRooms;
    
    private bool check;

    public GameObject key;
    public GameObject gun;

    [HideInInspector] public List<GameObject> rooms;

    private void Start()
    {
        StartCoroutine(RandomSpawner());
        check = false;
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        int numBoss = 0;
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        for(int i = rooms.Count - 1; i > 0; i--)
        {
            for(int j = 0; j <= bossRooms.Length - 1; j++)
            {
                if (rooms[i].name == bossRooms[j].name)
                { 
                    lastRoom = rooms[i].GetComponent<AddRoom>();
                    check = true;
                    numBoss = i;
                    break;
                    
                }
            }
            if (check)
                break;
        }

        int rand;
        do
        {
            rand = Random.Range(0, rooms.Count - 2);
        }
        while (rand == numBoss);

        Instantiate(key, rooms[rand].transform.position, Quaternion.identity);
        do
        {
            rand = Random.Range(0, rooms.Count - 2);
        }
        while (rand == numBoss);
        Instantiate(gun, rooms[rand].transform.position, Quaternion.identity);

        lastRoom.door.SetActive(true);
        lastRoom.isBossRoom = true;
    }
}
