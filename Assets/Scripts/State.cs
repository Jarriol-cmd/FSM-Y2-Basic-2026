
//This is the base class 
// It defines the common methods and fields that all other states inherit
// You can include methods that you want to allow other states to use here

using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class State
{
    protected PlayerScript player;
    protected StateMachine sm;

    public float verticalInput;
    public float horizontalInput;

    public bool isGrounded;
    public bool isFacingRight = true;

    public float speed;

    // base constructor
    public State(PlayerScript player, StateMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    //methods that can be overriden by each state
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
    public virtual void OnCollisionEnter2D(Collision2D collision) { }
    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerStay2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }

    //Common Shared Methods
    //Put methods that you wish to share between other states here
    //Set them to be public
    public void TestMethod(string text)
    {
        Debug.Log(text);
    }


    public void ReadInput()
    {
    }

    public void GroundCheck()
    {

        Vector2 position = player.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        Debug.DrawRay(position, direction, Color.black);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, player.groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
            Debug.DrawRay(position, direction, Color.green);
        }

        else if (hit.collider == null)
        {
            isGrounded = false;
            Debug.DrawRay(position, direction, Color.darkRed);
        }
    }

    public void DoFlipObject(bool flip)
    {
        // get the SpriteRenderer component
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        if (flip == true)
        {
            sr.flipX = true;
            isFacingRight = false;
        }

        else
        {
            sr.flipX = false;
            isFacingRight = true;
        }
    }

}
