using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource trainStartSound;
    [SerializeField] private AudioSource trainRunSound;
    [SerializeField] private Transform trainPos;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject imageFade;
    public void StartGame()
    {
        StartCoroutine(StartGameAnim());
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartGameAnim()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        trainStartSound.Play();
        
        trainRunSound.Play();
        trainRunSound.volume = 0f;

        float duration = 8f;
        float elapsedTime = 0f;
        float maxVolume = 1f;

        float moveSpeed = 0f;

        while (elapsedTime < duration)
        {
            trainRunSound.volume = Mathf.Lerp(0f, maxVolume, elapsedTime / duration);

            moveSpeed += Time.deltaTime * 0.1f;
            trainPos.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        trainRunSound.volume = maxVolume;
        
        imageFade.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
