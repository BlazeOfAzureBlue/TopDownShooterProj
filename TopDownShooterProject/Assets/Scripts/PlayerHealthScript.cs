using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int PlayerMaxHealth;
    public int PlayerHealth;
    public GameObject player;
    public GameObject[] RespawnPoints;
    public bool playerDead;

    private bool Invincibilityframe;
    private float Timer;

    public TMP_Text playerHealthGUI;
    public SoundManager soundManager;
    public EnemyManager enemyManager;

    private void Start()
    {
        PlayerHealth = PlayerMaxHealth;
        playerHealthGUI.text = PlayerHealth.ToString() + " / " + PlayerMaxHealth.ToString();
    }

    private void Update()
    {
        if (!Invincibilityframe)
        {
            Timer += Time.deltaTime;
            if (Timer > 1)
            {
                Invincibilityframe = true;
                Timer = 0;
            }
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        int NewSpawn = UnityEngine.Random.Range(0, RespawnPoints.Length);
        player.transform.position = RespawnPoints[NewSpawn].transform.position;
        PlayerHealth = PlayerMaxHealth;
        playerHealthGUI.text = PlayerHealth.ToString() + " / " + PlayerMaxHealth.ToString();
        player.SetActive(true);
        playerDead = false;
    }

    public void IncreaseHealth()
    {
        PlayerHealth += 1;
        playerHealthGUI.text = PlayerHealth.ToString() + " / " + PlayerMaxHealth.ToString();
    }

    public void OnHit()
    {
        if (Invincibilityframe)
        {
            PlayerHealth -= 1;
            Invincibilityframe = false;
            playerHealthGUI.text = PlayerHealth.ToString() + " / " + PlayerMaxHealth.ToString();
            soundManager.PlaySound("HitSound");
            if (PlayerHealth <= 0)
            {
                playerDead = true;
                player.SetActive(false);
                StartCoroutine(RespawnPlayer());
                soundManager.PlaySound("Death");
                enemyManager.Death();
            }
        }
    }
}
