using Unity.VisualScripting;
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

        Debug.Log("attack:" + player.isFacingRight);

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

        if (player.isFacingRight == true)
        {
            GameObject clone;
            clone = GameObject.Instantiate(player.weapon, player.transform.position, Quaternion.identity);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(15, 0);

            rb.transform.position = new Vector3(player.transform.position.x + 0.75f, player.transform.position.y, player.transform.position.z);

            rb.transform.Rotate(new Vector3(0, 0, 315));
        }

        if (player.isFacingRight == false)
        {
            GameObject clone;
            clone = GameObject.Instantiate(player.weapon, player.transform.position, Quaternion.identity);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(-15, 0);

            rb.transform.position = new Vector3(player.transform.position.x - 0.75f, player.transform.position.y, player.transform.position.z);

            rb.transform.Rotate(new Vector3(0, 0, 135));
        }


    }


}