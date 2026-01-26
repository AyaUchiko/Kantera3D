using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
    public float patrolRange;
    private Vector3 startPos;
    private bool moveRight = true;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float currentX = transform.position.x;

        {
            if(moveRight)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if(currentX>=startPos.x+patrolRange)
                {
                    moveRight = false;
                }
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if(currentX<=startPos.x-patrolRange)
                {
                    moveRight = true;
                }
            }
        }
    }
}
