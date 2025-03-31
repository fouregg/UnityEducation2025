using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private GameObject panelMenu;
    private PlayerAnimationController playerAnimationController;
    private MovementController movementController;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private AudioMixer musicMixer;
    private bool inPause = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelMenu = GameObject.Find("Canvas");
        panelMenu = panelMenu.transform.GetChild(1).gameObject;
        playerAnimationController = GetComponent<PlayerAnimationController>();
        movementController = GetComponent<MovementController>();
        soundSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        soundMixer.SetFloat("SoundVolume", -5);
        musicMixer.SetFloat("MusicVolume", -5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            inPause = !inPause;
            panelMenu.SetActive(inPause);
            movementController.Playing = !inPause;
            playerAnimationController.Playing = !inPause;
        }
    }

    public void onClickBack()
    {
        inPause = false;
        panelMenu.SetActive(false);
        movementController.Playing = true;
        playerAnimationController.Playing = true;
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    public void onChangeSoundSlider()
    {
        float value = soundSlider.value;
        if (value == 0)
            soundMixer.SetFloat("SoundVolume", -80);
        else
            soundMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }

    public void onChangeMusicSlider()
    {
        float value = musicSlider.value;
        if (value == 0)
            musicMixer.SetFloat("MusicVolume", -80);
        else
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
}
