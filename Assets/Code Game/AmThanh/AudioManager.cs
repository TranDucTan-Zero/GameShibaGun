using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    public AudioClip BG;
    public AudioClip Bullet;
    public AudioClip Die;

    private float initialMusicVolume;
    private float initialSFXVolume;
    private bool isAudioMuted; // Biến cờ để kiểm tra trạng thái âm thanh (đã tắt hay chưa)

    private float savedMusicVolume; // Biến để lưu trạng thái âm lượng ban đầu
    private float savedSFXVolume; // Biến để lưu trạng thái âm lượng ban đầu
    private bool shouldRestartAudio = false; // Thêm một cờ
    private void Start()
    {
        music.clip = BG;
        music.Play();

        // Lưu giá trị âm lượng ban đầu
        initialMusicVolume = music.volume;
        initialSFXVolume = SFX.volume;

        // Lưu trạng thái âm lượng ban đầu
        savedMusicVolume = music.volume;
        savedSFXVolume = SFX.volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isAudioMuted) // Kiểm tra xem âm thanh đã bị tắt chưa
        {
            SFX.PlayOneShot(clip);
        }
    }

    public void StopBackgroundMusic()
    {
        music.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
        isAudioMuted = (volume == 0); // Cập nhật trạng thái âm thanh (âm thanh đã tắt hay chưa)
    }

    public void SetSFXVolume(float volume)
    {
        SFX.volume = volume;
        isAudioMuted = (volume == 0); // Cập nhật trạng thái âm thanh (âm thanh đã tắt hay chưa)
    }

    public float GetMusicVolume()
    {
        return music.volume;
    }

    public float GetSFXVolume()
    {
        return SFX.volume;
    }

    public void ResetVolumes()
    {
        // Đặt lại âm lượng âm thanh về giá trị ban đầu
        music.volume = savedMusicVolume;
        SFX.volume = savedSFXVolume;
        isAudioMuted = (savedMusicVolume == 0); // Đặt lại trạng thái âm thanh khi đặt lại âm lượng
    }

    public void RestartAudio()
    {
        if (!shouldRestartAudio)
        { // Kiểm tra cờ trước khi khởi động lại âm thanh
            return;
        }

        StopBackgroundMusic();
        music.volume = savedMusicVolume;
        SFX.volume = savedSFXVolume;
        music.Play();

        shouldRestartAudio = false; // Đặt lại cờ sau khi đã khởi động lại âm thanh
    }
    public void SetShouldRestartAudio(bool shouldRestart)
    {
        shouldRestartAudio = shouldRestart;
    }
}