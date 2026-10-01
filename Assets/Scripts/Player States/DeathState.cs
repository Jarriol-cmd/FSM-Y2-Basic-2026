using UnityEngine;

public class DeathState : State
{
    float deadFor;

    public DeathState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        deadFor = 3f;
        player.anim.SetBool("Dead", true);
    }


    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("Dead", false);
    }

    public override void Update()
    {
        GroundCheck();
        deadFor -= Time.deltaTime;
        
        


        if (player.rb.linearVelocityX < 0.1f && deadFor <= 0 && isGrounded == true)
        {
            sm.ChangeState(sm.idleState);
        }

        if (player.rb.linearVelocityX > 0.1f && deadFor <= 0 && isGrounded == true)
        {
            sm.ChangeState(sm.runState);
        }

        if (player.jumpAction.IsPressed() && deadFor <= 0)
        {
            sm.ChangeState(sm.jumpState);
        }

        UIscript.ui.DrawText("*** You Are Dead ***\n");
        UIscript.ui.DrawText("Space to Jump");
        UIscript.ui.DrawText("Arrows to Move on the ground");
        UIscript.ui.DrawText("Do nothing to Idle");


    }


}
