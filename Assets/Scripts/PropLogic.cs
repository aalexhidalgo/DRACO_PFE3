using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLogic : MonoBehaviour
{
    public float SpinSpeed = 5f;

    //private GameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        //GameManagerScript = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
    }
}
