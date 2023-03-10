using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    //All _slides in the project, minus the _fadeSlide
    [SerializeField] private List<Transform> _slides = default;

    // String - exact name of scene to load
    [Header("Scene index (found in File > Build Settings > Scenes in Build)")]
    [Header("Scene has to be in the game build to work. Will implement loading by scene name later")]
    [SerializeField] private int _sceneIndex;

    //The panel that overlays all _slides and changes from clear to black
    // [SerializeField]
    private Image _fadeSlide = default;

    [Header("Fade Duration (Not working yet)")]
    [SerializeField] private float _fadeDuration = 0.75f;
    [Header("Slide duration (Only works for all slides right now)")]
    [SerializeField] private float _slideDuration = 5f;
    
    // Currently viewed slide
    private int _currentSlide = -1;

    // Whether the fade slide is currently fading
    private IEnumerator Start()
    {
        // Set our fade to black slide to black so that the audience can not see the first slide
        // _fadeSlide.color = Color.black;
        for (int i = 0; i < _slides.Count; i++) {
            _currentSlide++;
            _currentSlide = _currentSlide % _slides.Count;

            // Transition to the next slide
            StartCoroutine(SlideTransition());
            yield return new WaitForSeconds(_slideDuration);
        }

        SceneManager.LoadScene(_sceneIndex);
    }

    private IEnumerator SlideTransition()
    {
        // Fade to black
        yield return StartCoroutine(FadeToTargetColor(targetColor: Color.black));

        // Set only our current slide active - and all others inactive
        _slides.ForEach(slide => slide.gameObject.SetActive(_slides.IndexOf(slide) == _currentSlide));

        // Fade to clear
        yield return StartCoroutine(FadeToTargetColor(targetColor: Color.clear));
    }

    private IEnumerator FadeToTargetColor(Color targetColor)
    {
        // The total amount of seconds that has elapsed since the start of our lerp sequence
        float elapsedTime = 0.0f;

        // The color of our fade panel at the start of the lerp sequence
        // Color startColor = _fadeSlide.color;

        // While we haven't reached the end of the lerp sequence..
        while (elapsedTime < _fadeDuration)
        {
            // Increase our elapsed time
            elapsedTime += Time.deltaTime;

            // Perform a lerp to our target color
            // _fadeSlide.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeDuration);

            // Wait for the next frame
            yield return null;
        }
    }
}