using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public HealthBar healthBar;
    public GameOver gameOver; // Kết nối GameOver script trong Inspector
    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;

    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AmThanh").GetComponent<AudioManager>();
    }
    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.CompareTag("Player"))
                {
                    // Ngừng phát âm thanh nền (BG) bằng AudioManager
                    audioManager.StopBackgroundMusic();

                    // Phát âm thanh "Die" nếu nhân vật là Player
                    audioManager.PlaySFX(audioManager.Die);

                    // Chỉ hiển thị bảng Game Over nếu nhân vật này là Player
                    gameOver.ShowGameOver(FindObjectOfType<Killed>().currentKilled);
                }
                if (this.gameObject.tag == "Enemy")
                {
                    /* FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);*/
                    FindObjectOfType<Killed>().UpdateKilled();
                   /* FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));*/
                    Destroy(this.gameObject, 0.125f);
                }
                isDead = true;
                

            }
            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }
    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }
    // VẬt phầm hồi máu
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Cập nhật thanh máu (nếu có)
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }
    }

}