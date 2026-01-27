using UnityEngine;

public class Goal: MonoBehaviour
{
    public GameObject ResetText;
    public GameObject KanteraGauge;
    public GameObject restartButton;
    public GameObject titleButton;
    public GameObject fire;

    Animator animator;
    void Start()
    {
        restartButton.SetActive(false);
        titleButton.SetActive(false);
        fire.SetActive(false);

        animator = fire.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fire.SetActive(true);
            animator.Play("Fire");
            Destroy(KanteraGauge);
            Destroy(ResetText);
            Invoke("ButtonOn", 1f);
        }
    }

    private void ButtonOn()
    {
        restartButton.SetActive(true);
        titleButton.SetActive(true);
    }
}
