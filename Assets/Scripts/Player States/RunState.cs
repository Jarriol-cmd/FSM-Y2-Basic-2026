
//This is a derived class of State
//This means it inherits fields and methods from State.cs

using UnityEngine;

public class RunState : State
{
    protected float speed;
    protected float rotationSpeed;

    public RunState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        speed = 3;
        base.Enter();
        horizontalInput = verticalInput = 0.0f;

        Debug.Log("entering running state");

        
        player.anim.SetBool("Run", true);
        
    }

    public override void Exit()
    {
        player.anim.SetBool("Run", false);
        base.Exit();
    }



    public override void Update()
    {


        ReadInput();

        if (player.moveAction.ReadValue<Vector2>().magnitude < 0.1f)
        {
            sm.ChangeState(sm.idleState);
        }

        if (player.jumpAction.IsPressed())
        {
            sm.ChangeState(sm.jumpState);
        }

        if (player.attackAction.IsPressed())
        {
            sm.ChangeState(sm.attackState);
        }


        //debug move gameObject
        player.rb.linearVelocity = player.moveAction.ReadValue<Vector2>() * speed;


        UIscript.ui.DrawText("*** This is the running state ***\n");
        UIscript.ui.DrawText("Space = Jump state");
        UIscript.ui.DrawText("Enter to Attack");



    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in runstate");

        if( collision.tag == "enemy")
        {
            
            sm.ChangeState(sm.deathState);
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit collision in runstate");

        if (collision.tag == "enemy")
        {
            
            
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collided in run state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }

    public override void FixedUpdate()
    {
    }
}
