/*

name: Anthony Truong

course: CST306

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGen : MonoBehaviour
{
    public GameObject track;
    public GameObject[] tracks;

    private Vector3 position;

    private Transform playerPos;

    private float zAxis = 0;
    private float tileLength = 10;

    private int nbTile = 5;
    private int count = 0;

    void Start()
    {
        position = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < nbTile; i++)
        {
            spawnTile();
        }
    }

    void Update()
    {
        if (playerPos.position.z > (zAxis - nbTile * tileLength))
        {
            spawnTile();
            ++count;
        }
    }

    private void spawnTile()
    {
        if (GameManager.getDif() <= 1)
        {
            if (count == 4)
            {
                GameObject obstacl;
                int rnd = Random.Range(0, tracks.Length);
                obstacl = Instantiate(tracks[rnd]) as GameObject;
                obstacl.transform.position = Vector3.forward * zAxis;
                count = 0;
            }
            else
            {
                GameObject tile;
                tile = Instantiate(track) as GameObject;
                tile.transform.position = Vector3.forward * zAxis;
                zAxis += tileLength;
            }
        }
        else
        {
            if (GameManager.getDif() > 1)
            {
                if (count == 3)
                {
                    GameObject obstacl;
                    int rnd = Random.Range(0, tracks.Length);
                    obstacl = Instantiate(tracks[rnd]) as GameObject;
                    obstacl.transform.position = Vector3.forward * zAxis;
                    count = 0;
                }
                else
                {
                    GameObject tile;
                    tile = Instantiate(track) as GameObject;
                    tile.transform.position = Vector3.forward * zAxis;
                    zAxis += tileLength;
                }
            }
        }
    }
}
