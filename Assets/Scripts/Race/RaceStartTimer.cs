using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public partial class RaceManager : MonoBehaviour
{
    public class RaceStartTimer : MonoBehaviour
    {
        private Text _timerText;
        private UnityAction _endAction;

        private const float StartTextSize = 0.7f;
        private const float EndTextSize = 1f;

        public void Init(Text timer, UnityAction atEnd)
        {
            _timerText = timer;
            _endAction = atEnd;

            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            _timerText.gameObject.SetActive(true);

            int seconds = 3;

            for (int s = seconds; s > 0; s--)
            {
                _timerText.text = s.ToString();

                for (int i = 0; i < 50; i++)
                {
                    yield return new WaitForSeconds(0.02f);
                    float size = StartTextSize + ((EndTextSize - StartTextSize) * i * 0.02f);
                    transform.localScale = new Vector3(size, size, size);
                }
            }

            _timerText.text = "Go";
            _endAction?.Invoke();

            yield return new WaitForSeconds(0.3f);

            _timerText.gameObject.SetActive(false);

            Destroy(this);
            
            yield break;
        }
    }
}