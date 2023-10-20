using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject muzzle;

    AudioManager audioManager;

    public float TimeBtwFire = 0.2f;
    public float bulletForce;

    private float timeBtwFire;
    private bool autoFire = false;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AmThanh").GetComponent<AudioManager>();
    }
    void Update()
    {
        RotateGunTowardsMouse();
        timeBtwFire -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeBtwFire < 0)
        {
            FireBullet();
        }
        if (Input.GetMouseButtonDown(1))
        {
            // Khi nút chuột phải được nhấn, chuyển trạng thái bắn tự động (on/off)
            autoFire = !autoFire;
        }

        if (autoFire && timeBtwFire < 0)
        {
            FireBullet();
        }
    }

    void RotateGunTowardsMouse()
    {
        // Lấy vị trí chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Tính vector hướng từ súng tới vị trí chuột
        Vector2 lookDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        // Tính góc quay dựa trên vector hướng
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Cài đặt góc quay cho súng
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Kiểm tra và cài đặt việc lật ngược súng (nếu cần)
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(1, -1, 0);
        else
            transform.localScale = new Vector3(1, 1, 0);
    }

    void FireBullet()
    {
        
        /* foreach (Transform fire in firePos)
         {*/
        timeBtwFire = TimeBtwFire;

        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);

        // Effect
        Instantiate(muzzle, firePos.position, transform.rotation, transform);


        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        audioManager.PlaySFX(audioManager.Bullet);
    }
    /*}*/
}
