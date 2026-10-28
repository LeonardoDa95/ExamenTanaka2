using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp = 20;
    [SerializeField] private int vel = 5;
    [SerializeField] private int points = 50;

    Rigidbody rb;
    private Transform targetplayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetplayer = GetComponent<Transform>();
    }

    private void Update()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = targetplayer.position;
    }
}
