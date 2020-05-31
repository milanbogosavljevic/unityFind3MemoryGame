using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private float _yRotation;
    private float _flipAnimationSpeed;
    private bool _cardIsFliped;
    private bool _cardIsClicked;
    private bool _playAnimation;
    private bool _scaleCard;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite cardFront;
    [SerializeField] private GameController gameController;
    private SpriteRenderer _cardImage;

    public Card refference;
    void Start()
    {
        _cardIsFliped = false;
        _cardIsClicked = false;
        _playAnimation = false;
        _scaleCard = false;
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        _yRotation = 0;
        _flipAnimationSpeed = 3f;

        refference = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playAnimation)
        {
            _flipCard();
        }
    }

    private void OnMouseDown()
    {
        if (!_cardIsClicked)
        {
            _cardIsClicked = true;
            _startAnimation();
            //gameController.CardIsSelected(refference);
            gameController.CardIsSelected(gameObject);
            //StartCoroutine(ExampleCoroutine());
        }
    }

    private void _startAnimation()
    {
        _scaleCard = true;
        _playAnimation = true;
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);
        _startAnimation();
    }

    private void _flipCard()
    {
        _yRotation = _scaleCard ? _yRotation + _flipAnimationSpeed : _yRotation - _flipAnimationSpeed;
        if (_yRotation > 90f)
        {
            _yRotation = 90f;
            _cardImage.sprite = _cardIsFliped ? cardBack : cardFront;
            _scaleCard = false;
        }

        if (_yRotation < 0f)
        {
            _yRotation = 0f;
            _scaleCard = true;
            _playAnimation = false;
            _cardIsFliped = true;
        }
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
