using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab; // Reference to the apple prefab
    public int gridSizeX = 10; // Number of grid cells along the X-axis
    public int gridSizeY = 10; // Number of grid cells along the Y-axis

    private void Start()
    {
        //InvokeRepeating("SpawnApple", 2f, 4f);
        SpawnApple();
    }

    public void SpawnApple()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(0, gridSizeX), Random.Range(0, gridSizeY), 0);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity);
    }
}
