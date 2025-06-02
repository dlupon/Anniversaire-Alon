using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Colliction : MonoBehaviour
{
    [SerializeField] private Transform _letterContainer;
    private List<Letter> _letters = new List<Letter>();

    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Button ButtonFactory;

    private void Start()
    {
        _letterContainer.GetComponentsInChildren(_letters);

        foreach (Letter lLetter in _letters) CreateButtonAndHide(lLetter);

        Hide();
    }

    private void CreateButtonAndHide(Letter pLetter)
    {
        Button lNewButton = Instantiate(ButtonFactory, _buttonContainer);
        lNewButton.name = pLetter.name;
        lNewButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = pLetter.name;

        lNewButton.onClick.AddListener(pLetter.Show);

        pLetter.gameObject.SetActive(false);
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
}
