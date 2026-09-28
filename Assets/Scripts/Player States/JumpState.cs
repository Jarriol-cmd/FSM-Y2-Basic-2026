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
    }

    public override void Exit()
    {
        player.anim.SetBool("Jump", false);
    }

    public override void Update()
    {
        

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
