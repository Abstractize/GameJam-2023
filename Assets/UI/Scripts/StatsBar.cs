using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;

public class StatsBar : MonoBehaviour
{
    [Header("Stats Properties")]
    [SerializeField] private Slider _hunger;
    [SerializeField] private Slider _fun;
    [SerializeField] private Slider _hygiene;
    [SerializeField] private Slider _sleep;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _level;

    [Header("Action Button Properties")]
    [SerializeField] private Button _actionButton;
    [SerializeField] private TMP_Text _actionText;

    [Header("Player Properties")]
    [SerializeField] private PlayerController _controller;

    private void LateUpdate()
    {
        _hunger.value = _controller.Stats.Hunger.Value;
        _fun.value = _controller.Stats.Fun.Value;
        _hygiene.value = _controller.Stats.Hygiene.Value;
        _sleep.value = _controller.Stats.Sleep.Value;

        _coins.text = _controller.Wallet.Money.ToString();
        _level.text = _controller.Player.Level.ToString();

        _actionButton.gameObject.SetActive(_controller.Action != null);
        _actionButton.enabled = _controller.Action != null;
        _actionText.text = _controller.Action?.Name;
    }
}
