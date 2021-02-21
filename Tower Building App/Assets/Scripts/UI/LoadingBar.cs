// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class LoadingBar : MonoBehaviour
// {   

//     public Slider slider;
//     // Start is called before the first frame update
//     void Start()
//     {
//         StartCoroutine(LoadProgress());
//     }

//     IEnumerator LoadProgress(){
//         AsyncOperation operation = SceneManager.LoadScene(1);
//         while (!operation.isDone){
//             float progress = Mathf.Clamp01(operation.progess/.9f);
//             slider.value = progress;
//             yield return null;
//         }

//     }
