using UnityEngine;

public class DrinkObject : FoodObject
{
   public override void PlayerEntered()
   {
        int randomDrinkIndex = Random.Range(-10, 10);

       Destroy(gameObject);
      
       //increase food
       GameManager.Instance.ChangeFood(randomDrinkIndex);
   }
}
