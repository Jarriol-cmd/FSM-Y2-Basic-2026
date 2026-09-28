using UnityEngine;

public class DeathState : State
{

    public DeathState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {

        player.anim.SetBool("Dead", true);
    }


    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("Dead", false);
    }

    public override void Update()
    {
        
        
        


        if (player.interactAction.IsPressed())
        {
            sm.ChangeState(sm.idleState);
        }

        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f)
        {
            sm.ChangeState(sm.runState);
        }

        if (player.jumpAction.IsPressed())
        {
            sm.ChangeState(sm.jumpState);
        }

        UIscript.ui.DrawText("*** You Are Dead ***\n");
        UIscript.ui.DrawText("Space to Jump");
        UIscript.ui.DrawText("Arrows to Move");
        UIscript.ui.DrawText("E to Idle");


    }


}
