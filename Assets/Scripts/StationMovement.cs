using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;
    [SerializeField] private Transform TPPos;
    public bool shouldMove;

    void Update()
    {
        if (shouldMove)
            transform.position += Vector3.left * speed.value * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DecorTP"))
        {
            transform.position = TPPos.position;
            shouldMove = false;
        }
    }
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("DecorTP"))
    //     {
    //         shouldMove = false;
    //     }
    // }
}
