using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : SingletonBehavior<UiControl>
{
    [SerializeField]
    private GameObject _guidCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuidDisplay()
    {
        _guidCanvas.SetActive(true);
    }

    public void GuidHidden()
    {
        _guidCanvas.SetActive(false);
    }
}
