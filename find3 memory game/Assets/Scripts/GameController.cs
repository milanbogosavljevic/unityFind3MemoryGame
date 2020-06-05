using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject allCards;
    private readonly List<Card> _selectedCards = new List<Card>();
    private int _numberOfCardThatCanBeSelected;
    private int _numberOfCardsToMatch;
    private int _numberOfCardsMatched;
    private int _numberOfCards;

    void Start()
    {
        _numberOfCardThatCanBeSelected = 3;
        _numberOfCardsToMatch = 3;
        _numberOfCardsMatched = 0;
        _numberOfCards = allCards.transform.childCount;
        RearrangeCardsPositions();
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
        Debug.Log("Level passed");
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
                StartCoroutine(_flipBackSelectedCards());
            }
        }
    }
}
