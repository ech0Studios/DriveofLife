using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static int level = 0;
    public static bool isGameOver = false;
    public static bool isDebugMode = false;
    public static int currentState;
    public static int mainScore;
    public static int inGameScore = 0;

    public TextMeshProUGUI mainScoreText;
    public static TextMeshProUGUI winMainScoreText;
    public TextMeshProUGUI vwinMainScoreText;
    public TextMeshProUGUI currentScoreText;
    public static TextMeshProUGUI vcurrentScoreText;
    public GameObject states;
    // Panels   
    public GameObject mainmenuPanel;

    public static GameObject gamePanel;
    public GameObject vgamePanel;
    public static GameObject winPanel;
    public GameObject vwinPanel;
    public GameObject[] statePanels;
    public GameObject debugPanel;
    public GameObject settingsPanel;
    public GameObject challangePanel;
    public GameObject dailyRewardsPanel;
    public GameObject storePanel;
    //Sliders
    public Slider playerSpeedSlider;
    public Slider carSpeedSlider;
    public Slider thiefSpeedSlider;
    public Slider nitroForceSlider;
    public Slider camPosXSlider;
    public Slider camPosYSlider;
    public Slider camPOsZSlider;

    void Start()
    {
        Application.targetFrameRate = 150;
        PlayerScript.playerSpeed = 700;
        mainScore = PlayerPrefs.GetInt("mainScore");
        mainScoreText.text = mainScore.ToString();
        Time.timeScale = 0;
        isGameOver = true;
        gamePanel = vgamePanel;
        winPanel = vwinPanel;
        vcurrentScoreText = currentScoreText;
        winMainScoreText = vwinMainScoreText;
    }


    void Update()
    {
        if (isDebugMode)
        {
            PlayerScript.playerSpeed = playerSpeedSlider.value;
            CarScript.carSpeed = carSpeedSlider.value;
            CarBackScript.carSpeed = carSpeedSlider.value;
            AgentScript.thiefSpeed = thiefSpeedSlider.value;
            CamFollow.posX = camPosXSlider.value;
            CamFollow.posY = camPosYSlider.value;
            CamFollow.posZ = camPOsZSlider.value;
        }
    }
    public static void Win()
    {
        isGameOver = true;
        Time.timeScale = 0;
        MMVibrationManager.Haptic(HapticTypes.Success);
        winMainScoreText.text = mainScore.ToString();
        vcurrentScoreText.text = inGameScore.ToString();
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        isGameOver = true;
        MMVibrationManager.Haptic(HapticTypes.Failure);
        gamePanel.SetActive(false);
    }
    private void SpawntheState()
    {
        int i = Random.Range(0, 3);       
        currentState = i;
        statePanels[i].SetActive(true);
        states.transform.GetChild(i).gameObject.SetActive(true);
        Time.timeScale = 1;
        isGameOver = false;
        mainmenuPanel.SetActive(false);
        winPanel.SetActive(false);
        gamePanel.SetActive(true);
        PlayerScript.dontMove = false;
    }

    #region BUTTONS
    public void DebugMenuCloseButton()
    {
        if (currentState == 1)
        {
            statePanels[1].SetActive(true);
        }
        isGameOver = false;
        debugPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void DebugModeButton()
    {
        debugPanel.SetActive(true);
        isGameOver = true;
        statePanels[1].SetActive(false);
        Time.timeScale = 0;

    }
    public void StoreButton()
    {
        storePanel.SetActive(true);
    }
    public void ChallangePanelButton()
    {
        challangePanel.SetActive(true);
    }
    public void DailyRewardsButon()
    {
        dailyRewardsPanel.SetActive(true);
    }
    public void DebugModeOffButton()
    {
        isDebugMode = false;
        PlayerScript.playerSpeed = 700;
        CarScript.carSpeed = 8;
        CarBackScript.carSpeed = 8;
        AgentScript.thiefSpeed = 25;
        CamFollow.posX = 0;
        CamFollow.posY = 8;
        CamFollow.posZ = 0;

    }
    public void DebugModeOnButton()
    {
        isDebugMode = true;
    }
    public void StartButton()
    {
        // Spawn The First State
        SpawntheState();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);

    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseButton()
    {
        settingsPanel.SetActive(false);
        dailyRewardsPanel.SetActive(false);
        challangePanel.SetActive(false);
        storePanel.SetActive(false);
    }
    public void PauseButton()
    {
        isGameOver = true;
        gamePanel.SetActive(false);

    }
    public void ContinueButton()
    {
        isGameOver = false;
        gamePanel.SetActive(true);

    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevelButton()
    {
        PlayerPrefs.SetInt("inGameScore", 0);

        PlayerPrefs.SetInt("mainScore", (mainScore + inGameScore));
        inGameScore = 0;
        SceneManager.LoadScene(0);
    }
    #endregion

}
