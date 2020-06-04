using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Card> allCards;
    private List<Card> _selectedCards = new List<Card>();
    private int _numberOfCardThatCanBeSelected = 3;
    private int _numberOfCardsToMatch = 3;
    private int _numberOfCardsMatched = 0;

    public void CardIsSelected(Card selectedCard)
    {
        _selectedCards.Add(selectedCard);
        if (_selectedCards.Count == _numberOfCardThatCanBeSelected)
        {
            if (_cardsAreMatched())
            {
                _numberOfCardsMatched += _numberOfCardsToMatch;
                _selectedCards.Clear();
            }
            else
            {
                StartCoroutine(_flipBackSelectedCards());
            }
        }
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
        yield return new WaitForSeconds(2);
        foreach (Card card in _selectedCards)
        {
            card.FlipBackCard();
        }
        _selectedCards.Clear();
    }
}
