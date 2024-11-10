using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    public GameObject missionObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> missionObjects = new();
        int numObjects = 3;
        for (int i = 0; i < numObjects; i++) {
            Vector3 spawnPos = new(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            GameObject missionObject = Instantiate(missionObjectPrefab, spawnPos, Quaternion.identity);
            missionObjects.Add(missionObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
