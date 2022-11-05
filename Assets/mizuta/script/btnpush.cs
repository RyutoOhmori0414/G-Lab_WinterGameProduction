using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnpush : MonoBehaviour
{
    [SerializeField]
    string gotoscene = "";
    // Start is called before the first frame update
   public void chgscene()
    {
        SceneManager.LoadScene(gotoscene);
    }
}
