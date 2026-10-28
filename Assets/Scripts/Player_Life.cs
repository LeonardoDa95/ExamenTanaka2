using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    [SerializeField] private int hp = 50;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            hp = hp -5;
            Debug.Log("-5 de vida maldito");
            if (hp <= 0)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
    
}
