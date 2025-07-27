using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject robotPrefab, dogPrefab, catPrefab;
    public Transform spawnPoint;

    void Start()
    {
        string selected = GameManager.SelectedCharacter;
        GameObject prefab = selected switch
        {
            "Robot" => robotPrefab,
            "Dog" => dogPrefab,
            "Cat" => catPrefab,
            _ => robotPrefab
        };

        GameObject character = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Assign to UICanvasControllerInput
        var uiInput = FindObjectOfType<UICanvasControllerInput>();
        uiInput.starterAssetsInputs = character.GetComponent<StarterAssetsInputs>();

        // Assign to MobileDisableAutoSwitchControls
       // var mobileSwitch = FindObjectOfType<MobileDisableAutoSwitchControls>();
     //   mobileSwitch.playerInput = character.GetComponent<Player>();

        // Assign to Cinemachine Camera
        var cam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        var camTarget = character.transform.Find("PlayerCameraRoot");
        cam.Follow = camTarget;
        cam.LookAt = camTarget;
    }

    public void OnClickGotoHomeBtn()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
