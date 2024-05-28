using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace RpDev.Runtime.UI
{
	[RequireComponent(typeof(TMP_Text))]
	public class MessageOfTheHour : MonoBehaviour
	{
		private TMP_Text _text;

		private readonly string[] _positiveAdvicesEN = {
			"Embrace change and adapt to new challenges.",
			"Always be kind and compassionate to others.",
			"Pursue your passions and follow your dreams.",
			"Practice gratitude for the little things in life.",
			"Take care of your physical and mental health.",
			"Learn from your mistakes and keep moving forward.",
			"Stay curious and never stop learning.",
			"Believe in yourself and your abilities.",
			"Find joy in the simple moments.",
			"Set meaningful goals and work towards them.",
			"Be patient. Good things take time.",
			"Stay humble and open-minded.",
			"Help others whenever you can.",
			"Stay true to your values.",
			"Take breaks and prioritize self-care.",
			"Be a good listener.",
			"Focus on solutions, not problems.",
			"Keep a positive attitude, even in tough times.",
			"Celebrate your achievements, no matter how small.",
			"Treat yourself with kindness and self-compassion.",
			"Foster a sense of wonder and awe.",
		};

		private readonly string[] _positiveAdvicesRU =
		{
			"Прими изменения и приспосабливайся к новым вызовам.",
			"Всегда будь добрым и сострадательным к другим.",
			"Преследуй свои страсти и следуй за своими мечтами.",
			"Практикуй благодарность за мелочи в жизни.",
			"Заботься о своем физическом и психическом здоровье.",
			"Учись на своих ошибках и продолжай двигаться вперед.",
			"Оставайся любознательным и никогда не прекращай учиться.",
			"Верь в себя и свои способности.",
			"Находи радость в простых моментах.",
			"Устанавливай значимые цели и работай над их достижением.",
			"Будь терпелив. Хорошие вещи требуют времени.",
			"Оставайся скромным и открытым к новым идеям.",
			"Помогай другим, когда только можешь.",
			"Оставайся верным своим ценностям.",
			"Береги себя и уделяй внимание самопомощи.",
			"Будь хорошим слушателем.",
			"Сосредотачивайся на решениях, а не на проблемах.",
			"Сохраняй позитивное отношение, даже в трудные времена.",
			"Празднуй свои достижения, какими бы маленькими они ни были.",
			"Обращайся с собой с добротой и самосостраданием.",
			"Поддерживай чувство удивления и восхищения."
		};

		private void Awake ()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void Start ()
		{
			var advice = _positiveAdvicesRU[Random.Range(0, _positiveAdvicesEN.Length)];
			_text.text = string.Format(_text.text, advice);
		}
	}
}
