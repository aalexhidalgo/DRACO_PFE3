using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float Timer = 8f;
    private SpawnManager SpawnManagerScript;

    void Start()
    {
        SpawnManagerScript = FindObjectOfType<SpawnManager>();
        Destroy(gameObject, Timer);
    }

    void OnDestroy()
    {
        SpawnManagerScript.PointsOccupied.Remove(gameObject.transform);
    }

}
