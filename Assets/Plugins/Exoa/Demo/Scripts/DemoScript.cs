using Exoa.TutorialEngine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{

    public Button playBtn;
    public Button action1Btn;
    public Button action2Btn;
    public Button startTutorialBtn;
    public string startTutorialFile = "1.1";
    public string playTutorialFile = "1.2";
    private void OnDestroy()
    {
        TutorialEvents.OnTutorialComplete -= OnTutorialCompleted;
    }
    void Start()
    {
        if (playBtn != null) playBtn.onClick.AddListener(OnClickPlay);
        if (startTutorialBtn != null) startTutorialBtn.onClick.AddListener(OnClickStartTutorial);
        if (playBtn != null) playBtn.gameObject.SetActive(false);
        if (action1Btn != null) action1Btn.gameObject.SetActive(false);
        if (action2Btn != null) action2Btn.gameObject.SetActive(false);

    }

    void OnClickStartTutorial()
    {
        TutorialController.IsSkippable = false;
        TutorialLoader.instance.Load(startTutorialFile);

        if (startTutorialBtn != null) startTutorialBtn.gameObject.SetActive(false);
        if (playBtn != null) playBtn.gameObject.SetActive(true);
        if (action1Btn != null) action1Btn.gameObject.SetActive(true);
        if (action2Btn != null) action2Btn.gameObject.SetActive(true);
    }

    void OnClickPlay()
    {
        TutorialController.IsSkippable = true;
        TutorialLoader.instance.Load(playTutorialFile, true);
        TutorialEvents.OnTutorialComplete += OnTutorialCompleted;

        if (playBtn != null) playBtn.gameObject.SetActive(false);


    }

    private void OnTutorialCompleted()
    {
        TutorialEvents.OnTutorialComplete -= OnTutorialCompleted;
        if (startTutorialBtn != null) startTutorialBtn.gameObject.SetActive(true);
        if (playBtn != null) playBtn.gameObject.SetActive(true);
    }
    public bool clickAnywhereToGONext = true;
    private void Update()
    {
        if (clickAnywhereToGONext &&
            TutorialController.IsTutorialActive &&
            Input.GetMouseButtonDown(0))
        {
            TutorialController.instance.Next();
        }
    }
}
