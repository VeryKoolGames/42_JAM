using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool inputEnabled = true;
    public static InputManager Instance;
    private void Awake()
    {
        Instance = this;
    }
}