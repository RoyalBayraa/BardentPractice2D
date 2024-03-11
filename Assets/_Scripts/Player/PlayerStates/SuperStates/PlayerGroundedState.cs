using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    protected int xInput;

    public PlayerGroundedState(Player player, PlayerStateMahcine stateMahcine, PlayerData playerData, string animBoolName) : base(player, stateMahcine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormalizedInputX;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
