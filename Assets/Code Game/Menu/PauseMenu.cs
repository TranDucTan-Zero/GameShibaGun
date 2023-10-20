using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private AudioManager audioManager;  // Tạo tham chiếu đến AudioManager

    private void Start()
    {
        // Tìm AudioManager trong scene và lưu tham chiếu vào audioManager
        audioManager = FindObjectOfType<AudioManager>();

        // Đặt giá trị ban đầu của Slider dựa vào âm lượng âm thanh
        musicSlider.value = audioManager.GetMusicVolume();
        sfxSlider.value = audioManager.GetSFXVolume();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        audioManager.SetShouldRestartAudio(true); // Đặt cờ để cho phép khởi động lại âm thanh
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void exit()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioManager.SetSFXVolume(volume);
    }
}
