using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Craft : MonoBehaviour
{
 public Slider handleSlider;
    public Slider loopSlider;

    private bool isMoving;
    private bool isMovingForward;
    private int currentValue;
    private float timer;
    private float interval = 0.1f;

    void Start()
    {
        SetRandomSliderValue(handleSlider);

        loopSlider.minValue = -10;
        loopSlider.maxValue = 10;

        isMoving = true;
        isMovingForward = true;
        timer = 0f;
    }

    void Update()
    {
        if (isMoving)
        {
            if (timer >= interval)
            {
                int increment = 1;

                if (isMovingForward)
                {
                    loopSlider.value += increment;

                    if (loopSlider.value >= loopSlider.maxValue)
                    {
                        loopSlider.value = (int)loopSlider.maxValue;
                        isMovingForward = false;
                    }
                }
                else
                {
                    loopSlider.value -= increment;

                    if (loopSlider.value <= loopSlider.minValue)
                    {
                        loopSlider.value = (int)loopSlider.minValue;
                        isMovingForward = true;
                    }
                }

                timer = 0f;
            }

            timer += Time.deltaTime;
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            if (loopSlider.value == handleSlider.value)
            {
                Debug.Log("Acertou!");
            }
            else
            {
                SetRandomSliderValue(handleSlider);
                Debug.Log("Errou!");
            }
        }
    }

    void SetRandomSliderValue(Slider slider)
    {
        int randomValue = Random.Range((int)slider.minValue, (int)slider.maxValue + 1);
        slider.value = randomValue;
    }

    void FixedUpdate()
    {

    }
}
