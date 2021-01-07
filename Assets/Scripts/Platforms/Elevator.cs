using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _posA, _posB;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _atPointA;
    [SerializeField] private float _elevatorWaitingRate = 5.0f;
    private Elevator _elevatorParent;
    void Start()
    {
        StartCoroutine(SwitchingRoutine());
        _elevatorParent = GetComponentInParent<Elevator>();

        if (_elevatorParent == null)
        {
            Debug.LogError("Elevator parent is null");
        }
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            ElevatorMovement();
        }
    }

    private void ElevatorMovement()
    {
        if (!_atPointA)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _posA.position, Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _posB.position, Time.deltaTime);
        }

        if (transform.position == _posA.position)
        {
            _canMove = false;
            _atPointA = true;
        }
        if (transform.position == _posB.position)
        {
            _canMove = false;
            _atPointA = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = _elevatorParent.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    IEnumerator SwitchingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (_canMove == false)
            {
                yield return new WaitForSeconds(_elevatorWaitingRate);
                _canMove = true;
            }
        }
    }
}
