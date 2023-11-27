using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GhostState
{
    Idle,
    Move,
    Attack,
    Die
}

public class GhostStatus : MonoBehaviour
{
    public int enemyMaxHP = 60;
    public int enemyHP;
    public int enemyDamage = 7;
    
    public Slider healthSlider; // UI Slider를 참조하기 위한 변수

    public GhostState currentState = GhostState.Idle;
    public Animator ani;



    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        ani.SetBool("stand", true);
        ani.SetBool("walk", false);
        ani.SetBool("atk", false);
        ani.SetBool("die", false);
        UpdateEnemyStatus();
        enemyHP = enemyMaxHP;
        // healthSlider를 찾아서 할당
        healthSlider = GetComponentInChildren<Slider>();
        UpdateHealthUI(); // 초기 UI 업데이트
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;

        // 피가 음수가 되지 않도록 보호
        enemyHP = Mathf.Max(enemyHP, 0);
        
        UpdateHealthUI(); // 피가 감소할 때 UI 업데이트

        

    }

    public void EnemyDeath()
    {
        // 적이 죽으면 PlayerSoul을 10씩 증가
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.GetSoul(10);
        }
    }

    void UpdateHealthUI()
    {
        // Slider의 값을 피의 양에 맞게 업데이트
        healthSlider.value = (float)enemyHP / enemyMaxHP;
    }

    void UpdateEnemyStatus()
    {
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        System.Random random = new System.Random();
        int floor = playerStatus.playerFloor;
        for (int i = 0; i < floor; i++)
        {
            enemyMaxHP += random.Next(2, 4);
            enemyDamage += random.Next(1, 2);
        }
        
    }

    public void SetState(GhostState newState)
    {
        currentState = newState;
    }

}
