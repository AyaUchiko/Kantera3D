using UnityEngine;

public class LightCharge : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("”ÍˆÍ“à");
            PlayerController player = other.GetComponent<PlayerController>();
            GaugeController gauge = FindFirstObjectByType<GaugeController>();

            if (player != null && player.ChargeAction && gauge != null)
            {
                gauge.FullRecovery();
                Debug.Log("‘S‰ñ•œ");
                gameObject.SetActive(false);
            }
        }
    }
}
