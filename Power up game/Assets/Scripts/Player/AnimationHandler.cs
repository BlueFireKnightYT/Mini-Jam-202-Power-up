using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityX > 0.2)
        {
            anim.SetBool("Moving", true);
            sr.flipX = false;
        }
        else if (rb.linearVelocityX < -0.2)
        {
            anim.SetBool("Moving", true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool("Moving", false);
            sr.flipX = false;
        }
    }
}
