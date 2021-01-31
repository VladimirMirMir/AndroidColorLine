using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioClip dieSound;
    public AudioClip finishSound;
    public bool isAlive = true;
    public bool isFinished = false;
    public float baseSpeed = 5;
    public int coins = 0;
    public int currentLevel = 1;
    public Material trail;
    int m_deathCounter = 0;

    public static void Finish()
    {
        instance.GetComponent<SoundPlayer>().PlaySound(instance.finishSound);
        instance.isFinished = true;
        RewardWithCoins(20);
        instance.currentLevel++;
        PlayerPrefs.SetInt("Level", instance.currentLevel);
        GameObject.Find("Canvas").GetComponent<EndGameMenu>().ToggleMenu2();
    }

    public static void Die()
    {
        instance.GetComponent<SoundPlayer>().PlaySound(instance.dieSound);
        GameObject.Find("Canvas").GetComponent<EndGameMenu>().ToggleMenu();
        instance.m_deathCounter++;
        if(instance.m_deathCounter >= 3)
        {
            if(Advertisement.IsReady())
            {
                Advertisement.Show("video");
                instance.m_deathCounter = 0;
            }
        }
    }

    public static void ResetData()
    {
        Debug.Log("reset");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level", 1);
        instance.currentLevel = 1;
        PlayerPrefs.SetInt("Coins", 0);
        instance.coins = 0;
        PlayerPrefs.SetFloat("Speed", 5);
        instance.baseSpeed = 5;
    }

    public static void RewardWithCoins(int coinsCount)
    {
        instance.coins += coinsCount;
        PlayerPrefs.SetInt("Coins", instance.coins);
    }

    public static bool CanBuy(int price)
    {
        return instance.coins >= price;
    }

    public static void IncreaseSpeed(int price)
    {
        if (instance.coins >= price)
        {
            instance.baseSpeed += 2.5f;
            instance.coins -= price;
            PlayerPrefs.SetFloat("Speed", instance.baseSpeed);
            PlayerPrefs.SetInt("Coins", instance.coins);
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        if(Advertisement.isSupported)
        {
            Advertisement.Initialize("3913703", false);
        }
    }
}