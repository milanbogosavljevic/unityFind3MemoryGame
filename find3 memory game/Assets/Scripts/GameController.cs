using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Card> allCards;
    private List<GameObject> _selectedCards;

    //public void CardIsSelected(Card selectedCard)
    public void CardIsSelected(GameObject selectedCard)
    {
        Debug.Log(selectedCard.GetComponent<Card>());
        //_selectedCards.Add(selectedCard);
        //Debug.Log(_selectedCards);
    }
}
