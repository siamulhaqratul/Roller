using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image _fillMask;
    [SerializeField, Tooltip("Loading Time In Seconds"), Range(1f, 10f)] private float _loadingTime = 1.5f;

    private float _startTime;
    private bool _isLoading;



    #endregion

    #region Unity Methods

    private void Awake()
    {
        _startTime = 0f;
        _isLoading = true;
        _fillMask.fillAmount = 0f;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_startTime >= _loadingTime && _isLoading)
        {
            _isLoading = false;

        }
        else
        {
            _startTime += Time.deltaTime;
            _fillMask.fillAmount = _startTime / _loadingTime;




        }
    }


    #endregion
}
