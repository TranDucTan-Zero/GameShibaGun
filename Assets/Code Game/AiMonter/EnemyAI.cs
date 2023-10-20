using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 1.5f;
    public SpriteRenderer characterSR;
    public int minDamage;
    public int maxDamage;

    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float fireRate = 1.5f;  // 5s giữa các lần bắn
    public float maxBulletRange = 10f; // Khoảng cách tối đa cho phép bắn đạn


    private float nextFireTime;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;
    private bool isCalculatingPath = false;

    public float freezeDurationTime;
    private float freezeDuration;
    private Coroutine moveCoroutine;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent <Rigidbody2D>();
        freezeDuration = 0;
        target = FindObjectOfType<Player>().transform;

        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
        nextFireTime = Time.time + fireRate; // Bắt đầu bắn ngay sau khi khởi động
    }

    void CalculatePath()
    {
        if (!isCalculatingPath)
        {
            isCalculatingPath = true;
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathCompleted);
        }
    }

    void OnPathCompleted(Path p)
    {
        isCalculatingPath = false;
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    public void FreezeEnemy()
    {
        freezeDuration = freezeDurationTime;
    }

    IEnumerator MoveToTargetCoroutine()
    {
        while (currentWaypoint < path.vectorPath.Count)
        {
            while (freezeDuration > 0)
            {
                freezeDuration -= Time.deltaTime;
                yield return null;
            }

            Vector2 targetPosition = (Vector2)path.vectorPath[currentWaypoint];
            Vector2 direction = (targetPosition - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, targetPosition);
            if (distance < nextWayPointDistance)
                currentWaypoint++;

            if (force.x != 0)
            {
                if (force.x < 0)
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                else
                    characterSR.transform.localScale = new Vector3(1, 1, 0);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy hit Player");
            int damage = Random.Range(minDamage, maxDamage);
            Health playerHealth = collision.GetComponent<Health>();
            playerHealth.TakeDam(damage);
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer < maxBulletRange)  // Thay maxBulletRange bằng khoảng cách tối đa cho phép để quái vật bắn
            {
                ShootBullet();
            }

            nextFireTime = Time.time + fireRate; // Đặt thời gian bắn tiếp theo
        }
    }


    void ShootBullet()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = (target.position - transform.position).normalized;
            bulletRB.velocity = shootDirection * bulletSpeed;
        }
    }
}
