using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {

	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
	public GameObject gameTime;
	
	public Text scoreText; // UI reference
    private int score = 0;

	// Sound effects
    public AudioClip flipSound;
    public AudioClip matchSound;
    public AudioClip mismatchSound;
    public AudioClip gameOverSound;

    private AudioSource audioSource;


	private bool _init = false;
	private int _matches = 4;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		LoadGame();
    }

    // Update is called once per frame
    void Update () {
		if (!_init)
			initializeCards ();

		if (Input.GetMouseButtonUp (0))
			checkCards ();

	}

	void initializeCards()
	{
		for (int id = 0; id < 2; id++)
		{
			for (int i = 1; i < 5; i++)
			{

				bool test = false;
				int choice = 0;
				while (!test)
				{
					choice = Random.Range(0, cards.Length);
					test = !(cards[choice].GetComponent<cardScript>().initialized);
				}
				cards[choice].GetComponent<cardScript>().cardValue = i;
				cards[choice].GetComponent<cardScript>().initialized = true;
			}
		}

		foreach (GameObject c in cards)
			c.GetComponent<cardScript>().setupGraphics();

		if (!_init)
			_init = true;
	}

	public Sprite getCardBack() {
		return cardBack;
	}

	public Sprite getCardFace(int i) {
		return cardFace[i - 1];
	}

	void checkCards() {
		List<int> c = new List<int> ();

		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<cardScript> ().state == 1)
				c.Add (i);
		}

		if (c.Count == 2)
			cardComparison (c);
	}

	void cardComparison(List<int> c){
		cardScript.DO_NOT = true;

		int x = 0;

		if (cards[c[0]].GetComponent<cardScript>().cardValue == cards[c[1]].GetComponent<cardScript>().cardValue)
		{
			score += 10;
			x = 2;
			_matches--;
			UpdateScoreText();
			SaveGame();

			PlaySound(matchSound);

			if (_matches == 0)
			{
				PlaySound(gameOverSound);
				gameTime.GetComponent<timeScript>().endGame();
			}
		}
		else
		{
			score -= 2;
			UpdateScoreText();
			SaveGame();
			PlaySound(mismatchSound);
		}


		for (int i = 0; i < c.Count; i++)
			{
				cards[c[i]].GetComponent<cardScript>().state = x;
				cards[c[i]].GetComponent<cardScript>().falseCheck();
			}
	
	}

	 public void PlayFlipSound()
    {
		if (_init)
        PlaySound(flipSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
	
	void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
	void SaveGame()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Matches", _matches);
        PlayerPrefs.Save();
    }

	void LoadGame()
	{
		UpdateScoreText();
		if (PlayerPrefs.HasKey("Score"))
		{
			score = PlayerPrefs.GetInt("Score");
			_matches = PlayerPrefs.GetInt("Matches", 4);
		}
    }

    void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }

	public void reGame()
	{
		SceneManager.LoadScene("gameScene");
	}

	public void reMenu(){
		SceneManager.LoadScene ("menuScene");
	}
}
