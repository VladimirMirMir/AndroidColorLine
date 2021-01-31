using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public AudioClip click;

    public void ToggleMenu()
    {
        panel1.SetActive(!panel1.activeSelf);
    }

    public void ToggleMenu2()
    {
        panel2.SetActive(!panel2.activeSelf);
    }

    public void ToMenu()
    {
        GameObject.Find("GameManager").GetComponent<SoundPlayer>().PlaySound(click); ;
        if (panel1.activeSelf)
            ToggleMenu();
        if (panel2.activeSelf)
            ToggleMenu2();
        SceneManager.LoadScene(0);
    }

    public void Revive()
    {
        //ad
        GameObject.Find("GameManager").GetComponent<SoundPlayer>().PlaySound(click); ;
        if (PlayerPrefs.GetInt("Level") <= SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        else
            ToMenu();
    }
}
