using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private float _yRotation;
    private float _flipAnimationSpeed;
    private bool _cardIsFliped;
    private bool _cardIsFlipedBack;
    private bool _cardIsClicked;
    private bool _playAnimation;
    private bool _scaleCard;
    private bool _moveCardToPosition;
    private SpriteRenderer _cardImage;
    private Vector3 _targetPosition;
    private float _movingSpeed;
    
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite cardFront;
    [SerializeField] private GameController gameController;
    [SerializeField] public int cardValue;
    

    void Start()
    {
        _cardIsFliped = false;
        _cardIsClicked = false;
        _playAnimation = false;
        _scaleCard = false;
        _cardIsFlipedBack = false;
        _moveCardToPosition = false;
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        _yRotation = 0f;
        _flipAnimationSpeed = 5f;
        _movingSpeed = 3.0f;
    }
    
    void Update()
    {
        if (_playAnimation)
        {
            _flipCard();
        }

        if (_moveCardToPosition)
        {
            float step =  _movingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
            if (Vector3.Distance(transform.position, _targetPosition) < 0.001f)
            {
                _moveCardToPosition = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (gameController.canSelectCards)
        {
            if (!_cardIsClicked)
            {
                _cardIsClicked = true;
                _startAnimation();
                gameController.CardIsSelected(this);
            }
        }

    }

    private void _startAnimation()
    {
        _scaleCard = true;
        _playAnimation = true;
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
            if (_cardIsFlipedBack)
            {
                _cardIsClicked = false;
                _cardIsFlipedBack = false;
                _cardIsFliped = false;
                gameController.CountCardsThatFlipsBack();
            }
        }
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    public void FlipBackCard()
    {
        _cardIsFlipedBack = true;
        _startAnimation();
    }

    public int GetCardValue()
    {
        return cardValue;
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _moveCardToPosition = true;
    }
}
