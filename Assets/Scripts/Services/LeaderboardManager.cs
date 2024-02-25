using UnityEngine;
using YG;
using YG.Utils.LB;

namespace Services
{
    public class LeaderboardManager : MonoBehaviour
    {
        [Header("Есть unity update")]
        [SerializeField] private string nameInLeaderboard;
        [SerializeField] [Range(5, 20)] private float autosaveTime;
        
        private float _elapsedSaveTime;
        private float _scoreInLB;
        private float _timeInGame;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        private void OnDisable() => 
            YandexGame.GetDataEvent -= GetLoad;

        private void Update()
        {
            _timeInGame += Time.deltaTime;
            _elapsedSaveTime += Time.deltaTime;
            
            if(_elapsedSaveTime < autosaveTime) return;

            Save();
        }

        private void GetLoad() => 
            _timeInGame = YandexGame.savesData.timeInGame;

        private void Save()
        {
            YandexGame.savesData.timeInGame = _timeInGame;
            YandexGame.SaveProgress();
        }

        private void OnGetLeaderboard(LBData data)
        {
            _scoreInLB = data.thisPlayer.score;
            InvokeRepeating(nameof(SynchronizeLeaderboard), 2, 20);
            YandexGame.onGetLeaderboard -= OnGetLeaderboard;
        }

        private void SynchronizeLeaderboard()
        {
            float score = YandexGame.savesData.timeInGame;
            
            if (score > _scoreInLB)
            {
                _scoreInLB = score;
                YandexGame.NewLBScoreTimeConvert(nameInLeaderboard, _scoreInLB);
            }
        }
    }
}