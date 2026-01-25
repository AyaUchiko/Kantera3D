using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
    public float patrolRange;
    private Vector3 startPos;
    private bool movingRight = true;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float currentX = transform.position.x;

        {
            if(movingRight)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if(currentX>=startPos.x+patrolRange)
                {
                    movingRight = false;
                }
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if(currentX<=startPos.x-patrolRange)
                {
                    movingRight = true;
                }
            }
        }
    }
}
