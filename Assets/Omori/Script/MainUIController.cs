using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    [Tooltip("カウントダウン用のテキスト"), SerializeField]
    Text[] _countText;
    [SerializeField]
    Text[] _subCountText;

    [Header("赤のUI"), SerializeField]
    GameObject _winRedPanel;
    [SerializeField]
    GameObject _loseRedPanel;
    [SerializeField]
    GameObject _drawRedPanel;
    [Header("緑のUI"), SerializeField]
    GameObject _winGreenPanel;
    [SerializeField]
    GameObject _loseGreenPanel;
    [SerializeField]
    GameObject _drawGreenPanel;

    [Header("暗転用のパネル"), SerializeField]
    UnityEngine.UI.Image[] _fadePanels;

    public void CountTextUpdate(float count)
    {
        foreach(var n in _countText)
        {
            n.text = count.ToString("0");
        }
    }

    public void CountEnd()
    {
        foreach(var n in _countText)
        {
            n.text = "START!";
            StartCoroutine(CountHide(n));
        }
    }

    public void SubCountTextUpdate(float count)
    {
        var temp = Mathf.Floor(count);
        var temp2 = $"{Mathf.Floor(temp / 60).ToString("00")}:{(temp % 60).ToString("00")}";

        if (_subCountText[0].text != temp2)
        {
            foreach (var n in _subCountText)
            {
                n.text = temp2;
            }
        }
    }

    public void SubCountEnd()
    {
        foreach (var n in _subCountText)
        {
            n.text = "";
        }
    }
    public void EndGame(GameController.Team winTeam)
    {
        if (winTeam == GameController.Team.ATeam)
        {
            _winRedPanel.SetActive(true);
            _loseGreenPanel.SetActive(true);
        }
        else
        {
            _winGreenPanel.SetActive(true);
            _loseRedPanel.SetActive(true);
        }
    }

    public void DrawGame()
    {
        _drawRedPanel.SetActive(true);
        _drawGreenPanel.SetActive(true);
    }

    public void ToTitle()
    {
        foreach(var n in _fadePanels)
        {
            n.gameObject.SetActive(true);
            n.DOFade(1f, 1f).OnComplete(() =>
            {
                SceneManager.LoadScene("maintitle");
                SingletonBGMController.instance.ToTitle();
            });
        }
    }

    IEnumerator CountHide(Text text)
    {
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }
}
