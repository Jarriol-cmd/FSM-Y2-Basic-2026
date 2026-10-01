using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : State
{
    float timer = 0f;

    public AttackState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        timer = 1f;
        player.anim.SetBool("Attack", true);
    }


    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("Attack", false);
    }

    public override void Update()
    {
        GroundCheck();

        timer -= Time.deltaTime;

        if (player.rb.linearVelocityX < 0.1f && isGrounded == true && timer <= 0)
        {
            sm.ChangeState(sm.idleState);
        }

        if (player.rb.linearVelocityX > 0.1f && isGrounded == true && timer <= 0)
        {
            sm.ChangeState(sm.runState);
        }

        if (player.jumpAction.IsPressed() && isGrounded == true)
        {
            sm.ChangeState(sm.jumpState);
        }

        UIscript.ui.DrawText("*** You have Attacked ***\n");
        UIscript.ui.DrawText("Space to Jump");
        UIscript.ui.DrawText("Arrows to Move");
        UIscript.ui.DrawText("Do nothing to Idle");



    }


}