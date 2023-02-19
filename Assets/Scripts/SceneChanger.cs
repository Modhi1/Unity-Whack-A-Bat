using UnityEngine;
using UnityEngine.SceneManagement;

    public class SceneChanger : MonoBehaviour
    {

        public void PlayGameButtonClicked(int roomNum)
        {
            // a better way is to use index 
            SceneManager.LoadScene("Cave"+roomNum+" scene");
        }


        public void BackToMain()
        {
        // a better way is to use index 
          SceneManager.LoadScene("Main menu");

        }


        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }



