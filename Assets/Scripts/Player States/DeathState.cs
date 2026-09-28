using UnityEngine;

public class DeathState : State
{

    public DeathState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {

        player.sr.color = new Color(0.9f, 0.9f, 0.9f);  //change the sprite colour
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        player.anim.SetBool("Idle", false);
        player.anim.SetBool("Run", false);
        player.anim.SetBool("Dead", true);
        player.anim.SetBool("Attack", false);
        player.anim.SetBool("Jump", false);


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
