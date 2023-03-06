using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;
    private bool isActive;
    private bool throwKnife;
    [SerializeField] private float speed = 10.0f;

    [SerializeField] private AudioSource knifeToLog;
    [SerializeField] private AudioSource knifeToKnife;
    [SerializeField] private AudioSource knifeThrowing;

    private void Start()
    {
        isActive = true;
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.activeKnives > 0)
        {
            knifeThrowing.Play();
            throwKnife = true;
        }
    }

    private void FixedUpdate()
    {
        if (throwKnife && isActive) // if player taps a screen and this knife isn't thrown
        {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);   // since we disable angular drag, we just make move it up
            rb.gravityScale = 1;
            isActive = false;
        }            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        if (collision.gameObject.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();
            knifeToLog.Play();
            // Freeze the position of Knife to the position of Log
            FreezingPosition(collision);
            // After collision we changing size of collider 
            ChangingKnifeColliderSize();

            // TODO: call gameManager to spawn another knife
            GameManager.instance.KnifeHitted();

            // if are avaiblable knives then we can thwo knife
            if (GameManager.instance.activeKnives > 0)
                isActive = true;
        }
        else if (collision.gameObject.tag == "Knife")
        {
            knifeToKnife.Play();
            // after collision with other knife, this knife will have reaction force
            rb.AddForce(new Vector2(Random.Range(1, -1), -1), ForceMode2D.Impulse);
            // TODO: call gameManager to game over
            GameManager.instance.GameOver();
        }
    }

    // Freezing the position of knife to log
    private void FreezingPosition(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        this.transform.SetParent(collision.collider.transform);
    }

    // Channging size and offset of knife boxCollider2D
    private void ChangingKnifeColliderSize()
    {
        knifeCollider.offset = new Vector2(0f, -0.3f);
        knifeCollider.size = new Vector2(0.32f, 1.5f);
    }
}
