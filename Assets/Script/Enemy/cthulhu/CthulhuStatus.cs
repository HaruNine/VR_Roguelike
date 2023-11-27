using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CthulhuState
{
    Idle,
    Move,
    Attack,
    Die
}

public class CthulhuStatus : MonoBehaviour
{
    public int enemyMaxHP = 120;
    public int enemyHP;
    public int enemyDamage = 13;
    
    public Slider healthSlider; // UI Slider�� �����ϱ� ���� ����

    public CthulhuState currentState = CthulhuState.Idle;
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
        // healthSlider�� ã�Ƽ� �Ҵ�
        healthSlider = GetComponentInChildren<Slider>();
        UpdateHealthUI(); // �ʱ� UI ������Ʈ
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;

        // �ǰ� ������ ���� �ʵ��� ��ȣ
        enemyHP = Mathf.Max(enemyHP, 0);
        
        UpdateHealthUI(); // �ǰ� ������ �� UI ������Ʈ

        

    }

    public void EnemyDeath()
    {
        // ���� ������ PlayerSoul�� 10�� ����
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.GetSoul(10);
        }
    }

    void UpdateHealthUI()
    {
        // Slider�� ���� ���� �翡 �°� ������Ʈ
        healthSlider.value = (float)enemyHP / enemyMaxHP;
    }

    void UpdateEnemyStatus()
    {
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        System.Random random = new System.Random();
        int floor = playerStatus.playerFloor;
        for (int i = 0; i < floor; i++)
        {
            enemyMaxHP += random.Next(3, 6);
            enemyDamage += random.Next(1, 3);
        }
        
    }

    public void SetState(CthulhuState newState)
    {
        currentState = newState;
    }

}
