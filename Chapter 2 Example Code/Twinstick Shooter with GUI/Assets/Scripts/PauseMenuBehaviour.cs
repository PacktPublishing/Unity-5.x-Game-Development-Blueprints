using UnityEngine;

public class PauseMenuBehaviour : MainMenuBehaviour
{
    public static bool isPaused;

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    public void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            if (!optionsMenu.activeInHierarchy)
            {
                // If false becomes true and vice-versa
                isPaused = !isPaused;

                // If isPaused is true, 0 otherwise 1
                Time.timeScale = (isPaused) ? 0 : 1;

                pauseMenu.SetActive(isPaused);
            }
            else
            {
                OpenPauseMenu();
            }
            
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }


    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeLabel();
    }

    private void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[currentQuality];

        optionsMenu.transform.FindChild("Quality Level").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " +
                                                                                                    qualityName;
    }

    private void UpdateVolumeLabel()
    {
        optionsMenu.transform.FindChild("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " +
                                                                                                    (AudioListener.volume * 100).ToString("f2") + "%";
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
