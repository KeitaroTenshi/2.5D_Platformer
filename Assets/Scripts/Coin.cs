using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ScoreUpdate();
            Destroy(this.gameObject);
        }
    }

}
