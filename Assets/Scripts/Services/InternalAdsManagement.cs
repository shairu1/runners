using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InternalAdsManagement : MonoBehaviour
{
    private static InternalAdsManagement instance;

    [Header("UI")]
    [SerializeField] private GameObject _adsObject;
    [SerializeField] private TextMeshProUGUI _adsText;

    private UnityAction _adsAction;

    private void Awake() =>
        instance = this;
    
    public static void FullscreenAds()
    {
        instance._adsAction = AdsManagement.FullscreenAds;
        instance.StartCoroutine(instance.StartTimer());
    }

    public static void RewardAds()
    {
        instance._adsAction = AdsManagement.RewardAds;
        instance.StartCoroutine(instance.StartTimer());
    }

    private IEnumerator StartTimer()
    {
        _adsObject.SetActive(true);
        _adsText.text = "до рекламы 3 секунды";
        yield return new WaitForSeconds(1);
        _adsText.text = "до рекламы 2 секунды";
        yield return new WaitForSeconds(1);
        _adsText.text = "до рекламы 1 секунда";
        yield return new WaitForSeconds(1);
        _adsAction?.Invoke();
        _adsObject.SetActive(false);
        yield break;
    }
}
