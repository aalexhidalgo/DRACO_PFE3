using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float speed = 30;

    //Puntos a seguir
    [SerializeField] private Transform[] points;
    private int totalPoints;
    private int nextPoint;

    public GameObject Fireball;
    public Transform PlayerTransform;
    public bool canMove;

    void Awake()
    {

    }
    void Start()
    {
        transform.position = points[0].position;
        totalPoints = points.Length;
        nextPoint = 1;
        canMove = true;

        //transform.LookAt(points[nextPoint].position);

        //Colisi�n con pared que nos permita hacer los puntos de ruta (if blablabla)
        //StartCoroutine(BossAttack());
    }

    void Update()
    {
        if(canMove)
        {
            if (Vector3.Distance(transform.position, points[nextPoint].position) < 0.1f)
            {
                canMove = false;

                nextPoint++;

                if (nextPoint == totalPoints)
                {
                    nextPoint = 0;
                }

                StartCoroutine(BossAttack());
                //transform.LookAt(points[nextPoint].position);
            }

            transform.position = Vector3.MoveTowards(transform.position, points[nextPoint].position, speed * Time.deltaTime);

            
        }

        transform.GetChild(0).transform.LookAt(PlayerTransform);
    }

    public IEnumerator BossAttack()
    {
        float Timer = 0.5f;
        int SpawnRate = Random.Range(0, 4);
        yield return new WaitForSeconds(Timer);

        for (int i = 0; i <= SpawnRate; i++)
        {
            Instantiate(Fireball, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
            yield return new WaitForSeconds(Timer);
        }
        canMove = true;
    }
}