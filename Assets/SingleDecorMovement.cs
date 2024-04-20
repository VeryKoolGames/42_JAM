using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDecorMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed.value * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DecorTP"))
        {
            Destroy(gameObject);
        }
    }
}
