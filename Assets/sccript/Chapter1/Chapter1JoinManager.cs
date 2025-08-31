using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chapter1JoinManager : MonoBehaviour
{
    [SerializeField] ActorDetail[] eachActor;  //1 kitten, 2 for puppy , 3 for robbot

    [SerializeField] Transform blocksParent;
    [SerializeField] List<GameObject> currentBlocks = new List<GameObject>();
    [SerializeField] List<GameObject> newList = new List<GameObject>();

    [SerializeField] List<Joining> eachJoining;
    [SerializeField] AudioSource audioSource;

    [SerializeField] TopPosition[] topPosition; //top position depends on the length of word 2, 3 or 4

    int currentInd = 0;

    public void StartNewLine()
    {
        if (currentInd < eachJoining.Count)
        {
            
            StartCoroutine(StartAnim());
        }
    }

    IEnumerator StartAnim()
    {
        int waitCount = 0;
        int latterCount = -1;
        newList.Clear();
        StartTypingAndAudioClip();

        yield return new WaitForSeconds(eachJoining[currentInd].waitTimers[waitCount]);
        //remove latter from top which are not used  in  this word
        foreach(GameObject eachBlock in currentBlocks)
        {
            string topLatter = eachBlock.GetComponent<LetterBlock>().letter.ToLower();
            if (!eachJoining[currentInd].word.Contains(topLatter))
            {
                eachBlock.GetComponent<LetterBlock>().BringBackToBottom();
            }
        }
        waitCount++;
        yield return new WaitForSeconds(eachJoining[currentInd].waitTimers[waitCount]);
        //add first block to current  block
        foreach (char eachLatter in eachJoining[currentInd].word)
        {
            latterCount++;
            string currentLatter = eachLatter.ToString().ToLower();
            GameObject go =  CheckAlreadyThere(currentLatter);
            if(go!=null)
            {
                newList.Add(go);
                Vector3 newPos = topPosition[eachJoining[currentInd].word.Length - 2].topPos[latterCount];
                go.GetComponent<LetterBlock>().SwapPos(newPos);
            }
            else
            {
                
                go = GetFromBottom(currentLatter);
                newList.Add(go);
                Vector3 newPos = topPosition[eachJoining[currentInd].word.Length - 2].topPos[latterCount];
                go.GetComponent<LetterBlock>().BringOnTop(newPos);
            }
        }

        waitCount++;

        //fill  new  list  on current blocks  array
        currentBlocks.Clear();
        yield return new WaitForSeconds(eachJoining[currentInd].waitTimers[waitCount]);
        FillNewListInCurrentList();
        yield return new WaitForSeconds(eachJoining[currentInd].waitTimers[waitCount]);

        //join now words
        latterCount = -1;
        foreach(GameObject blocks in currentBlocks)
        {
            latterCount++;
            Vector3 newPos = topPosition[eachJoining[currentInd].word.Length - 2].joiningPos[latterCount];
            blocks.GetComponent<LetterBlock>().SwapPos(newPos);
        }
        waitCount++;
        yield return new WaitForSeconds(eachJoining[currentInd].waitTimers[waitCount]);

        EndAnim();
    }
    public void FillNewListInCurrentList()
    {
       

        foreach(GameObject eachBlock in newList)
        {
            currentBlocks.Add(eachBlock);
        }
        //newList.Clear();

    }
    GameObject CheckAlreadyThere(string latter)
    {
        foreach(GameObject eachLatter in currentBlocks)
        {
            if(eachLatter.GetComponent<LetterBlock>().letter.ToLower()==latter)
            {
                return eachLatter;
            }

        }
        return null;
    }
    GameObject GetFromBottom(string latter)
    {
        foreach (Transform eachLatter in blocksParent)
        {
            if (eachLatter.GetComponent<LetterBlock>().letter.ToLower() == latter)
            {
                return eachLatter.gameObject;
            }

        }
        return null;
    }

    public void StartTypingAndAudioClip()
    {
        if(currentInd!=0)
            eachActor[eachJoining[currentInd-1].actorInd].actorDialogBox.SetActive(false);

        eachActor[eachJoining[currentInd].actorInd].actorDialogBox.SetActive(true);
        eachActor[eachJoining[currentInd].actorInd].dialogueTextField.text = eachJoining[currentInd].typleLine;
    }
    public void EndAnim()
    {
        currentInd++;
        if (currentInd < eachJoining.Count)
        {
            StartNewLine();
        }
        else
            Debug.Log("game end")
;    }
}
[System.Serializable]
public class Joining
{
    public string typleLine;
    public AudioClip clip;
    public int actorInd;
    public bool joiningNeeded;
    public string word;
    public float[] waitTimers;
}

[System.Serializable]
public class TopPosition
{
    public Vector3[] topPos;
    public Vector3[] joiningPos;
}
