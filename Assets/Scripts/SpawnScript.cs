using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    [SerializeField] private GameObject Kunai;

    [SerializeField] private float startTime;

    [SerializeField] private float spawnTime;

    [SerializeField] private float y1;
    [SerializeField] private float y2;

    private Vector2 spawnPoint;

    void Start()
    {
        InvokeRepeating("fire", startTime, spawnTime);
    }

    void fire()
    {
        var randomY = Random.Range(0,9);
        if(randomY >4 )
        {
            spawnPoint = new Vector2(transform.position.x, y1);
        }
        else
        {
            spawnPoint = new Vector2(transform.position.x, y2);
        }

        Debug.Log(randomY);

        Instantiate(Kunai, spawnPoint, Quaternion.identity);
    }

}
