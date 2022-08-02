using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    //El temporizador y el SpawnManager
    #region variables 
    private float Timer = 8f;

    private SpawnManager SpawnManagerScript;
    #endregion  
    void Start() //encontramos Script spawnManager y empezamos la cuenta atrás para destruir el objeto
    {
        SpawnManagerScript = FindObjectOfType<SpawnManager>();
        StartCoroutine(DestroyPrefab());
    }

    void OnDestroy()
    {
        SpawnManagerScript.PointsOccupied.Remove(SpawnManagerScript.PointsOccupied[0]);
    } //cuando se destruya indicamos al spawnManager que su posicion ya no está ocupada

    public IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    } //esperamos el tiempo del timer y destruimos el objeto
}
