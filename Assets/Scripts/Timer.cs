using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[SerializeField]
	bool activeOnStart;

	[SerializeField]
	bool loop;

	[SerializeField]
	float timerSecondStep = 1f;

	[SerializeField]
	float timerAlarmTime = 1f;

	[SerializeField]
	UnityEvent onTimer;

	[SerializeField]
	[ReadOnly]
	float currentTime;

	Coroutine timerRoutine;

	private void Start()
	{
		if (activeOnStart)
			StartTimer();
	}

	[Button]
	public void StartTimer()
	{
		currentTime = timerAlarmTime;
		timerRoutine= StartCoroutine(TimerRoutine());
	}

	[Button]
	public void StopTimer()
	{
		StopCoroutine(timerRoutine);
	}

	private IEnumerator TimerRoutine()
	{
		while (currentTime > 0)
		{
			currentTime -= 1;
			yield return new WaitForSeconds(timerSecondStep);
		}
		onTimer.Invoke();
		if (loop)
			StartTimer();
	}
}
