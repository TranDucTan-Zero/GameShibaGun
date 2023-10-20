using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8f;

    public Rigidbody2D rb;
    public SpriteRenderer characterSR;
/*    public float dashBoost = 2f;
    
    public float DashTime;*/

    private float sprintTimeRemaining = 0.0f;
    private float originalMoveSpeed;

    Animator animator;
    public Vector3 moveInput;
    public GameObject damPopUp;
    /*public LosePanel losePanel;*/
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
    }
    private void Update()
    {
        if (sprintTimeRemaining > 0)
        {
            // Nếu đang trong thời gian tốc hành, giảm thời gian còn lại
            sprintTimeRemaining -= Time.deltaTime;
            if (sprintTimeRemaining <= 0)
            {
                // Hết thời gian tốc hành, trở về tốc độ ban đầu
                moveSpeed = originalMoveSpeed;
            }
        }
        // Xử lý các logic khác, ví dụ: Input và di chuyển
        // Cập nhật thời gian bất tử còn lại

        /// Part 2
        // Movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        // Áp dụng Dead Zone

        transform.position += moveSpeed * Time.deltaTime * moveInput;

        //
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        /* if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
         {
             Debug.Log("Dashing");   
             animator.SetBool("Roll", true);
             moveSpeed += dashBoost;
             dashTime = DashTime;
             once = true;
         }

         if (dashTime <= 0 && once == true)
         {
             animator.SetBool("Roll", false);
             moveSpeed -= dashBoost;
             once = false;
         }
         else
         {
             dashTime -= Time.deltaTime;
         }*/
        // Rotate Face
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                characterSR.transform.localScale = new Vector3(1, 1, 0);
            else
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
        }

    }
    public void TakeDamageEffect(int damage)
    {
      
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);

            // Bạn có thể loại bỏ việc thay đổi văn bản và animation nếu bạn không cần chúng.
        }
    }
    public void ApplySprintBoost(float sprintBoost, float sprintDuration)
    {
        if (sprintTimeRemaining <= 0)
        {
            // Chỉ áp dụng tốc hành nếu người chơi không còn trong tình trạng tốc hành
            originalMoveSpeed = moveSpeed;
            moveSpeed += sprintBoost;
            sprintTimeRemaining = sprintDuration;
        }
    }

}
