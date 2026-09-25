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

        player.sr.color = new Color(0.8f, 0.3f, 0.4f);  //change the sprite colour
    }

    public override void Exit()
    {
        //exit the jump state
    }

    public override void Update()
    {
        player.anim.SetBool("Idle", true);
        player.anim.SetBool("Run", true);
        player.anim.SetBool("Death", true);
        player.anim.SetBool("Attack", true);
        player.anim.SetBool("Jump", true);

        ReadInput();

        if (player.interactAction.IsPressed())
        {
            sm.ChangeState(sm.idleState);

        }

        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f )
        {
            sm.ChangeState(sm.runState);
        }

        if (player.attackAction.IsPressed())
        {
            sm.ChangeState(sm.attackState);
        }


        UIscript.ui.DrawText("*** This is the jumping state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("E = Idle State");
        UIscript.ui.DrawText("Enter to Attack");


    }

    public override void OnTriggerEnter2D(Collider2D collision)
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
