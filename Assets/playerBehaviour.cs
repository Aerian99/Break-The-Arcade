using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerBehaviour : MonoBehaviour
{
    private Animator animator;
    public static int _playerLifes;
    public TextMeshProUGUI lifes;
    public TextMeshProUGUI bullets;

    private float timer = 0.0f;
    private int seconds;

    // CURSOR
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        animator = GetComponent<Animator>();
        _playerLifes = 5;
    }

    void Update()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        if (_playerLifes <= 0)
        {
            Destroy(this.gameObject);
        }
        lifes.text = "Lifes:  " + _playerLifes;
        bullets.text = "Bullets:  " + PurpleShoot.bulletCounter;
    }
}
