using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp = 20;
    [SerializeField] private float vel = 2f;
    [SerializeField] private int points = 50;

    private Rigidbody rb;
    private Transform targetplayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        targetplayer = player.transform;
    }

    void FixedUpdate()
    {
        Vector3 direction = targetplayer.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        rb.linearVelocity = new Vector3(
            direction.x * vel,
            rb.linearVelocity.y,
            direction.z * vel
        );
    }
    public void TakeDamage(int damage)
    {
        hp = hp - damage;

        Debug.Log("Vida del enemigo: " + hp);

        if (hp <= 0)
        {
            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            scoreManager.AddPoints(points);

            Clear_Zone zone =
                FindFirstObjectByType<Clear_Zone>();

            zone.EnemyDefeated();

            Destroy(gameObject);
        }
    }
}