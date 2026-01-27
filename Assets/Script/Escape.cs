using UnityEngine;

public class Escape : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
}
