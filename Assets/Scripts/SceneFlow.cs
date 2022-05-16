using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            DataPersistance.DracoState.SaveCoins(otherCollider.gameObject.GetComponent<PlayerController>().MoneyCounter);
            SceneManager.LoadScene("Store");
        }
    }
}
