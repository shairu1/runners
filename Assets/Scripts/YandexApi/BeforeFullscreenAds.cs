using System.Collections;
using TMPro;
using UnityEngine;
using YG;

namespace YandexApi
{
    public class BeforeFullscreenAds : MonoBehaviour
    {
        [Header("Межстраничная реклама")]
        [Tooltip("Интервал между запуском полноэкранной рекламы (от 60 секунд)")] [Range(60,180)]
        [SerializeField] private int timerForAd;
        [Tooltip("панель для сокрытия объектов сцены")]
        [SerializeField] private GameObject adsObject;
        [Tooltip("Предупреждение о рекламе")]
        [SerializeField] private TextMeshProUGUI timerText;

        private string _adsBefore3;
        private string _adsBefore2;
        private string _adsBefore1;
    
        private void OnEnable() => 
            YandexGame.SwitchLangEvent += ChangeText;

        private void OnDisable() => 
            YandexGame.SwitchLangEvent -= ChangeText;

        private void Start() => 
            StartCoroutine(AdShowDelay());

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
                    _adsBefore3 = "Reklam gösterilmeden 3 saniye önce";
                    _adsBefore2 = "Reklam gösterilmeden 2 saniye önce";
                    _adsBefore1 = "Reklam gösterilmeden 1 saniye önce";
                    break;
                case "id":
                    _adsBefore3 = "3 detik sebelum iklan ditampilkan";
                    _adsBefore2 = "2 detik sebelum iklan ditampilkan";
                    _adsBefore1 = "1 detik sebelum iklan ditampilkan";
                    break;

            }
        }

        /// <summary>
        /// интервал между межстраничной рекламой
        /// </summary>
        private IEnumerator AdShowDelay()
        {
            yield return new WaitForSeconds(timerForAd);
            StartCoroutine(InitializedAds());
        }
        /// <summary>
        /// предупреждение о показе рекламы
        /// </summary>
        private IEnumerator InitializedAds()
        {

            adsObject.SetActive(true);
            timerText.text = _adsBefore3;
            yield return new WaitForSeconds(1);
            timerText.text = _adsBefore2;
            yield return new WaitForSeconds(1);
            timerText.text = _adsBefore1;
            yield return new WaitForSeconds(1);
            adsObject.SetActive(false);
            StartCoroutine(AdShowDelay());
            YandexGame.FullscreenShow();
        }
    }
}
