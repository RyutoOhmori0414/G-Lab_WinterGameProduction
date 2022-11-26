using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnpush : MonoBehaviour
{
    [SerializeField]
    GameObject FadeOutScreen;

    [SerializeField]
    string gotoscene = "";

    [SerializeField]
    int fadeouttime = 120; //フェードアウトアニメーションのフレーム秒数
    // Start is called before the first frame update
   public void chgscene()
    {
        StartCoroutine(FadeOutCall());
    }

    IEnumerator FadeOutCall()
    {
        FadeOutScreen.GetComponent<Image>().enabled = true;
        while (FadeOutScreen.GetComponent<Image>().color.a < 1.0f)
        {
            FadeOutScreen.GetComponent<Image>().color += new Color(0, 0, 0, 1.0f / fadeouttime); //Imageのカラーを変更
            yield return null;
        }
        SceneManager.LoadScene(gotoscene);
        yield break;
    }
}
