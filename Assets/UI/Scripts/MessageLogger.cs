using System.Collections;
using TMPro;
using UnityEngine;

public class MessageLogger : MonoBehaviour
{
    [SerializeField] private float _waitTime = 3.0f;
    [SerializeField] private GameObject _logObject;
    [SerializeField] private TMP_Text _message;

    public void LogMessage(string message)
        => StartCoroutine(DisplayMessage(message));

    private IEnumerator DisplayMessage(string message)
    {
        _message.text = message;
        _logObject.SetActive(true);
        yield return new WaitForSeconds(_waitTime);
        _logObject.SetActive(false);
    }
}
