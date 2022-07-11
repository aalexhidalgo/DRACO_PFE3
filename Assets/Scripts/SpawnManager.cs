using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] Points;
    public GameObject DracoShield;
    public int RemainDracoShields = 2;
    public GameObject BossShield;

    public List<Transform> PointsOccupied = new List<Transform>();

    private PlayerController PlayerControllerScript;
    private BossLogic BossLogicScript;
    private GameManager GameManagerScript;
    private float MinTimeSpawn = 5f;
    private float MaxTimeSpawn = 20f;
    private Transform Pos;
    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        BossLogicScript = FindObjectOfType<BossLogic>();
        GameManagerScript = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnRandomPrefab());
    }

    public IEnumerator SpawnRandomPrefab()
    {
        while (GameManagerScript.GameOver == false && BossLogicScript.Win == false) //mientras no hemos ganado ni perdido, es decir jugamos
        {
            float SpawnRate = Random.Range(MinTimeSpawn, MaxTimeSpawn); //Generamos un spawn  rate aleatorio para los prefabs (escudos) que vamos a instanciar
            yield return new WaitForSeconds(SpawnRate); //esperamos
            int RandomIndex = Random.Range(0, Points.Length); //Creamos un index aleatorio del array de posiciones va a intentar instanciarse el escudo
            Pos = Points[RandomIndex]; //Metemos en una variable tipo transform la posición seleccionada aleatoriamente

            while(PointsOccupied.Contains(Pos)) //si en la posición que ha salido random ya hay un objeto instanciado
            {
                RandomIndex = Random.Range(0, Points.Length); //creamos un nuevo index aleatorio y guardamos la nueva posición del prefab
                Pos = Points[RandomIndex];
            }

            int PrefabSelected = Random.Range(0, 2); //Elegimos random cual de los dos prefabs vamos a instanciar
            if(PrefabSelected == 0 && GameManagerScript.pause == false) //si sale 0, primera opción
            {
                Instantiate(BossShield, Pos.position, BossShield.transform.rotation);//instanciamos un escudo del boss
                PointsOccupied.Add(Pos);
            }

            else //si ha salido 1 instanciaremos un escudo de Draco PERO...
            {
                if (GameManagerScript.pause == false)
                {
                    if (RemainDracoShields > 0)
                    {
                        RemainDracoShields--;
                        Instantiate(DracoShield, Pos.position, DracoShield.transform.rotation);
                        PointsOccupied.Add(Pos);
                    }
                    else //Si ya hemos spawneado anteriormente en la batalla todos los escudos de Draco instanciaremos el del boss en su lugar
                    {
                        Instantiate(BossShield, Pos.position, BossShield.transform.rotation);
                        PointsOccupied.Add(Pos);
                    }
                }                
            }
        }
    }
}
