using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Linq;

public class TitleManager : MonoBehaviour
{
    private bool isStarting = false;
    void Update()
    {
        if (isStarting) return;

        if (Keyboard.current.anyKey.wasPressedThisFrame ||(Gamepad.current != null && Gamepad.current.allControls.Any(c => c is UnityEngine.InputSystem.Controls.ButtonControl button && button.isPressed)))
        {
            StartCoroutine(GoToNextScene());
        }
    }

    IEnumerator GoToNextScene()
    {
        isStarting = true;
        if (FadeManager.Instance != null)
        {
            yield return StartCoroutine(FadeManager.Instance.FadeOut());
        }
        SceneManager.LoadScene("Stage1");
    }
}