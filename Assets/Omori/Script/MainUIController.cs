using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [Tooltip("カウントダウン用のテキスト"), SerializeField]
    Text[] _countText;

    public void CountTextUpdate(float count)
    {
        foreach(var n in _countText)
        {
            n.text = count.ToString("0.00");
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

    IEnumerator CountHide(Text text)
    {
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }
}
