
//This is a derived class of State
//This means it inherits fields and methods from State.cs

using UnityEngine;
using System.Collections;

public class IdleState : State
{
    // constructor
    public IdleState( PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        // this method is called when the state begins

        player.anim.SetBool("Idle", true);
        isGrounded = true;
    }

    public override void Exit()
    {
        // this method is called when the state has finished

        player.anim.SetBool("Idle", false);
    }


    public override void Update()
    {


        if ( player.moveAction.ReadValue<Vector2>().magnitude > 0.1f && isGrounded == true )
        {
            sm.ChangeState(sm.runState);
        }

        if (player.jumpAction.IsPressed())
        {
            sm.ChangeState(sm.jumpState);
        }


        if(player.attackAction.IsPressed())
        {
            sm.ChangeState(sm.attackState);
        }

        

        UIscript.ui.DrawText("*** This is the idle state ***\n");
        UIscript.ui.DrawText("Space = Jump State");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("Enter to Attack");


    }

    public override void FixedUpdate()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in idle state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collided in idle state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }

}
