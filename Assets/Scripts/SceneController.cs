using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public const int ROWS = 4;
    public const int COLUMNS = 4;
    public const float OFF_SET_X = 2f;
    public const float OFF_SET_Y = 2f;

    public const int GOOD_MATCH = 10;
    public const int BAD_MATCH = -2;

    public const float WAITING_TIME = 0.7f;

    public int[] idTable = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

    private MainCard Pair1;
    private MainCard Pair2;

    private int SCORE = 0;

    [SerializeField]
    private TextMesh textScore;
    [SerializeField]
    private MainCard mainCard;
    [SerializeField]
    private Sprite[] faces;

    private void Start()
    {
        Vector3 startPosition = mainCard.transform.position;
        idTable = Shuffle(idTable);

        for (int i = 0; i < COLUMNS; i++)
        {
            for (int j = 0; j < ROWS; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = mainCard;
                }
                else
                {
                    card = Instantiate(mainCard) as MainCard;
                }

                int index = j * COLUMNS + i;
                card.setFaceAndId(idTable[index], faces[idTable[index]]);

                float posX = startPosition.x + (OFF_SET_X * i);
                float posY = startPosition.y + (OFF_SET_Y * j);

                card.transform.position = new Vector3(posX, posY, startPosition.z);
            }
        }
    }

    private int[] Shuffle(int[] tab)
    {
        for (int i = 0; i < tab.Length; i++)
        {
            int tmp = tab[i];
            int randIndex = Random.Range(0, tab.Length);
            tab[i] = tab[randIndex];
            tab[randIndex] = tmp;
        }
        return tab;
    }

    public bool canWork()
    {
        return Pair2 == null;
    }

    public void showed(MainCard card)
    {
        if (Pair1 == null)
        {
            Pair1 = card;
        }
        else
        {
            Pair2 = card;
            StartCoroutine(matchCheckingCuroutine());
        }
    }

    private IEnumerator matchCheckingCuroutine()
    {
        if (Pair1.id == Pair2.id)
        {
            setNewScore(GOOD_MATCH);
        }
        else
        {
            setNewScore(BAD_MATCH);

            yield return new WaitForSeconds(WAITING_TIME);

            Pair1.setBackCard();
            Pair2.setBackCard();
        }
        Pair1 = null;
        Pair2 = null;
    }

    public void setNewScore(int points)
    {
        SCORE += points;
        textScore.text = "SCORE: " + SCORE;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
