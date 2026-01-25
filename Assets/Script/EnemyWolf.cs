using UnityEngine;

public class EnemyWolf : MonoBehaviour
{
    public GameObject attack;
    void Start()
    {
        if(attack !=null)
        {
            attack.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    void Attack()
    {
        if(attack!=null)
        {
            attack.SetActive(true);
        }
    }
}
