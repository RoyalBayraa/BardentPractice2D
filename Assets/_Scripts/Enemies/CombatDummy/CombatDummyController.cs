using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private float maxHealth, knockBackSpeedX, knockBackSpeedY, knockBackDuration, knockBackDeathSpeedX, knockBackDeathSpeedY, deathTorque;

    [SerializeField] private GameObject aliveGO, brokenTopGO, brokenBotGO;
    [SerializeField] private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;

    [SerializeField] private bool applyKnockBack;
    private PlayerController pc;
    private Animator aliveAnim;

    private int playerFacingDir;

    private float currentHealth, knockbackStart;

    private bool playerOnLeft, knockBack;

    private void Start()
    {
        currentHealth = maxHealth;
        pc = InSceneInstancesSingleton.Instance.PlayerControllerRef;
        aliveAnim = aliveGO.GetComponent<Animator>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockBack();
    }

    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        if (attackDetails.position.x < aliveGO.transform.position.x)
        {
            playerFacingDir = 1;
        }
        else
        {
            playerFacingDir = -1;
        }

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(.0f, .0f, Random.Range(0.0f, 360f))); ;
        if (playerFacingDir == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockBack && currentHealth > 0.0f)
        {
            // knock back
            KnockBack();
        }

        if (currentHealth <= 0.0f)
        {
            //die
            Die();
        }
    }


    private void KnockBack()
    {
        knockBack = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockBackSpeedX * playerFacingDir, knockBackSpeedY);
    }

    private void CheckKnockBack()
    {
        if (Time.time >= knockbackStart + knockBackDuration && knockBack)
        {
            knockBack = false;
            rbAlive.velocity = new Vector2(.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockBackSpeedX * playerFacingDir, knockBackSpeedY);
        rbBrokenBot.velocity = new Vector2(knockBackDeathSpeedX * playerFacingDir, knockBackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDir, ForceMode2D.Impulse);

    }
}
