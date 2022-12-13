using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleUIController : MonoBehaviour
{
    [SerializeField]
    Image[] _fadePanel;
    [SerializeField]
    float _fadeDuration = 2f;
    [SerializeField]
    Button _toGameButton;

    public void FadeAndLoadScene(string sceneName)
    {
        foreach (var n in _fadePanel)
        {
            n.gameObject.SetActive(true);
            n.DOFade(1f, _fadeDuration).
                OnComplete(() =>
                {
                    SceneManager.LoadScene(sceneName);
                    if (sceneName == "TestMapScene")
                    {
                        SingletonBGMController.instance.ToGame();
                    }
                });
        }
    }
}
