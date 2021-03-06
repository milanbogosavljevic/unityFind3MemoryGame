﻿using System;
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
    
    // polja koja se kontrolisu iz editora
    [SerializeField] private bool switchCardsPositionsFeatureIsActive;
    [SerializeField] private int numberOfSelectedCardsToActivateSwitchPosition;
    [SerializeField] private bool cardsMovingHorizontalIsActive;
    [SerializeField] private float horizontalMovingSpeed;
    [SerializeField] private bool scaleCardFeatureIsActive;
    [SerializeField] private int numberOfSelectedCardToActivateScaleCard;
    [SerializeField] private bool randomizeCardRotationOnStart;
    [SerializeField] private bool flipBackCardsFeatureIsActive;
    [SerializeField] private int numberOfSelectedCardToActivateFlipBackCard;
    
    private const int _NUMBER_OF_CARDS_THAT_CAN_BE_SELECTED = 3;
    private const int _NUMBER_OF_CARDS_TO_MATCH = 3;
    
    private readonly List<Card> _selectedCards = new List<Card>();
    private List<Card> _unmatchedCards = new List<Card>();
    private List<Card> _matchedCards = new List<Card>();
    private int _numberOfCardsMatched;
    private int _numberOfCards;
    private int _flipBackCardsCounter;
    private int _cardSelectCounter;
    private int _scaleCardFeatureCounter;
    private int _flipBackCardsFeatureCounter;
    private int[] _bestTime = {1000,1000};
    private bool _levelPassed;
    private bool _shouldCheckFlipBackCardFeature;

    private SoundController _soundController;

    [HideInInspector] public bool canSelectCards;

    void Start()
    {
        _numberOfCardsMatched = 0;
        _flipBackCardsCounter = 0;
        _cardSelectCounter = 0;
        _scaleCardFeatureCounter = 0;
        _flipBackCardsFeatureCounter = 0;
        _numberOfCards = allCards.transform.childCount;
        _setBestTime();
        RearrangeCardsPositions();
        SetIconsAlpha();
        timer.ActivateTimer(true);
        canSelectCards = true;
        _activateCardsHorizontalMovement(cardsMovingHorizontalIsActive, horizontalMovingSpeed);
        
        _soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();
    }

    private void SetIconsAlpha()
    {
        GameObject icons = GameObject.Find("Icons");
        Color disabledColor = new Color(1f,1f,1f,0.2f);
        Color enabledColor = new Color(0.285f,0.933f,0.207f,1f);

        Image turnBackIcon = icons.transform.Find("turnBackIcon").GetComponent<Image>();
        Image rotationIcon = icons.transform.Find("rotationIcon").GetComponent<Image>();
        Image movingIcon = icons.transform.Find("movingIcon").GetComponent<Image>();
        Image switchPositionIcon = icons.transform.Find("switchPositionIcon").GetComponent<Image>();
        Image scaleIcon = icons.transform.Find("scaleIcon").GetComponent<Image>();

        turnBackIcon.color = flipBackCardsFeatureIsActive ? enabledColor : disabledColor;
        rotationIcon.color = randomizeCardRotationOnStart ? enabledColor : disabledColor;
        movingIcon.color = cardsMovingHorizontalIsActive ? enabledColor : disabledColor;
        switchPositionIcon.color = switchCardsPositionsFeatureIsActive ? enabledColor : disabledColor;
        scaleIcon.color = scaleCardFeatureIsActive ? enabledColor : disabledColor;
    }

    private void RearrangeCardsPositions()
    {
        float[] zRotations = {0f,90f,-90f};
        foreach (Transform card in allCards.transform)
        {
            _unmatchedCards.Add(card.GetComponent<Card>());
            Vector3 cardPosition = card.position;
            int randomIndex = Random.Range(0, allCards.transform.childCount);
            Transform randomChild = allCards.transform.GetChild(randomIndex);
            card.position = randomChild.position;
            randomChild.position = cardPosition;
            if (scaleCardFeatureIsActive)
            {
                randomChild.GetComponent<Card>().SetCardScale("up");
                card.GetComponent<Card>().SetCardScale("down");
            }

            if (randomizeCardRotationOnStart)
            {
                int randomZIndex = Random.Range(0, zRotations.Length);
                float randomZRotation = zRotations[randomZIndex];
                card.GetComponent<Card>().SetZRotation(randomZRotation);
            }
        }
    }

    private void _levelIsPassed()
    {
        _soundController.PlayLevelPassedSound();
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

        return counter == _NUMBER_OF_CARDS_TO_MATCH;
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

    private void _switchCardsPosition()
    {
        if (_unmatchedCards.Count > _NUMBER_OF_CARDS_TO_MATCH)
        {
            int randomIndex1 = Random.Range(0, _unmatchedCards.Count);
            int randomIndex2 = Random.Range(0, _unmatchedCards.Count);
            while (randomIndex2 == randomIndex1)
            {
                randomIndex2 = Random.Range(0, _unmatchedCards.Count);
            }
            Card card1 = _unmatchedCards[randomIndex1];
            Card card2 = _unmatchedCards[randomIndex2];
            card2.MoveToPosition(card1.transform.position);
            card1.MoveToPosition(card2.transform.position);
        }
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
        bool hasNewBestTime = false;
        if (_bestTime[0] > time[0])
        {
            hasNewBestTime = true;
        }else if (_bestTime[0] == time[0])
        {
            if (_bestTime[1] > time[1])
            {
                hasNewBestTime = true;
            }
        }

        if (!hasNewBestTime) return;
        
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
    
    public void CardIsSelected(Card selectedCard)
    {
        _shouldCheckFlipBackCardFeature = true;
        _soundController.PlaySelectCardSound();
        _selectedCards.Add(selectedCard);
         if (_selectedCards.Count == _NUMBER_OF_CARDS_THAT_CAN_BE_SELECTED)
         {
             if (_cardsAreMatched())
             {
                 _shouldCheckFlipBackCardFeature = false;
                 int valueToRemove = _selectedCards[0].cardValue;
                 _filterUnmatchedCards(valueToRemove);
                 _matchedCards.AddRange(_selectedCards);
                 _selectedCards.Clear();
                 _numberOfCardsMatched += _NUMBER_OF_CARDS_TO_MATCH;
                 if (_numberOfCardsMatched == _numberOfCards)
                 {
                     _levelPassed = true;
                     _levelIsPassed();
                 }
             }
             else
             {
                 canSelectCards = false;
                 StartCoroutine(_flipBackSelectedCards());
             }
         }
         
         print(_shouldCheckFlipBackCardFeature);

         if (!_levelPassed)
         {
              _checkSwitchFeature();
              _checkScaleFeature();
              if (_shouldCheckFlipBackCardFeature)
              {
                  _checkFlipBackFeature();
              }
         }
    }

    private void _checkFlipBackFeature()
    {
        if (flipBackCardsFeatureIsActive)
        {
            _flipBackCardsFeatureCounter++;
            if (_flipBackCardsFeatureCounter == numberOfSelectedCardToActivateFlipBackCard)
            {
                _flipBackCardsFeatureCounter = 0;
                _flipBackMatchedCards();
            }
        }
    }
    
    private void _flipBackMatchedCards()
    {
        if (_matchedCards.Count > 2)
        {
            int randomIndex = Random.Range(0, _matchedCards.Count);
            int randomCardValue = _matchedCards[randomIndex].cardValue;
            int numberOfCards = _matchedCards.Count - 1;
            _numberOfCardsMatched -= _NUMBER_OF_CARDS_TO_MATCH;
            for (int i = numberOfCards; i >= 0; i--)
            {
                if (_matchedCards[i].cardValue == randomCardValue)
                {
                    _matchedCards[i].FlipBackCard();
                    _unmatchedCards.Add(_matchedCards[i]);
                    _matchedCards.Remove(_matchedCards[i]);
                }
            }
        }
    }

    private void _checkSwitchFeature()
    {
        if (switchCardsPositionsFeatureIsActive)
        {
            _cardSelectCounter++;
            if (_cardSelectCounter == numberOfSelectedCardsToActivateSwitchPosition)
            {
                _cardSelectCounter = 0;
                _switchCardsPosition();
            }
        }
    }

    private void _checkScaleFeature()
    {
        if (scaleCardFeatureIsActive)
        {
            _scaleCardFeatureCounter++;
            if (_scaleCardFeatureCounter == numberOfSelectedCardToActivateScaleCard)
            {
                _scaleCardFeatureCounter = 0;
                _activateScaleCardAnimation();
            }
        }
    }

    private void _filterUnmatchedCards(int valueToRemove)
    {
        int numberOfCards = _unmatchedCards.Count - 1;
        for (int i = numberOfCards; i >= 0; i--)
        {
            if (_unmatchedCards[i].cardValue == valueToRemove)
            {
                _unmatchedCards.Remove(_unmatchedCards[i]);
            }
        }
    }

    private void _activateCardsHorizontalMovement(bool activate, float speed)
    {
        foreach (Transform card in allCards.transform)
        {
            card.GetComponent<Card>().StartMovingHorizontal(activate, speed);
        }
    }

    private void _activateScaleCardAnimation()
    {
        if (_unmatchedCards.Count > _NUMBER_OF_CARDS_TO_MATCH)
        {
            int randomIndex1 = Random.Range(0, _unmatchedCards.Count);
            int randomIndex2 = Random.Range(0, _unmatchedCards.Count);
            while (randomIndex2 == randomIndex1)
            {
                randomIndex2 = Random.Range(0, _unmatchedCards.Count);
            }
            Card card1 = _unmatchedCards[randomIndex1];
            Card card2 = _unmatchedCards[randomIndex2];
            card2.ActivateScaleAnimation(true);
            card1.ActivateScaleAnimation(true);
        }
    }
    
    public void CountCardsThatFlipsBack()
    {
        _flipBackCardsCounter++;
        if (_flipBackCardsCounter == _NUMBER_OF_CARDS_THAT_CAN_BE_SELECTED)
        {
            canSelectCards = true;
            _flipBackCardsCounter = 0;
        }
    }
}

/* dodavanje nivoa
    copy svih komponenti
    u gamecontroller componenti u editoru promeniti Level Number
    dodavanje na build settings listu
    dodavanje dugmeta na display listu + dodavanje dugmeta u Level Buttons listu u editoru + promena Level Number parametra na dugmetu u editoru + dodavanje gold/silver times
 */
