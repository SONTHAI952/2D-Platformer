using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    [SerializeField] private AudioSource deathAudio;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathAudio.Play();
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
    }

    private void RestartLetvel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
