using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject allCards;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Timer timer;
    [SerializeField] private int levelNumber;
    
    private readonly List<Card> _selectedCards = new List<Card>();
    private int _numberOfCardThatCanBeSelected;
    private int _numberOfCardsToMatch;
    private int _numberOfCardsMatched;
    private int _numberOfCards;
    private int _flipBackCardsCounter;
    private int[] _bestTime = {1000,1000};

    public bool canSelectCards;

    void Start()
    {
        _numberOfCardThatCanBeSelected = 3;
        _numberOfCardsToMatch = 3;
        _numberOfCardsMatched = 0;
        _flipBackCardsCounter = 0;
        _numberOfCards = allCards.transform.childCount;
        _setBestTime();
        RearrangeCardsPositions();
        timer.ActivateTimer(true);
        canSelectCards = true;
    }

    private void RearrangeCardsPositions()
    {
        foreach (Transform card in allCards.transform)
        {
            Vector3 cardPosition = card.position;
            int randomIndex = Random.Range(0, allCards.transform.childCount);
            Transform randomChild = allCards.transform.GetChild(randomIndex);
            card.position = randomChild.position;
            randomChild.position = cardPosition;
        }
    }

    private void _levelIsPassed()
    {
        timer.ActivateTimer(false);
        int[] time = timer.GetTime();
        _checkBestTime(time);
    }

    private bool _cardsAreMatched()
    {
        int valueToCheck = _selectedCards[0].GetCardValue();
        int counter = 1;
        for (int i = 1; i < _selectedCards.Count; i++)
        {
            if (valueToCheck == _selectedCards[i].GetCardValue())
            {
                counter++;
            }
        }

        return counter == _numberOfCardsToMatch;
    }

    private IEnumerator _flipBackSelectedCards()
    {
        yield return new WaitForSeconds(1);
        foreach (Card card in _selectedCards)
        {
            card.FlipBackCard();
        }
        _selectedCards.Clear();
    }
    


    private void _setBestTime()
    {
        string minutesKey = "level" + levelNumber + "Minutes";
        string secondsKey = "level" + levelNumber + "Seconds";
        if (PlayerPrefs.HasKey(minutesKey))
        {
            _bestTime[0] = PlayerPrefs.GetInt(minutesKey);
            _bestTime[1] = PlayerPrefs.GetInt(secondsKey);
            string minutes = _bestTime[0] < 10 ? "0" + _bestTime[0].ToString() : _bestTime[0].ToString();
            string seconds = _bestTime[1] < 10 ? "0" + _bestTime[1].ToString() : _bestTime[1].ToString();
            bestTimeText.text = minutes + ":" + seconds;
        }
    }

    private void _checkBestTime(int[] time)
    {
        if (_bestTime[0] >= time[0])
        {
            if (_bestTime[1] > time[1])
            {
                string minutesKey = "level" + levelNumber + "Minutes";
                string secondsKey = "level" + levelNumber + "Seconds";
                string minutes = time[0] < 10 ? "0" + time[0].ToString() : time[0].ToString();
                string seconds = time[1] < 10 ? "0" + time[1].ToString() : time[1].ToString();
                bestTimeText.text = minutes + ":" + seconds;
                _bestTime[0] = time[0];
                _bestTime[1] = time[1];
                PlayerPrefs.SetInt(minutesKey, time[0]);
                PlayerPrefs.SetInt(secondsKey, time[1]);
            }
        }
    }   
    
    public void CardIsSelected(Card selectedCard)
         {
             _selectedCards.Add(selectedCard);
             if (_selectedCards.Count == _numberOfCardThatCanBeSelected)
             {
                 if (_cardsAreMatched())
                 {
                     _selectedCards.Clear();
                     _numberOfCardsMatched += _numberOfCardsToMatch;
                     if (_numberOfCardsMatched == _numberOfCards)
                     {
                         _levelIsPassed();
                     }
                 }
                 else
                 {
                     canSelectCards = false;
                     StartCoroutine(_flipBackSelectedCards());
                 }
             }
         }
    
    public void CountCardsThatFlipsBack()
    {
        _flipBackCardsCounter++;
        if (_flipBackCardsCounter == _numberOfCardThatCanBeSelected)
        {
            canSelectCards = true;
            _flipBackCardsCounter = 0;
        }
    }
}
