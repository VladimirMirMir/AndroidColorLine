using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject m_settings;
    [SerializeField] GameObject m_shop;
    [SerializeField] Text sign;
    [SerializeField]int m_signClicked = 0;
    //[SerializeField] GameObject m_dieScreen;

    bool m_settingsAreOpened = false;
    bool m_shopIsOpened = false;
    //bool m_dieScreenIsOpened = false;

    public void SignActive()
    {
        m_signClicked++;
        if (m_signClicked >= 3)
            sign.text = "Made by Vladimir Miroschnikov. miroschnikovvladimir@yandex.ru";
    }

    public void ToggleSettingsPanel()
    {
        if (m_shopIsOpened)
            ToggleShopPanel();
        m_settings.SetActive(!m_settingsAreOpened);
        m_settingsAreOpened = !m_settingsAreOpened;
    }

    //public IEnumerator ShowDieScreen()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    m_dieScreen.SetActive(true);
    //    m_dieScreenIsOpened = true;
    //}

    public void ToggleShopPanel()
    {
        if (m_settingsAreOpened)
            ToggleSettingsPanel();
        m_shop.SetActive(!m_shopIsOpened);
        m_shopIsOpened = !m_shopIsOpened;
    }

    //public void ToggleDieScreen()
    //{
    //    if (m_dieScreenIsOpened)
    //        ToggleDieScreen();
    //    m_dieScreen.SetActive(!m_dieScreenIsOpened);
    //    m_dieScreenIsOpened = !m_dieScreenIsOpened;
    //}

    public void RestartLevel()
    {
        ChangeScene();
    }

    public void WatchAd()
    {
        //AD
        Revive();
    }

    public void Revive()
    {
        Debug.Log("Rev");
    }

    public void ChangeScene()
    {
        if (m_settingsAreOpened)
            ToggleSettingsPanel();
        if (m_shopIsOpened)
            ToggleShopPanel();
        //if (m_dieScreenIsOpened)
        //    ToggleDieScreen();
        if (PlayerPrefs.GetInt("Level") <= SceneManager.sceneCountInBuildSettings - 1)
        {
            if (PlayerPrefs.GetInt("Level") == 0)
                PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            GameManager.ResetData();
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
    }

    public void ToMainMenu()
    {
        Debug.Log("!!!");
        if (m_settingsAreOpened)
            ToggleSettingsPanel();
        if (m_shopIsOpened)
            ToggleShopPanel();
        //if (m_dieScreenIsOpened)
        //    ToggleDieScreen();
        SceneManager.LoadScene(0);
    }

    public void ResetData()
    {
        GameManager.ResetData();
    }

    public void ToggleSound()
    {
        Debug.Log("SOUND");
    }


    [System.Serializable]
    public class ShopItem
    {
        public Sprite image;
        public Material material;
        public int price;
        public bool isPurchased;
    }

    GameObject m_itemTemplate;
    [SerializeField] Transform m_shopScrollView;
    [SerializeField] List<ShopItem> m_shopItems;
    public Text coinsText;
    Button buyButton;

    private void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString() + "$";
        m_itemTemplate = m_shopScrollView.GetChild(0).gameObject;
        for (int i = 0; i < m_shopItems.Count; i++)
        {
            GameObject g = Instantiate(m_itemTemplate, m_shopScrollView);
            buyButton = g.transform.GetChild(1).GetComponent<Button>();
            buyButton.AddEventListener(i, OnShopItemBtnClicked);
            g.transform.GetChild(2).GetComponent<Text>().text = m_shopItems[i].price.ToString() + "$";
            if (m_shopItems[i].isPurchased)
            {
                g.transform.GetChild(0).GetComponent<Image>().sprite = m_shopItems[i].image;
                g.transform.GetChild(0).GetComponent<Image>().color = m_shopItems[i].material.color;
                buyButton.interactable = false;
                buyButton.transform.GetChild(0).GetComponent<Text>().text = "SOLD";
                g.transform.GetChild(2).GetComponent<Text>().text = "";
            }
        }
        Destroy(m_itemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (GameManager.CanBuy(m_shopItems[itemIndex].price))
        {
            GameManager.IncreaseSpeed(m_shopItems[itemIndex].price);
            m_shopItems[itemIndex].isPurchased = true;
            GameObject g = m_shopScrollView.GetChild(itemIndex).gameObject;
            g.transform.GetChild(0).GetComponent<Image>().sprite = m_shopItems[itemIndex].image;
            g.transform.GetChild(0).GetComponent<Image>().color = m_shopItems[itemIndex].material.color;
            buyButton = g.transform.GetChild(1).GetComponent<Button>();
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "SOLD";
            g.transform.GetChild(2).GetComponent<Text>().text = "+SPEED";
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
            GameManager.instance.trail = m_shopItems[itemIndex].material;
        }
        else
        {
            GameObject g = m_shopScrollView.GetChild(itemIndex).gameObject;
            buyButton = g.transform.GetChild(1).GetComponent<Button>();
            buyButton.transform.GetChild(0).GetComponent<Text>().color = Color.red;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "NO $";
            StartCoroutine(NOMONEY(itemIndex));
        }
    }

    public IEnumerator NOMONEY(int itemIndex)
    {
        float timer = 0f;
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        m_shopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.black;
        m_shopScrollView.GetChild(itemIndex).GetChild(1).GetChild(0).GetComponent<Text>().text = "BUY";
    }
}
