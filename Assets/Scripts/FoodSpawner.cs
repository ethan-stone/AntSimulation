using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int numFood;
    public float spawnSize;

    Vector2 screenHalfSizeInWorldUnits;

    void Start() {
        screenHalfSizeInWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        for (int i = 0; i < numFood; i++) {
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeInWorldUnits.x, screenHalfSizeInWorldUnits.x), Random.Range(-screenHalfSizeInWorldUnits.y, screenHalfSizeInWorldUnits.y));
            GameObject newFood = (GameObject) Instantiate(foodPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * 0));
            newFood.transform.localScale = Vector2.one * spawnSize;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
