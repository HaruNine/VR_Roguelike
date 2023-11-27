// PlayerStatus.cs
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int playerMaxHP = 100;
    public int playerHP { get; private set; }
    public int playerDamage = 20;
    public int playerSoul { get; private set; }
    public int paySoulHP = 10;
    public int paySoulDM = 10;
    public int UpHP = 10;
    public int UpDM = 3;
    public int playerFloor { get; private set; }
    public int Count_Box {  get; private set; }
    public int Kill_Count {  get; private set; }
    public int HPLv {  get; private set; }
    public int ATKLv {  get; private set; }

    private void Awake()
    {
        playerHP = playerMaxHP;
        //playerFloor = 40;
        //playerHP = 50;
        //playerSoul = 1000;
    }

    // 플레이어가 피해를 입음
    public void TakeDamage(int damage)
    {
        playerHP -= damage;

        // 피가 음수가 되지 않도록 보호
        playerHP = Mathf.Max(playerHP, 0);

        if (playerHP <= 0)
        {
            Die(); // 플레이어 체력이 0 이하이면 Die 메서드 호출
        }
    }

    // 플레이어가 사망
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetSoul(int soul)
    {
        Kill_Count++;
        playerSoul += soul;
    }

    public void TreasureBox(int soul)
    {
        PlayerController PC = FindObjectOfType<PlayerController>();
        PC.buff_boxEF.Play();
        playerSoul += (soul+(playerFloor/10*10));
        Count_Box++;
    }

    public void RecoveryHP()
    {
        playerHP = playerMaxHP;
    }

    public void UP_MaxHP()
    {
        playerSoul -= paySoulHP;
        playerMaxHP += UpHP;
        playerHP += UpHP;
        paySoulHP += 10;
        UpHP += 2;
        HPLv++;
    }

    public void UP_playerDamage()
    {
        playerSoul -= paySoulDM;
        playerDamage += UpDM;
        paySoulDM += 10;
        UpDM += 1;
        ATKLv++;
    }

    public void Player_Clear()
    {
        playerFloor++;
    }

    public void downHP()
    {
        PlayerController PC = FindObjectOfType<PlayerController>();
        PC.debuff_boxEF.Play();
        if (playerHP <= (20 + (playerFloor / 10 * 5)))
        {
            playerHP = playerHP/2;
        }
        else { playerHP -= (20 + (playerFloor / 10 * 5)); }
    }

    public void downSoul()
    {
        PlayerController PC = FindObjectOfType<PlayerController>();
        PC.debuff_boxEF.Play();
        if (playerSoul <= (10 + (playerFloor / 10 * 10)))
        {
            playerSoul = 0;
        }
        else { playerSoul -= (10 + (playerFloor / 10 * 10)); }
    }

    public void upStats()
    {
        PlayerController PC = FindObjectOfType<PlayerController>();
        PC.buff_boxEF.Play();
        if (playerHP + (5 + (playerFloor / 10 * 10)) >= playerMaxHP)
        {
            playerHP = playerMaxHP;
        }
        else { playerHP += (5 + (playerFloor / 10 * 10)); }
        playerSoul += (5 + (playerFloor / 10 * 10));
    }

    public void RandomSelectChest()
    {
        int random = Mathf.Clamp(Random.Range(-2, 4), 0, 3);

        switch (random)
        {
            case 0:
                TreasureBox(20);
                break;
            case 1:
                downHP();
                break;
            case 2:
                downSoul();
                break;
            case 3:
                upStats();
                break;
            default:
                break;
        }
    }
}
