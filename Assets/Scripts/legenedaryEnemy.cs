using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class legendaryEnemy : Enemies
{
    public GameObject prefab;
    public GameObject common;
    public int healthPoints = 4;

    protected override void Start()
    {
        base.Start();
        setStats();
        canvas = FindObjectOfType<Canvas>();
        initiative = setInitiative();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // makeStatBlock();
        turnManager.setTurnStatusNum(initiative);
        InitializeEnemyOnTile();
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void setStats()
    {
        damage = 4;
        hitPoints = 8;
        origionalHitPoints = hitPoints;
        blockPoints = 3;
        setCoins();
        name1 = "Legendary Enemy";
        baseName = name1;
        prefixNum = Random.Range(0, 100);
        prefixName = pickPrefix(prefixNum);
        name1 = prefixName + name1;
        moveSpeed = 1 * tileSize;
        attackRange = (2 * tileSize);
        applySpecialEffect();
    }
    int setInitiative()
    {
        initiative = Random.Range(1, 19) - 1;
        if (initiative < 1)
        {
            initiative = 1;
        }

        return initiative;
    }

    protected void setCoins()
    {
        coins = Random.Range(3, 8);
    }
    protected override void startTurn()
    {
        base.startTurn();

    }
    protected override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
    }

    protected override void makeStatBlock()
    {
        base.makeStatBlock();
    }

    protected override void setStatBlock()
    {
        base.setStatBlock();
    }

    protected override void endTurn()
    {
        base.endTurn();
    }

    //Tile Code

    protected override void InitializeEnemyOnTile()
    {
        base.InitializeEnemyOnTile();
    }

    protected override Tile GetCurrentTile()
    {
        Tile tile = base.GetCurrentTile();
        return tile;
    }

    protected override Tile GetTileAtPosition(Vector3 position)
    {
        Tile tile = base.GetTileAtPosition(position);
        return tile;
    }

    protected override void MoveToTile(Tile newTile)
    {
        base.MoveToTile(newTile);
    }

    protected override Tile FindClosestAdjacentTile(Vector3 direction)
    {
        Tile newTile = base.FindClosestAdjacentTile(direction);
        return newTile;
    }

    protected override List<Tile> GetAdjacentTiles()
    {
        List<Tile> adjacentTiles = new List<Tile>();
        adjacentTiles = base.GetAdjacentTiles();

        return adjacentTiles;
    }

    //Take Damage and Die
    public override void drop()
    {
        base.drop();
    }

    public override void enemySlain()
    {
        base.enemySlain();

    }

    public override void takePhysicalDamage(int damage)
    {
        base.takePhysicalDamage(damage);

    }

    //Handle Attack

    public override bool IsPlayerAdjacent()
    {
        bool returnValue;
        returnValue = base.IsPlayerAdjacent();
        return returnValue;
    }

    public override void AttackPlayer()
    {
        base.AttackPlayer();
    }
    protected override string applySpecialEffect()
    {
        return base.applySpecialEffect();
    }
}