using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private CreditsPlayer CreditsPlayerScript;
    private float speed = 10f;

    void Start()
    {
        CreditsPlayerScript = FindObjectOfType<CreditsPlayer>();
    }
    void Update()
    {
        if(CreditsPlayerScript.florIsMoving == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
