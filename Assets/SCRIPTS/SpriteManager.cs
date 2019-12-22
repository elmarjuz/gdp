using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour
{
    public bool randomizedSprite;
    public bool randomizedTint;
    public bool randomizedRotation;
    public float randomizedSize = 1;
    public Sprite[] sprites;
    public int index = -1;
    Sprite currentSprite;

    // Use this for initialization
    void Start()
    {
        if (index > -1 && index < sprites.Length)
        {
            currentSprite = sprites[index];
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
        if (randomizedSprite)
        {
            currentSprite = sprites[(int)Random.Range(0, sprites.Length)];
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
        if (randomizedSize != 1)
        {
            float currentSize = Random.Range(randomizedSize / 2, randomizedSize * 2);
            transform.localScale = new Vector3(currentSize, currentSize, 1);
        }

        if (randomizedTint)
        {
            SetColor(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.3f, 0.9f));
        }

        if (randomizedRotation)
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(1, 360));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSprite(Sprite external)
    {
        GetComponent<SpriteRenderer>().sprite = external;
    }

    public void PickSprite(int value)
    {
        if (value >= 0 && value < sprites.Length)
        {
            currentSprite = sprites[value];
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
        else
        {
            print("u r stupid. gimme good value.");
        }
    }

    public void SetColor(float r, float g, float b, float a)
    {
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, a);
    }

    public void setAlpha(float value)
    {
        value = value / 100;
        Color newColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, value);
        GetComponent<SpriteRenderer>().color = newColor;
    }
}
