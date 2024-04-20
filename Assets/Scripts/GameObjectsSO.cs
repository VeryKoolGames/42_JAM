using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectsSO", menuName = "ScriptableObjects/Event/GameObjectsSO", order = 1)]
public class GameObjectsSO : ScriptableObject
{
    public List<GameObject> Objects;
}
