using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CardSelectSystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private GameObject enemyObject;
    private Movement movement;
    private ObjectPool objectPool;
    private Transform trashTransform;
    private List<CardSelectComponent> cardSelectList = new List<CardSelectComponent>();
    private List<CardBaseComponent> cardBaseList = new List<CardBaseComponent>();
    private int picUpCardIndex = -1;

    public CardSelectSystem(GameEvent gameEvent, Movement movement, ObjectPool objectPool, GameObject player, GameObject enemy, Transform trash)
    {
        this.gameEvent = gameEvent;
        this.movement = movement;
        this.objectPool = objectPool;
        this.playerObject = player;
        this.enemyObject = enemy;
        this.trashTransform = trash;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(CardSelectComponent cardSelect)
    {
        cardSelect.BasePosition = cardSelect.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnUpdate()
    {
        List<int> picIndexList = new List<int>();

        for (int i = 0; i < cardBaseList.Count; i++)
        {
            CardSelectComponent cardSelect = cardSelectList[i];
            CardBaseComponent cardBase = cardBaseList[i];

            if (cardSelect.AttackEffectList.Count > 0)
            {
                List<int> indexList = new List<int>();
                cardSelect.AttackEffectList.ForEach(x =>
                {
                    if (x.MoveNext()) return;
                    indexList.Add(cardSelect.AttackEffectList.IndexOf(x));
                    return;
                });

                indexList.ForEach(x => cardSelect.AttackEffectList.RemoveAt(x));
            }

            if (!cardSelect.gameObject.activeSelf) continue;

            RectTransform rectTransform = cardBase.GetComponent<RectTransform>();

            if (IsMouseOnCard(cardBase))
            {
                if (!Input.GetMouseButton(0))
                {
                    rectTransform.anchoredPosition = cardSelect.BasePosition + cardSelect.PositionOffset;
                    picIndexList.Add(i);
                    if (picIndexList.Count >= 2)
                    {
                        RectTransform picRectTransform = cardSelectList[picIndexList[0]].GetComponent<RectTransform>();
                        picRectTransform.anchoredPosition = cardSelectList[picIndexList[0]].BasePosition;
                    }
                }
                else if (picUpCardIndex == -1)
                {
                    picUpCardIndex = i;
                }
            }
            else
            {
                rectTransform.anchoredPosition = cardSelect.BasePosition;
            }

            if (picUpCardIndex != i) continue;
            rectTransform.anchoredPosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
            cardSelect.LiftPosition = rectTransform.anchoredPosition;

            if (!Input.GetMouseButtonUp(0)) continue;
            picUpCardIndex = -1;

            if (cardSelect.LiftPosition.y < cardSelect.UseHeight) continue;
            CharacterBaseComponent characterBase = playerObject.GetComponentInParent<CharacterBaseComponent>();

            if (characterBase.ManaPoint < cardBase.CostPoint) continue;
            DamageComponent damage = enemyObject.GetComponent<DamageComponent>();
            damage.DamagePoint = characterBase.AttackPoint * cardBase.AttackPoint;
            GenerateAttackEffect(cardSelect);
            characterBase.ManaPoint -= cardBase.CostPoint;
            cardSelect.LiftPosition = Vector3.zero;
            cardSelect.gameObject.SetActive(false);
        }
    }

    private bool IsMouseOnCard(CardBaseComponent cardBase)
    {
        RectTransform rectTransform = cardBase.GetComponent<RectTransform>();
        Vector2 position = rectTransform.anchoredPosition;
        Vector2 size = rectTransform.sizeDelta * rectTransform.localScale / 2;
        float rad = rectTransform.rotation.z * Mathf.Deg2Rad;
        Vector2 vertex_left_up = new Vector2(
    (-size.x) * Mathf.Cos(rad) + (size.y * -Mathf.Sin(rad)),
    (-size.x) * Mathf.Sin(rad) + (size.y * Mathf.Cos(rad)));
        Vector2 vertex_right_up = new Vector2(
    (size.x) * Mathf.Cos(rad) + (size.y * -Mathf.Sin(rad)),
    (size.x) * Mathf.Sin(rad) + (size.y * Mathf.Cos(rad)));
        Vector2 vertex_left_down = new Vector2(
    (-size.x) * Mathf.Cos(rad) + (-size.y * -Mathf.Sin(rad)),
    (-size.x) * Mathf.Sin(rad) + (-size.y * Mathf.Cos(rad)));
        Vector2 vertex_right_down = new Vector2(
    (size.x) * Mathf.Cos(rad) + (-size.y * -Mathf.Sin(rad)),
    (size.x) * Mathf.Sin(rad) + (-size.y * Mathf.Cos(rad)));

        vertex_left_up += position;
        vertex_right_up += position;
        vertex_left_down += position;
        vertex_right_down += position;

        Vector2 left_down_to_left_up = vertex_left_up - vertex_left_down;
        Vector2 left_up_to_right_up = vertex_right_up - vertex_left_up;
        Vector2 right_up_to_right_down = vertex_right_down - vertex_right_up;
        Vector2 right_down_to_left_down = vertex_left_down - vertex_right_down;

        Vector2 mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector2 mousePosition_to_left_down = mousePosition - vertex_left_down;
        Vector2 mousePosition_to_left_up = mousePosition - vertex_left_up;
        Vector2 mousePosition_to_right_up = mousePosition - vertex_right_up;
        Vector2 mousePosition_to_right_down = mousePosition - vertex_right_down;

        float crossCheck = 0.0f;
        crossCheck = left_down_to_left_up.x * mousePosition_to_left_down.y - left_down_to_left_up.y * mousePosition_to_left_down.x;
        if (crossCheck > 0) return false;
        crossCheck = left_up_to_right_up.x * mousePosition_to_left_up.y - left_up_to_right_up.y * mousePosition_to_left_up.x;
        if (crossCheck > 0) return false;
        crossCheck = right_up_to_right_down.x * mousePosition_to_right_up.y - right_up_to_right_down.y * mousePosition_to_right_up.x;
        if (crossCheck > 0) return false;
        crossCheck = right_down_to_left_down.x * mousePosition_to_right_down.y - right_down_to_left_down.y * mousePosition_to_right_down.x;
        if (crossCheck > 0) return false;

        return true;
    }

    private void GenerateAttackEffect(CardSelectComponent cardSelect)
    {
        cardSelect.AttackEffect = objectPool.GetGameObject(cardSelect.AttackEffectPrefab);
        cardSelect.AttackEffect.SetActive(true);
        cardSelect.AttackEffect.transform.SetParent(cardSelect.EffectRoot.transform);
        Vector3 tempPos = cardSelect.transform.position + new Vector3(0, 0, 5);
        tempPos = Camera.main.ScreenToWorldPoint(new Vector3(tempPos.x, tempPos.y, 0.1f));
        cardSelect.AttackEffect.transform.position = new Vector3(tempPos.x, tempPos.y, 0);
        cardSelect.AttackEffect.transform.localScale = new Vector3(1, 1, 1);
        PlayerAttackEffectComponent playerAttack = cardSelect.AttackEffect.GetComponent<PlayerAttackEffectComponent>();
        playerAttack.StartPosition = new Vector3(tempPos.x, tempPos.y, 0);
        tempPos = trashTransform.position + new Vector3(0, 0, 10);
        tempPos = Camera.main.ScreenToWorldPoint(new Vector3(tempPos.x, tempPos.y, 0.1f));
        playerAttack.EndPosition = new Vector3(10, -5.5f, 0);
        cardSelect.AttackEffectList.Add(AttackEffect(cardSelect, playerAttack));
    }

    private IEnumerator AttackEffect(CardSelectComponent cardSelect, PlayerAttackEffectComponent playerAttack)
    {
        while (true)
        {
            playerAttack.Timer += Time.deltaTime;
            float ratio = (playerAttack.EndPosition.x - playerAttack.StartPosition.x) * (playerAttack.Timer / playerAttack.TimerLimit);
            Vector3 temp = movement.Parabola(playerAttack.StartPosition, playerAttack.EndPosition, 60, ratio);
            playerAttack.transform.position = temp;
            if (playerAttack.Timer >= playerAttack.TimerLimit)
            {
                playerAttack.Timer = 0;
                gameEvent.ReleaseObject(playerAttack.gameObject);
                // gameEvent.transform.position = playerAttack.StartPosition;
                yield break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        CardSelectComponent cardSelect = gameObject.GetComponent<CardSelectComponent>();
        CardBaseComponent cardBase = gameObject.GetComponent<CardBaseComponent>();

        if (cardSelect == null || cardBase == null) return;

        cardSelectList.Add(cardSelect);
        cardBaseList.Add(cardBase);

        Initialize(cardSelect);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        CardSelectComponent cardSelect = gameObject.GetComponent<CardSelectComponent>();
        CardBaseComponent cardBase = gameObject.GetComponent<CardBaseComponent>();

        if (cardSelect == null || cardBase == null) return;

        cardSelectList.Remove(cardSelect);
        cardBaseList.Remove(cardBase);
    }
}
