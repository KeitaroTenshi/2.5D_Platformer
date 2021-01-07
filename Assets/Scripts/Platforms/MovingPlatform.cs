using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _platformMovementSpeed = 2.0f;
    [SerializeField] private Transform _pointA, _pointB;
    private bool _atPointA = true;
    private void FixedUpdate()
    {
        if (_atPointA)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _pointB.transform.position, (_platformMovementSpeed * Time.deltaTime));
        }
        else if (!_atPointA)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _pointA.transform.position, (_platformMovementSpeed * Time.deltaTime));
        }

        if (transform.position == _pointA.transform.position)
        {
            _atPointA = true;
        }
        if (transform.position == _pointB.transform.position)
        {
            _atPointA = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
