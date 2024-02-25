using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Services
{
    public class InternalAdsManagement : MonoBehaviour
    {
        private static InternalAdsManagement instance;

        [Header("UI")] [SerializeField] private GameObject _adsObject;
        [SerializeField] private TextMeshProUGUI _adsText;


        private float _timerForAd;
        private UnityAction _adsAction;

        private string _adsBefore3;
        private string _adsBefore2;
        private string _adsBefore1;

        private void OnEnable() =>
            YandexGame.SwitchLangEvent += ChangeText;

        private void OnDisable() =>
            YandexGame.SwitchLangEvent -= ChangeText;

        private void Awake() =>
            instance = this;

        private void Update() =>
            instance._timerForAd += Time.deltaTime;

        private void ChangeText(string language)
        {
            switch (language)
            {
                case "ru":
                    _adsBefore3 = "до показа рекламы 3 секунды";
                    _adsBefore2 = "до показа рекламы 2 секунды";
                    _adsBefore1 = "до показа рекламы 1 секунда";
                    break;
                case "en":
                    _adsBefore3 = "3 seconds before the ad is shown";
                    _adsBefore2 = "2 seconds before the ad is shown";
                    _adsBefore1 = "1 second before the ad is shown";
                    break;
                case "tr":
                    _adsBefore3 = "Reklam g?sterilmeden 3 saniye ?nce";
                    _adsBefore2 = "Reklam g?sterilmeden 2 saniye ?nce";
                    _adsBefore1 = "Reklam g?sterilmeden 1 saniye ?nce";
                    break;
                case "id":
                    _adsBefore3 = "3 detik sebelum iklan ditampilkan";
                    _adsBefore2 = "2 detik sebelum iklan ditampilkan";
                    _adsBefore1 = "1 detik sebelum iklan ditampilkan";
                    break;
            }
        }

        public static void FullscreenAds()
        {
            if (instance._timerForAd < 60) return;

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
            _adsText.text = _adsBefore3;
            yield return new WaitForSeconds(1);
            _adsText.text = _adsBefore2;
            yield return new WaitForSeconds(1);
            _adsText.text = _adsBefore1;
            yield return new WaitForSeconds(1);
            _adsAction?.Invoke();
            _adsObject.SetActive(false);
            YandexGame.FullscreenShow();
        }
    }
}