using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplayController : MonoBehaviour
{
    public GameObject redCardNoEncounter;
    public GameObject blueCardNoEncounter;
    public GameObject greenCardNoEncounter;
    public GameObject greyCardNoEncounter;

    public Text displayText;
    public Transform rewardSpawn;

    private GameObject _parent = null;

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }
    public void HideThisAndParent()
    {
        if (_parent != null)
        {
            this.gameObject.SetActive(false);
            this._parent.SetActive(false);
        }
    }

    public void DisplayCardAsReward(int id)
    {
        if (id == -1)
        {
            Debug.LogError("NO CARD REWARD WAS SET FOR NPC. SET REWARD CARD ID IN INSPECTOR FOR NPC PREFAB");
        }
        Card card = (Card)Cards.CreateCardWithID(id);
        GameObject _cardPrefabInstance = null;
        switch (card.GetElement())
        {
            case "Intimidation":
                _cardPrefabInstance = Instantiate(redCardNoEncounter, rewardSpawn.position, rewardSpawn.rotation, this.gameObject.transform);
                break;
            case "Sympathy":
                _cardPrefabInstance = Instantiate(blueCardNoEncounter, rewardSpawn.position, rewardSpawn.rotation, this.gameObject.transform);
                break;
            case "Persuasion":
                _cardPrefabInstance = Instantiate(greenCardNoEncounter, rewardSpawn.position, rewardSpawn.rotation, this.gameObject.transform);
                break;
            case "Preparation":
                _cardPrefabInstance = Instantiate(greyCardNoEncounter, rewardSpawn.position, rewardSpawn.rotation, this.gameObject.transform);
                break;
        }

        NoEncounterCardPrefabController cardController = _cardPrefabInstance.GetComponent<NoEncounterCardPrefabController>();
        cardController.DisableInteractions();
        _cardPrefabInstance.transform.localScale = new Vector3(_cardPrefabInstance.transform.localScale.x + 0.22f, _cardPrefabInstance.transform.localScale.y + 0.22f, _cardPrefabInstance.transform.localScale.z);
        card.SetAndInitializeNoEncounterFrontendController(cardController);
        GameState.Player.collection.Value.Add(card.GetId());
        GameState.Player.collection.Value.Add(card.GetId());
        GameState.Player.collection.Value.Add(card.GetId());
        GameState.Player.collection.Raise();
        displayText.text = card.GetName();
    }
}
