using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
   public Tile EndTile;

   public override void Init(Vector2Int coord)
   {
       base.Init(coord);
       GameManager.Instance.BoardManager.SetCellTile(coord, EndTile);
   }

    public override void PlayerEntered()
    {
        if (!(GameManager.Instance.m_FoodAmount <= 0)) {
            GameManager.Instance.NewLevel();
        }
    }
}