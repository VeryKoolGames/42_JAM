using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class EnemyMovement : MonoBehaviour
    {
        public Vector2 targetPosition;
        public bool canMove;

        private void Update()
        {
            if (!canMove)
                return;
            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
            }
        }
    }
}