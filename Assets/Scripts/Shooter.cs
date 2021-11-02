using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetSprite;

    private void Start()
    {
        targetSprite.enabled = false;
    }
}
