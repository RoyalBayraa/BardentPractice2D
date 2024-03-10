using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }


    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;


    public D_DodgeState dodgeStateData;


    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;
    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, statemachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, statemachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, statemachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, statemachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData,  this);
        lookForPlayerState = new E2_LookForPlayerState(this, statemachine, "lookForPlayer", lookForPlayerStateData,  this);
        stunState = new E2_StunState(this, statemachine, "stun", stunStateData,  this);
        deadState = new E2_DeadState(this, statemachine, "dead", deadStateData, this);
        dodgeState = new E2_DodgeState(this, statemachine, "dodge", dodgeStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, statemachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData,  this);

        statemachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (isDead)
        {
            statemachine.ChangeState(deadState);
        }
        else if (isStunned && statemachine.currentState != stunState)
        {
            statemachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMaxAgroRange())
        {
            statemachine.ChangeState(rangedAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTrunImmedietly(true);
            statemachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
