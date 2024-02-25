using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Services
{
    public class MetricaManager : MonoBehaviour
    {
        private const string EventName = "triggers";
        
        public static MetricaManager Instance;

        private void Awake() => 
            Instance = this;

        /// <summary>
        /// Отправляет метрику без вложений (уровень загружен, уровень пройден, первый убитый моб, куплен скин и тд.)
        /// </summary>
        /// <param name="id">Название метрики в консоли</param>
        public static void Send(string id) =>
            YandexMetrica.Send(id);

        /// <summary>
        /// Отправляет вложенную метрику
        /// Уровень :
        ///     монстров убито - 123
        ///     первое убийство - 123
        ///     выстрелов - 1234
        ///     и тд
        /// в данном случае отправляем вложенные метрики в цель triggers
        /// </summary>
        /// <param name="name"></param>
        public static void AttachedSend(string name)
        {
            var eventParams = new Dictionary<string, string>
            {
                { EventName, name }
            };
            YandexMetrica.Send(EventName, eventParams);
        }
        
    }
}