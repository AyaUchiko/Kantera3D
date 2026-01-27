using UnityEngine;

public class Invisiblefloor: MonoBehaviour
{
    public float needIntensity = 35f;

    private MeshRenderer meshRenderer;
    private Collider floorCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        floorCollider = GetComponent<Collider>();

        if (meshRenderer != null) meshRenderer.enabled = false;
    }

    void Update()
    {
        LightController lightCtr = FindFirstObjectByType<LightController>();

        if (lightCtr != null)
        {
            bool isVisible = lightCtr.intensity >= needIntensity;

            if (meshRenderer != null)
            {
                meshRenderer.enabled = isVisible;
            }
            if (floorCollider != null)
            {
                floorCollider.enabled = isVisible;
            }
        }
    }
}
