

using System;
using System.Collections;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonSays : MonoBehaviour
{


    [SerializeField] GameObject[] rocks;
    [SerializeField] GameObject[] rocksShow;
    [SerializeField] GameObject[] sequence;
    [SerializeField] GameObject[] sequenceShow;
    [SerializeField] Image timerBar;
    float totalTime;
    float atualTime;
    [SerializeField] int position = 0;
    [SerializeField] int showPosition = 0;

    public bool play = false;
    public bool canPlay = false;
    [SerializeField] int level = 1;
    [SerializeField] int fails = 0;
    Coroutine timer;
    GM gm;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject errorPanel;
    [SerializeField] GameObject correctPanel;
    [SerializeField] HingeJoint hinge;

    void Start()
    {

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!play)
            {
                //Ani.SetTrigger("Button");
                play = true;
                Play();

            }
            if (atualTime > 0)
            {
                atualTime -= Time.deltaTime;
                timerBar.fillAmount = atualTime / totalTime;

            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void CreateSequence(GameObject[] rocks, GameObject[] rocksShow, int limit)
    {

        sequence = new GameObject[limit];
        sequenceShow = new GameObject[limit];

        for (int i = 0; i < sequence.Length; i++)
        {
            int rd = UnityEngine.Random.Range(0, rocks.Length);
            sequence[i] = rocks[rd];
            sequenceShow[i] = rocksShow[rd];

        }
    }

    public void Play()
    {
        fails = 0;
        if (winPanel.activeSelf == true) winPanel.SetActive(false);
        if (losePanel.activeSelf == true) losePanel.SetActive(false);
        if (startText.activeSelf == true) startText.SetActive(false);

        CreateSequence(rocks, rocksShow, level + 1);
        StartCoroutine(ShowSequence(showPosition, 1));
    }
    public void RockObject(GameObject rock)
    {

        CheckSequence(rock, sequence);
    }

    void CheckSequence(GameObject rock, GameObject[] sequence)
    {

        if (rock == sequence[position])
        {
            position += 1;
            //chamar sfx de acerto
        }
        else
        {
            //chamar sfx de erro
            timerBar.gameObject.SetActive(false);
            StopCoroutine(timer);
            position = 0;
            fails++;

            if (fails > 2)
            {
                Lose();
                return;
            }
            errorPanel.SetActive(true);
            StartCoroutine(DisableObject());

            return;
        }

        if (position == sequence.Length)
        {
            // se acertar a sequencia toda vem pra ca
            timerBar.gameObject.SetActive(false);
            StopCoroutine(timer);
            level++;
            position = 0;
            if (level > 3)
            {
                Win();
                return;
            }
            correctPanel.SetActive(true);
            CreateSequence(rocks, rocksShow, level + 1);
            StartCoroutine(DisableObject());
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2);
        if (correctPanel.activeSelf) correctPanel.SetActive(false);
        if (errorPanel.activeSelf) errorPanel.SetActive(false);

        StartCoroutine(ShowSequence(showPosition, 1));
    }
    IEnumerator ShowSequence(int pos, float time)
    {
        yield return new WaitForSeconds(time);
        canPlay = false;

        if (pos >= sequenceShow.Length)
        {
            canPlay = true;
            totalTime = level * 5;

            timer = StartCoroutine(Timer(totalTime));
            yield break;
        }


        sequenceShow[pos].SetActive(true);
        StartCoroutine(UnShow(pos));

    }
    IEnumerator Timer(float timer)
    {
        timerBar.gameObject.SetActive(true);
        atualTime = totalTime;
        if (timerBar != null) timerBar.fillAmount = 1;

        yield return new WaitForSeconds(timer);
        position = 0;
        fails++;
        timerBar.gameObject.SetActive(false);
        if (fails > 2)
        {
            Lose();
            yield break;
        }

        errorPanel.SetActive(true);
        StartCoroutine(DisableObject());


    }
    IEnumerator UnShow(int pos)
    {
        yield return new WaitForSeconds(2f);

        sequenceShow[pos].SetActive(false);
        pos++;
        StartCoroutine(ShowSequence(pos, 0.2f));

    }
    [ContextMenu("Win")]
    void Win()
    {
        // Porta();
        gm.puzzle1 = true;

        Array.Clear(sequenceShow, 0, sequenceShow.Length);
        Array.Clear(sequence, 0, sequence.Length);
        level = 1;
        play = false;

        winPanel.SetActive(true);

    }
    void Lose()
    {
        Array.Clear(sequenceShow, 0, sequenceShow.Length);
        Array.Clear(sequence, 0, sequence.Length);
        level = 1;
        play = false;

        losePanel.SetActive(true);
    }

    void Porta()
    {
        var motor = hinge.motor;
        motor.force = 100;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = true;

    }
}
