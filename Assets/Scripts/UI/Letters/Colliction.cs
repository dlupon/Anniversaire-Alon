using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnBocal.TweeningSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Colliction : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Letters
    [Header("Letter")]
    [SerializeField] private Transform _letterContainer;
    private List<Letter> _letters = new List<Letter>();
    private bool _inputReactif = false;

    // -------~~~~~~~~~~================# // Buttons
    [Header("Buttons")]
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Button ButtonFactory;
    private List<TextMeshProUGUI> _buttonsText = new List<TextMeshProUGUI>();

    // -------~~~~~~~~~~================# // Animations
    [Header("Animations")]
    [SerializeField] private Image _background;
    private Tween _animator = new Tween();

    private void Start()
    {
        _letterContainer.GetComponentsInChildren(_letters);

        foreach (Letter lLetter in _letters) CreateButtonAndHide(lLetter);

        Hide();
    }

    private void CreateButtonAndHide(Letter pLetter)
    {
        Button lNewButton = Instantiate(ButtonFactory, _buttonContainer);
        TextMeshProUGUI lButtonText = lNewButton.transform.GetComponentInChildren<TextMeshProUGUI>();

        lNewButton.name = pLetter.name;
        lNewButton.onClick.AddListener(pLetter.Show);

        if (_letters.IndexOf(pLetter) < PlayerPrefs.GetInt(nameof(Heart), 0))
            lButtonText.text = pLetter.name;
        else
        {
            lButtonText.text = "???";
            lNewButton.enabled = false;
        }
        _buttonsText.Add(lButtonText);

        pLetter.gameObject.SetActive(false);
    }
    public void Show()
    {
        _inputReactif = false;
        _animator.CompleteAndClear();
        _animator.Color(_background, new Color(0, 0, 0, 0), Color.black, .25f);

        float lDelay;
        float lDuration;
        foreach (TextMeshProUGUI pText in _buttonsText)
        {
            lDelay = .5f + 1f * ((float)_buttonsText.IndexOf(pText) / (float)_buttonsText.Count);
            lDuration = .5f;


            _animator.Color(pText, new Color(0, 0, 0, 0), pText.color, lDuration, pDelay : lDelay);
            _animator.Scale(pText.rectTransform, 0, 1, lDuration, EaseType.OutBack, lDelay);

            pText.color = new Color(0, 0, 0, 0);
        }

        _animator.Start();

        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
