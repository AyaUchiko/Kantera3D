using UnityEngine;

public class WolfAreaTrigger : MonoBehaviour
{
    public bool attackArea;
    private EnemyWolf parentWolf;

    void Start()
    {
        parentWolf = GetComponentInParent<EnemyWolf>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

            if (attackArea)
            {
                parentWolf.OnEnterAttackArea(player);
            }
            else
            {
                parentWolf.OnEnterDetectionArea();
            }
        }
    }
}