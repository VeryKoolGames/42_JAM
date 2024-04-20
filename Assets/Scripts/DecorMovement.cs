using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;
    [SerializeField] private Transform TPPos;
    // Start is called before the first frame update

    void Update()
    {
        transform.position += Vector3.left * speed.value * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DecorTP"))
        {
            transform.position = TPPos.position;
        }
    }
}
