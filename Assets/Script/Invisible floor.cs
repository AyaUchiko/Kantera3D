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
        LightController lightCtrl = FindFirstObjectByType<LightController>();

        if (lightCtrl != null)
        {
            bool isVisible = lightCtrl.intensity >= needIntensity;

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
