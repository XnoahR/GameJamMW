using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public bool detected, lockmode, isAttacking;
    public int health;
    [SerializeField] private int hp;
    [SerializeField] protected float speed;
    private void Awake()
    {
        hp = health;
    }
    public void Attack()
    {

    }
    public void Damaged()
    {
        hp -= 50;
        if (hp <= 0)
        {
            //Die
            Die();
        }
    }
    public void Die()
    {
        GetComponent<Animator>().SetBool("Die", true);
        Destroy(gameObject, 3);
    }
    public void LockPlayer()
    {
        detected = true;
        Debug.Log("Detected");
    }
}

public interface IEnemy
{
    public void Die();
    public void Attack();
    public void Damaged();
    public void LockPlayer();
}
