using System.Collections.Generic;
using TMPro;
using UnBocal.TweeningSystem;
using UnityEngine;

public class LetterSpaming : Letter
{
    [SerializeField] private List<Transform> _stikerPrefabs;
    [SerializeField] private RectTransform _container;
    [SerializeField] private int _stickerCount = 100;
    [SerializeField] private TextMeshProUGUI _text;

    private List<Transform> _stickers = new List<Transform>();

    private Tween _animator = new Tween();

    public override void Show()
    {
        base.Show();

        _animator.CompleteAndClear();
        for (int lstickerCount = 0; lstickerCount < _stickerCount; lstickerCount++)
            AddSticker();

        _animator.Whrite(_text, 1f, pDelay: 1).Apply();
        _animator.Start();
    }

    private void AddSticker()
    {
        Transform lSticker = Instantiate(_stikerPrefabs[Random.Range(0, _stikerPrefabs.Count)], _container);
        lSticker.rotation = Quaternion.AngleAxis(45f * .5f - Random.value * 45, Vector3.forward);

        lSticker.position = _container.rect.size * new Vector2(Random.value, Random.value);

        _animator.Scale(lSticker, 0, 1, 1 + Random.value * .5f, EaseType.OutBack, Random.value).Apply();
        _stickers.Add(lSticker);
    }

    public override void Hide()
    {
        base.Hide();

        _animator.CompleteAndClear();
        foreach (Transform lTransform in _stickers)
            Destroy(lTransform.gameObject);

        _stickers.Clear();
    }
}