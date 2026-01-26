using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject cage;
    public float upSpeed;
    Rigidbody cageRb;
    private bool check = false;
    public float maxHeight = 1f;
    void Start()
    {
        cageRb = cage.GetComponent<Rigidbody>();
        cageRb.isKinematic = false;
    }

    void FixedUpdate()
    {
        if(check)
        {
            if (cage.transform.position.y < maxHeight)
            {
                cageRb.isKinematic = true;
                cageRb.MovePosition(cage.transform.position + Vector3.up * upSpeed * Time.fixedDeltaTime);
            }
            else
            {
                cageRb.isKinematic = true;
                cageRb.linearVelocity = Vector3.zero;

                Vector3 pos = cage.transform.position;
                pos.y = maxHeight;
                cage.transform.position = pos;
            }
        }
        else
        {
            cageRb.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        check = true;
    }
    private void OnTriggerExit(Collider other)
    {
        check = false;
    }
}
