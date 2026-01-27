using UnityEngine;
using UnityEngine.SceneManagement;  

public class TitleButton : MonoBehaviour
{
    public void TitleSceneButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
