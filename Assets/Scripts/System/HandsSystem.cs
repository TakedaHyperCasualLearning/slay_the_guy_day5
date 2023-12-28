using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsSystem
{
    private GameObject playerObject;
    private GameEvent gameEvent = null;
    private Transform deckTransform = null;
    private List<HandsComponent> handsList = new List<HandsComponent>();

    public HandsSystem(GameEvent gameEvent, GameObject player, Transform deckTransform)
    {
        this.gameEvent = gameEvent;
        playerObject = player;
        this.deckTransform = deckTransform;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(HandsComponent hands)
    {
        for (int i = 0; i < hands.CardList.Count; i++)
        {
            CardBaseComponent card = hands.CardList[i];
            gameEvent.AddComponentList?.Invoke(card.gameObject);
            DrawEffectComponent drawEffect = card.gameObject.GetComponent<DrawEffectComponent>();
            drawEffect.EndPosition = card.transform.position;
            drawEffect.EndRotation = hands.transform.rotation;
            drawEffect.StartPosition = deckTransform.position;
            card.transform.position = deckTransform.position;
            card.transform.rotation = hands.transform.rotation;
            card.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < handsList.Count; i++)
        {
            HandsComponent hands = handsList[i];
            if (!hands.gameObject.activeSelf) continue;

            foreach (var drawEffect in hands.DrawEffectList)
            {
                if (drawEffect.MoveNext()) continue;
                hands.DrawEffectList.Remove(drawEffect);
                break;
            }

            TurnComponent turn = playerObject.GetComponent<TurnComponent>();
            if (!turn.IsMyTurn || turn.TurnState != TurnState.Draw) continue;

            hands.DrawEffectTimer += Time.deltaTime;
            if (hands.DrawEffectTimer < hands.DrawEffectLimitTime) continue;
            DrawCard(hands);
            hands.DrawCount++;
            hands.DrawEffectTimer = 0.0f;

            if (hands.DrawCount != hands.CardList.Count) continue;
            hands.DrawCount = 0;
            turn.TurnState = TurnState.Play;
            Debug.Log(turn.gameObject.name + "Battle phase");
        }
    }

    private void DrawCard(HandsComponent hands)
    {
        List<CardBaseComponent> cardList = gameEvent.Draw();
        for (int i = 0; i < cardList.Count; i++)
        {
            hands.CardList[hands.DrawCount].CostPoint = cardList[i].CostPoint;
            hands.CardList[hands.DrawCount].AttackPoint = cardList[i].AttackPoint;
            hands.CardList[hands.DrawCount].Title = cardList[i].Title;
            hands.CardList[hands.DrawCount].Description = cardList[i].Description;

            hands.CardList[hands.DrawCount].CostPointText.text = cardList[i].CostPoint.ToString();
            hands.CardList[hands.DrawCount].TitleText.text = cardList[i].Title;
            hands.CardList[hands.DrawCount].DescriptionText.text = cardList[i].Description;

            hands.CardList[hands.DrawCount].gameObject.SetActive(true);
            Debug.Log(hands.CardList[hands.DrawCount].Title + "を手札に追加");

            DrawEffectComponent drawEffect = hands.CardList[hands.DrawCount].gameObject.GetComponent<DrawEffectComponent>();
            hands.DrawEffectList.Add(DrawEffect(drawEffect));
        }
    }

    private IEnumerator DrawEffect(DrawEffectComponent drawEffect)
    {
        while (true)
        {
            drawEffect.Timer += Time.deltaTime;
            float ratio = drawEffect.Timer / drawEffect.LimitTime;
            drawEffect.transform.position = Vector3.Lerp(drawEffect.StartPosition, drawEffect.EndPosition, ratio);
            drawEffect.transform.rotation = Quaternion.Lerp(drawEffect.transform.rotation, drawEffect.EndRotation, ratio);
            drawEffect.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(3, 3, 3), ratio);

            if (drawEffect.Timer >= drawEffect.LimitTime)
            {
                drawEffect.transform.position = drawEffect.EndPosition;
                drawEffect.transform.rotation = drawEffect.EndRotation;
                drawEffect.transform.localScale = new Vector3(3, 3, 3);
                drawEffect.Timer = 0;
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        HandsComponent hands = gameObject.GetComponent<HandsComponent>();

        if (hands == null) return;

        handsList.Add(hands);

        Initialize(hands);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        HandsComponent hands = gameObject.GetComponent<HandsComponent>();

        if (hands == null) return;

        handsList.Remove(hands);
    }
}
