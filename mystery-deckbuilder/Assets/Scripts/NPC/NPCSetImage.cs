using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSetImage : MonoBehaviour
{
    [SerializeField] private Sprite _streetViewStationary;
    [SerializeField] private Sprite _streetViewWave;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetStreetViewStationary();
    }

    public void SetStreetViewStationary()
    {
        _spriteRenderer.sprite = _streetViewStationary;
    }

    public void SetStreetViewWave()
    {
        _spriteRenderer.sprite = _streetViewWave;
    }
}
