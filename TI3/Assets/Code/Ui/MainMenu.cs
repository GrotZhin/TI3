using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    float sounds = 1.0f;
    float music = 1.0f;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    void Start()
    {
        sounds = PlayerPrefs.GetFloat("Sounds", 1.0f);
        music = PlayerPrefs.GetFloat("Music", 1.0f);
        soundSlider.value = sounds;
        musicSlider.value = music;
    }
    public void PlayGame(string sceneName)
    {
        // Load the specified game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
    public void SavePreferences()
    {
        // Save player preferences
        PlayerPrefs.SetFloat("Sounds", sounds);
        PlayerPrefs.SetFloat("Music", music);
    }
    public void SetSoundVolume()
    {
        sounds = soundSlider.value;
        // Update sound volume in the game
        AudioListener.volume = sounds;
    }
    public void SetMusicVolume()
    {
        music = musicSlider.value;
        // Update music volume in the game
        AudioListener.volume = music;
    }
}
