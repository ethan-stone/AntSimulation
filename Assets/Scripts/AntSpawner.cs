using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public Ant antPrefab;
    public GameObject colonyPrefab;
    public int numAnts;
    
    Vector2 screenHalfSizeInWorldUnits;

    void Start() {
        screenHalfSizeInWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        Vector2 colonyPosition = new Vector2(Random.Range(-screenHalfSizeInWorldUnits.x, screenHalfSizeInWorldUnits.x), Random.Range(-screenHalfSizeInWorldUnits.y, screenHalfSizeInWorldUnits.y));
        for (int i = 0; i < numAnts; i++) {
            float radius = 3f;
            float spawnAngle = Random.Range(-180, 180);

            Vector2 antPosition = new Vector2(colonyPosition.x + Random.Range(-radius, radius), colonyPosition.y + Random.Range(-radius, radius));
            Ant newAnt = Instantiate(antPrefab);
            newAnt.position = antPosition;
        }
        Instantiate(colonyPrefab, colonyPosition, Quaternion.Euler(Vector3.forward * 0));
    }
}
