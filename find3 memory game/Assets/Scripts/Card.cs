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
    private bool _rotateCard;
    private bool _moveCardToPosition;
    private bool _moveCardHorizontal;
    private bool _scaleCard;
    private string _currentScale;
    private SpriteRenderer _cardImage;
    private Vector3 _targetPosition;
    private float _movingSpeed;
    private float _horizontalMovingSpeed;
    private Vector3 _originalPosition;//todo ako nekad budem hteo da se zaustavi karta da zna gde da se vrati
    private float _upScale;
    private float _downScale;
    
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite cardFront;
    [SerializeField] private GameController gameController;
    [SerializeField] public int cardValue;


    void Start()
    {
        _cardIsFliped = false;
        _cardIsClicked = false;
        _playAnimation = false;
        _rotateCard = false;
        _cardIsFlipedBack = false;
        _moveCardToPosition = false;
        _moveCardHorizontal = false;
        _scaleCard = false;
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        _yRotation = 0f;
        _flipAnimationSpeed = 5f;
        _movingSpeed = 3.0f;
        _upScale = 0.7f;
        _downScale = 0.3f;
        _originalPosition = transform.position;
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

        if (_moveCardHorizontal)
        {
            if (transform.position.x > 3.35f)
            {
                transform.position = new Vector3(-3.35f, transform.position.y, transform.position.z);    
            }
            transform.position += Vector3.right * (Time.deltaTime * _horizontalMovingSpeed);
        }

        if (_scaleCard)
        {
            ScaleCard();
        }
    }

    private void ScaleCard()
    {
        if (transform.localScale.x < _upScale && transform.localScale.x > _downScale)
        {
            Vector3 scaleValues = _currentScale == "up" ? new Vector3(0.01f, 0.01f, 0f) : new Vector3(-0.01f, -0.01f, 0f);
            transform.localScale += scaleValues;
        }
        else
        {
            ActivateScaleAnimation(false);

        }
    }

    public void ActivateScaleAnimation(bool activate)
    {
        if (activate)
        {
            _currentScale = _currentScale == "up" ? "down" : "up";
        }
        else
        {
            SetCardScale(_currentScale);
        }
        
        _scaleCard = activate;
    }

    public void SetCardScale(string upDown)
    {
        float scaleValue;
        if (upDown == "up")
        {
            scaleValue = _upScale - 0.01f;
        }
        else
        {
            scaleValue = _downScale + 0.01f;
        }
        transform.localScale = new Vector3(scaleValue, scaleValue, 0f);
        _currentScale = upDown;
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
        _rotateCard = true;
        _playAnimation = true;
    }

    private void _flipCard()
    {
        _yRotation = _rotateCard ? _yRotation + _flipAnimationSpeed : _yRotation - _flipAnimationSpeed;
        if (_yRotation > 90f)
        {
            _yRotation = 90f;
            _cardImage.sprite = _cardIsFliped ? cardBack : cardFront;
            _rotateCard = false;
        }

        if (_yRotation < 0f)
        {
            _yRotation = 0f;
            _rotateCard = true;
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

    public void StartMovingHorizontal(bool move, float speed)
    {
        _horizontalMovingSpeed = speed;
        _moveCardHorizontal = move;
    }
}
