using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour {

    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
