using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject damPopUp;
    Health health;

    private void Start()
    {
        health = FindObjectOfType<Player>().GetComponent<Health>();
    }

    public void TakeDamEffect(int damage)
    {
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Animator animator = instance.GetComponentInChildren<Animator>();
            if (damage <= 10) animator.Play("normal");
            else animator.Play("critical");
        }
    }
}
