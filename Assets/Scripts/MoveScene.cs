using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [SerializeField] private bool haveToCheckUnlock;
    [SerializeField] private string nextSceneRank;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.canUsePortal)
        {
            if (other.GetComponent<Player>() != null && !haveToCheckUnlock)
            {
                GameManager.Instance.LoadScene(nextScene);
            }
            else if (other.GetComponent<Player>() != null)
            {
                switch (nextSceneRank)
                {
                    case "C":
                        if (GameManager.Instance.unlockMapC)
                            GameManager.Instance.LoadScene(nextScene);
                        break;
                    case "B":
                        if (GameManager.Instance.unlockMapB)
                            GameManager.Instance.LoadScene(nextScene);
                        break;
                    case "A":
                        if (GameManager.Instance.unlockMapA)
                            GameManager.Instance.LoadScene(nextScene);
                        break;
                    case "S":
                        if (GameManager.Instance.unlockMapS)
                            GameManager.Instance.LoadScene(nextScene);
                        break;
                    case "SS":
                        if (GameManager.Instance.unlockMapSS)
                            GameManager.Instance.LoadScene(nextScene);
                        break;
                }
            }
            GameManager.Instance.PortalDelay();
        }
    }

}
