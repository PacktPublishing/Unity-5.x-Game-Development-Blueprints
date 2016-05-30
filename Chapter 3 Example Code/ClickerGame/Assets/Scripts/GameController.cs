using UnityEngine;
using UnityEngine.UI; // Text

public class GameController : MonoBehaviour {

    /// <summary>
    /// How much cash the player has
    /// </summary>
    private float _cash;
    public float Cash
    {
        get
        {
            return _cash;
        }
        set
        {
            _cash = value;
            cashText.text = "You have: $" + _cash.ToString("0.00");
        }
    }

    /// <summary>
    /// How much cash is automatically earned per second
    /// </summary>
    private float _cashPerSecond;
    public float CashPerSecond
    {
        get
        {
            return _cashPerSecond;
        }
        set
        {
            _cashPerSecond = value;
            rateText.text = "per second: " + 
                            _cashPerSecond.ToString("0.0");
        }
    }

    [Tooltip("How much cash players will make when they hit the button.")]
    public float cashPerClick = 1;

    [Header("Object References")]
    public Text cashText;
    public Text rateText;

	// Use this for initialization
	void Start ()
	{
	    Cash = 0;
        CashPerSecond = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Cash += CashPerSecond * Time.deltaTime;
	}

    public void ClickedButton()
    {
        Cash += cashPerClick;
    }

}
