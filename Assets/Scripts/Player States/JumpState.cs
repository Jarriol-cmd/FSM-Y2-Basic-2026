//This is a derived class of State
//This means it inherits fields and methods from State.cs


using UnityEngine;

public class JumpState : State
{
    float rotationSpeed;


    public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        player.anim.SetBool("Jump", true);
        isGrounded = false;
        speed = 3;
        player.rb.linearVelocityY = 4f;
    }

    public override void Exit()
    {
        player.anim.SetBool("Jump", false);
        isGrounded = true;
    }

    public override void Update()
    {
        GroundCheck();

        ReadInput();

        if (player.rb.linearVelocityX > 0.1f && isGrounded == true)
        {
            sm.ChangeState(sm.idleState);

        }

        if (player.rb.linearVelocityX < 0.1f && isGrounded == true)
        {
            sm.ChangeState(sm.runState);
        }

        if (player.attackAction.IsPressed())
        {
            sm.ChangeState(sm.attackState);
        }

        player.rb.linearVelocityX = player.moveAction.ReadValue<Vector2>().x * speed;

        if (player.rb.linearVelocityX > 0)
        {
            isFacingRight = true;
        }

        if (player.rb.linearVelocityX < 0)
        {
            isFacingRight = false;
        }

        UIscript.ui.DrawText("*** This is the jumping state ***\n");
        UIscript.ui.DrawText("Left/Right arrows while grounded = Move State");
        UIscript.ui.DrawText("Press nothing to enter Idle State");
        UIscript.ui.DrawText("Enter to Attack");


        if (player.rb.linearVelocityX >= 0 && isFacingRight == true)
        {
            DoFlipObject(false);

        }

        if (player.rb.linearVelocityX <= -0 && isFacingRight == false)
        {
            DoFlipObject(true);
        }

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in jump state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collided in jump state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }

    public override void FixedUpdate()
    {
        //Fixed Update 
    }
}
