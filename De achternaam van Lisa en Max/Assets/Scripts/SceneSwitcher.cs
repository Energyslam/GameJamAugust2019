using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public void GoToGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
