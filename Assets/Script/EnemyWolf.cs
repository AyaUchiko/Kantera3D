using UnityEngine;

public class EnemyWolf : MonoBehaviour
{
    [Header("•\¦İ’è")]
    public GameObject eyes;
    public GameObject attack;

    [Header("Œ‚‘Şİ’è")]
    public float needIntensity = 30f;
    public float needRange = 4f;
    void Start()
    {
        if(eyes!=null)
        {
            attack.SetActive(false);
        }
        if(attack !=null)
        {
            attack.SetActive(false);
        }
    }

    public void OnEnterDetectionArea()
    {
        if (eyes != null) eyes.SetActive(true);
        Debug.Log("˜T‚ª‚±‚¿‚ç‚ğŒ©‚Ä‚¢‚é...");
    }

    public void OnEnterAttackArea(PlayerController player)
    {
        LightController lightCtrl = FindFirstObjectByType<LightController>();

        if (lightCtrl != null &&
            lightCtrl.intensity >= needIntensity &&
            lightCtrl.range >= needRange)
        {
            Destroy(gameObject);
        }
        else
        { 
            if (attack != null) attack.SetActive(true);
            player.StartCoroutine(player.DeadProcess());
        }
    }
}
