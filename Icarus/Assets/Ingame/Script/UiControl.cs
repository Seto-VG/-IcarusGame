using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : SingletonBehavior<UiControl>
{
    [Tooltip("表示したいキャンバス")]
    [SerializeField]
    private GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasDisplay()
    {
        _canvas.SetActive(true);
    }

    public void CanvasHidden()
    {
        _canvas.SetActive(false);
    }
}
