
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

        player.sr.color = new Color(0.5f, 0.8f, 0.7f);
    }

    public override void Exit()
    {
        // this method is called when the state has finished

        //you should disable any running coroutines here
        player.StopAllCoroutines();
    }


    public override void Update()
    {
        player.anim.SetBool("Idle", true);
        player.anim.SetBool("Run", false);
        player.anim.SetBool("Death", false);
        player.anim.SetBool("Attack", false);
        player.anim.SetBool("Jump", false);

        if ( player.moveAction.ReadValue<Vector2>().magnitude > 0.1f )
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

        //example of running a coroutine from a state and not directly from the monobehaviour
        if (player.crouchAction.IsPressed())
        {
            player.StartCoroutine( IdleCo() );
        }

        UIscript.ui.DrawText("*** This is the idle state ***\n");
        UIscript.ui.DrawText("Space = Jump State");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("C = Start the coroutine");
        UIscript.ui.DrawText("Enter to Attack");


    }

    public override void FixedUpdate()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
    }


    public IEnumerator IdleCo()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Coroutine step 1");

            yield return new WaitForSeconds(2);
            Debug.Log("Coroutine step 2");

            Debug.Log("Coroutine repeat " + (i+1));

        }
        yield break;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in idle state");

        if (collision.tag == "enemy")
        {
            sm.ChangeState(sm.deathState);
        }
    }


}
