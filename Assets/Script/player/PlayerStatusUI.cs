using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    public Slider PlayerhealthSlider;
    public GameObject DM_Soul_Text;
    public GameObject HP;
    public GameObject Now;
    public GameObject Level;

    public PlayerStatus playerStatus;
    private TextMesh DM_Soul_TextMesh;
    private TextMesh HP_TextMesh;
    private TextMesh Now_TextMesh;
    private TextMesh Level_TextMesh;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        PlayerhealthSlider = GetComponentInChildren<Slider>();

        DM_Soul_TextMesh = DM_Soul_Text.GetComponent<TextMesh>();
        HP_TextMesh = HP.GetComponent<TextMesh>();
        Now_TextMesh = Now.GetComponent<TextMesh>();
        Level_TextMesh = Level.GetComponent<TextMesh>();

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        PlayerhealthSlider.value = (float)playerStatus.playerHP / playerStatus.playerMaxHP;

        if (DM_Soul_TextMesh != null && HP_TextMesh != null && Now_TextMesh != null)
        {
            DM_Soul_TextMesh.text = "Damage : " + playerStatus.playerDamage.ToString() + " || " + "Soul : " + playerStatus.playerSoul.ToString();
            HP_TextMesh.text = playerStatus.playerHP.ToString() + " / " + playerStatus.playerMaxHP.ToString();
            Now_TextMesh.text = "Floor : " + playerStatus.playerFloor.ToString() + " || Kill : "+playerStatus.Kill_Count.ToString() + " || Box : " + playerStatus.Count_Box.ToString();
            Level_TextMesh.text = "HPLv. " + playerStatus.HPLv.ToString() + " || ATKLv. " + playerStatus.ATKLv.ToString();
        }
    }
}
