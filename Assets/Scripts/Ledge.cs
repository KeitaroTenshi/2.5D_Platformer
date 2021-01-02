using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] private Vector3 _handPos, _standPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            Player player = other.GetComponentInParent<Player>();

            if (player != null)
            {
                player.LedgeGrabbed(_handPos, this);
            }
        }
    }
    public Vector3 GetStandPos()
    {
        return _standPos;
    }
}
