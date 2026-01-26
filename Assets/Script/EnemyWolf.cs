using UnityEngine;

public class EnemyWolf : MonoBehaviour
{
    public GameObject eyes;
    public GameObject attack;

    public float needIntensity = 30f;
    public float needRange = 4f;
    void Start()
    {
        if(eyes!=null)
        {
            eyes.SetActive(false);
        }
        if(attack !=null)
        {
            attack.SetActive(false);
        }
    }

    public void OnEnterDetectionArea()
    {
        if (eyes != null) eyes.SetActive(true);
    }

    public void OnEnterAttackArea(PlayerController player)
    {
        LightController lightCtrl = FindFirstObjectByType<LightController>();

        if (lightCtrl != null &&lightCtrl.intensity >= needIntensity &&lightCtrl.range >= needRange)
        {
            Destroy(gameObject);
            return;
        }
        else
        { 
            if (attack != null) attack.SetActive(true);
            player.StartCoroutine(player.DeadProcess());
        }
    }
}
