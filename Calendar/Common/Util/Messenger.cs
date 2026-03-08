/*
 * Send로 Messenger에 신호를 보내면 해당 신호를 구독한 구독자들이 예약된 동작을 실행하도록 만듦 
 */

/*
 * Send로 Messenger에 신호를 보내면 해당 신호를 구독한 구독자들이 예약된 동작을 실행하도록 만듦 
 */
using System.Diagnostics;

namespace Calendar.Common.Util
{
    public static class Messenger
    {
        private static readonly Dictionary<Type, List<(object Recipient, Action<object?> Action)>> _subscribers = new();

        /// <summary>
        /// 메세지를 구독하게 만드는 메서드
        /// </summary>
        public static void Subscribe<T>(object recipient, Action<T> action)
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<(object, Action<object?>)>();

            // 들어온 Action<T>를 Action<object?>로 변환해서 저장
            _subscribers[type].Add((recipient, obj =>
            {
                if (obj is T target) action(target);
            }
            ));
        }

        /// <summary>
        /// 구독자에게 메세지를 보내서 실행하도록함
        /// </summary>
        public static void Send<T>(T message)
        {
            var type = typeof(T);
            // t를 구독중인 구독자가 있으면
            if (_subscribers.ContainsKey(type))
            {
                // 구독자들이 예약된 동작을 실행하게 신호를 줌
                foreach (var subscriber in _subscribers[type].ToList())
                    subscriber.Action.Invoke(message);
            }
        }
        /// <summary>
        /// 특정 객체의 모든 구독을 해제
        /// </summary>
        public static void Unregister(object recipient)
        {
            foreach (var key in _subscribers.Keys)
            {
                _subscribers[key].RemoveAll(s => s.Recipient == recipient);
            }
        }

        /// <summary>
        /// 디버그용
        /// </summary>
        private static void DebugCall()
        {
            Debug.WriteLine($"======= [Messenger] 현재 구독 현황 =======");
            foreach (var entry in _subscribers) // KeyValuePair로 순회
            {
                Type messageType = entry.Key;
                var subscriberList = entry.Value;

                if (subscriberList.Count > 0)
                {
                    Debug.WriteLine($"메시지 타입: {messageType.Name}");
                    foreach (var sub in subscriberList)
                    {
                        Debug.WriteLine($" - 남은 구독자: {sub.Recipient.GetType().Name} (Hash: {sub.Recipient.GetHashCode()})");
                    }
                }
            }
            Debug.WriteLine($"==========================================");
        }
    }
}
