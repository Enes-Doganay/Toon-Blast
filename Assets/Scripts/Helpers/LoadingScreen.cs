using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    public void SetProgress(float progress)
    {
        loadingBar.value = progress;
    }
}
