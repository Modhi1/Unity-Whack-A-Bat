using UnityEngine;

    public class GameOver : MonoBehaviour
    {

    #region Variables 

    [SerializeField] private CanvasGroup gameOverImg;
    [SerializeField] private GameObject gameOverText;

    #endregion

    private void Start()
    {
        GameManager.instance.GameEnded += EndGame;
    }

    public void EndGame()
    {
            // set the scale initally to be 0
        GetComponent<RectTransform>().localScale = Vector3.zero;

        gameOverText.SetActive(true);
        
        LeanTweenExt.LeanScale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.2f);

        LeanTween.alphaCanvas(gameOverImg, 1, 1F);
    }



    }


