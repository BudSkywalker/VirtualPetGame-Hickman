using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet Game
//Name: Logan Hickman
//Section: 2021SP.SGD.213.2101
//Instructor: Aurore Wold
//Date: 02/22/2022
/////////////////////////////////////////////
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Current active pet
    /// </summary>
    public Pet activePet;

    /// <summary>
    /// How long needs to pass to win the game
    /// </summary>
    [SerializeField]
    private float winTime = 300f;
    /// <summary>
    /// How long needs to pass to be able to do something with the pet again
    /// </summary>
    [SerializeField] 
    private float cooldownTime = 1f;
    /// <summary>
    /// Timer to track cooldown
    /// </summary>
    private float cooldownTimer;
    /// <summary>
    /// Timer to track gameplay time
    /// </summary>
    private float gameTimer;
    /// <summary>
    /// Makes sure game is being played
    /// </summary>
    private bool isPlaying = false;
    /// <summary>
    /// Hunger display
    /// </summary>
    [SerializeField] 
    private Slider hungerSlider;
    /// <summary>
    /// Happiness display
    /// </summary>
    [SerializeField]
    private Slider happinessSlider;
    /// <summary>
    /// Energy display
    /// </summary>
    [SerializeField] 
    private Slider energySlider;
    /// <summary>
    /// Button to feed pet
    /// </summary>
    [SerializeField]
    private Button feedButton;
    /// <summary>
    /// Button to play with pet
    /// </summary>
    [SerializeField]
    private Button playWithButton;
    /// <summary>
    /// Button to make pet rest
    /// </summary>
    [SerializeField]
    private Button restButton;
    /// <summary>
    /// Input field for the pet name
    /// </summary>
    [SerializeField]
    private TMP_InputField nameInput;
    /// <summary>
    /// Button that starts the game
    /// </summary>
    [SerializeField]
    private Button playButton;
    /// <summary>
    /// Panel that contains the start menu
    /// </summary>
    [SerializeField]
    private GameObject startPanel;
    /// <summary>
    /// Panel that contains the game
    /// </summary>
    [SerializeField]
    private GameObject gamePanel;
    /// <summary>
    /// Panel for game over
    /// </summary>
    [SerializeField]
    private GameObject endPanel;
    /// <summary>
    /// Message for game over
    /// </summary>
    [SerializeField]
    private TMP_Text endText;


    void Update()
    {
        if(activePet == null)
        {
            return;
        }

        if (activePet.Happiness <= 0 || activePet.Hunger <= 0 || activePet.Energy <= 0 ||
           activePet.Happiness > 100 || activePet.Hunger > 100 || activePet.Energy > 100)
        {
            GameOver(false);
        }
        else if (gameTimer + winTime < Time.time) 
        {
            GameOver(true);
        }

        if(cooldownTimer + cooldownTime < Time.time)
        {
            feedButton.interactable = true;
            playWithButton.interactable = true;
            restButton.interactable = true;
        }

        if(!isPlaying)
        {
            feedButton.interactable = false;
            playWithButton.interactable = false;
            restButton.interactable = false;
        }
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame()
    {
        isPlaying = true;
        gameTimer = Time.time;
        cooldownTimer = Time.time;
        StartCoroutine(DecreaseStats());
    }

    /// <summary>
    /// Feeds the pet
    /// </summary>
    public void FeedPet()
    {
        activePet.Eat();
        feedButton.interactable = false;
        playWithButton.interactable = false;
        restButton.interactable = false;
        cooldownTimer = Time.time;
        UpdateStats();
    }

    /// <summary>
    /// Plays with the pet
    /// </summary>
    public void PlayWithPet()
    {
        activePet.Play();
        feedButton.interactable = false;
        playWithButton.interactable = false;
        restButton.interactable = false;
        cooldownTimer = Time.time;
        UpdateStats();
    }

    /// <summary>
    /// Makes the pet go to bed
    /// </summary>
    public void RestPet()
    {
        activePet.Rest();
        feedButton.interactable = false;
        playWithButton.interactable = false;
        restButton.interactable = false;
        cooldownTimer = Time.time;
        UpdateStats();
    }

    /// <summary>
    /// Checks to see if the play button should be enabled
    /// </summary>
    public void CheckEnablePlay()
    {
        if (nameInput.text == "")
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void PlayGame()
    {
        FindObjectOfType<GameManager>().activePet = new Pet(nameInput.text);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        FindObjectOfType<GameManager>().StartGame();
    }

    /// <summary>
    /// Reloads the scene
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Handles what happens when the game ends
    /// </summary>
    /// <param name="win">Did the game win or lose</param>
    private void GameOver(bool win)
    {
        isPlaying = false;
        gamePanel.SetActive(false);
        endPanel.SetActive(true);

        if(win)
        {
            endText.text = "Congrats! You kept " + activePet.PetName + " alive for 5 minutes!";
        }
        else
        {
            if(activePet.Hunger > 100)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for overfeeding your pet";
            }
            else if(activePet.Hunger <= 0)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for neglecting to feed them";
            }
            else if (activePet.Happiness > 100)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for spoiling your pet";
            }
            else if (activePet.Happiness <= 0)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for neglecting their happiness";
            }
            else if (activePet.Energy > 100)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for making it lazy";
            }
            else if (activePet.Energy <= 0)
            {
                endText.text = "You lost " + activePet.PetName + " to pet services for letting it get exhausted";
            }
        }
    }

    /// <summary>
    /// Updates the UI
    /// </summary>
    private void UpdateStats()
    {
        hungerSlider.value = activePet.Hunger;
        hungerSlider.fillRect.GetComponent<Image>().color = new Color(2 - (activePet.Hunger / 50f), activePet.Hunger / 50f, 0);
        hungerSlider.GetComponentInChildren<TMP_Text>().text = "Hunger: " + activePet.Hunger;

        happinessSlider.value = activePet.Happiness;
        happinessSlider.fillRect.GetComponent<Image>().color = new Color(2 - (activePet.Happiness / 50f), activePet.Happiness / 50f, 0);
        happinessSlider.GetComponentInChildren<TMP_Text>().text = "Happiness: " + activePet.Happiness;

        energySlider.value = activePet.Energy;
        energySlider.fillRect.GetComponent<Image>().color = new Color(2 - (activePet.Energy / 50f), activePet.Energy / 50f, 0);
        energySlider.GetComponentInChildren<TMP_Text>().text = "Energy: " + activePet.Energy;
    }

    /// <summary>
    /// Decreases stats randomly every 3 seconds
    /// </summary>
    IEnumerator DecreaseStats()
    {
        while(isPlaying)
        {
            activePet.DecreaseStatsRandomly();
            UpdateStats();
            yield return new WaitForSeconds(3);
        }
    }
}
