using UnityEngine;

public class PushBox : MonoBehaviour
{
    Rigidbody boxRb;
    bool isPush;

    void Start()
    {
        boxRb = GetComponent<Rigidbody>();
    }

    public void SetPush(bool push)
    {
        isPush = push;
    }

    public void MoveBox(float inputX, float speed)
    {
        if (!isPush) return;

        if (Mathf.Abs(inputX) < 0.1f) return;

        boxRb.linearVelocity = new Vector3(
            inputX * speed,boxRb.linearVelocity.y,0f);
    }
}
