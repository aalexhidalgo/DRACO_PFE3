using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float Timer = 8f;
    private SpawnManager SpawnManagerScript;
    private GameManager GameManagerScript;

    void Start()
    {
        SpawnManagerScript = FindObjectOfType<SpawnManager>();
        GameManagerScript = FindObjectOfType<GameManager>();
        Destroy(gameObject, Timer);
    }

    void OnDestroy()
    {
        SpawnManagerScript.PointsOccupied.Remove(SpawnManagerScript.PointsOccupied[0]);
    }
}
