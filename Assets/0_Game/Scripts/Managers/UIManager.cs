using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _movementCountText;
    [SerializeField] private GameObject _winPanelGO;
    [SerializeField] private GameObject _losePanelGO;
    private void OnEnable()
    {
        GameManager.Instance.OnNumberOfMovementChanged += OnNumberOfMovementChanged;
        GameManager.Instance.OnGameFinished += OnGameFinished;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNumberOfMovementChanged += OnNumberOfMovementChanged;
        GameManager.Instance.OnGameFinished += OnGameFinished;
    }

    private void OnGameFinished(bool state)
    {
        if (state)
        {
            _winPanelGO.SetActive(true);
        }
        else
        {
            _losePanelGO.SetActive(true);
        }
    }

    private void OnNumberOfMovementChanged(int movementCount)
    {
        _movementCountText.text = movementCount + " Moves";
    }

    public void OnNextLevelButtonClicked()
    {

    }
    public void OnRetryButtonClicked()
    {

    }
}
