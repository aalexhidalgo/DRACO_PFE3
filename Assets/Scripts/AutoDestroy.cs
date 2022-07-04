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
        StartCoroutine(DestroyPrefab());
    }

    void OnDestroy()
    {
        SpawnManagerScript.PointsOccupied.Remove(SpawnManagerScript.PointsOccupied[0]);
    }

    public IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}
