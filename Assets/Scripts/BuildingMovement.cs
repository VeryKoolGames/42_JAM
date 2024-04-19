using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;

    void Update()
    {
        transform.position += Vector3.left * speed.value * Time.deltaTime;
    }
}
