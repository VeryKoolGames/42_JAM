using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFade : MonoBehaviour
{
    [SerializeField] private GameObject fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayFade());
    }

    // Update is called once per frame
    IEnumerator delayFade()
    {
        yield return new WaitForSeconds(1.8f);
    }
}
