using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    public Slider handleSlider;
    public Slider loopSlider;
    public SystemCraft craft;

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
    }

    public void CraftButtonClicked()
    {
        int handleValue = (int)handleSlider.value;
        int loopValue = (int)loopSlider.value;

        if (loopValue == handleValue || loopValue == handleValue - 1 || loopValue == handleValue + 1)
        {
            craft.CraftItem();
            Debug.Log("Acertou!");
        }
        else
        {
            SetRandomSliderValue(handleSlider);
            Debug.Log("Errou!");
            craft.RemoveItem();
        }
    }


    void SetRandomSliderValue(Slider slider)
    {
        int randomValue = Random.Range((int)slider.minValue, (int)slider.maxValue + 1);
        slider.value = randomValue;
    }
}
