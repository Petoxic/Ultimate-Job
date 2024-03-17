using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodDictionary
{
    public static Dictionary<FoodType, string> foodPath = new Dictionary<FoodType, string>(){
        {FoodType.STEAK, "food-OCAL_40"},
        {FoodType.CHICKEN, "food-OCAL_41"},
        {FoodType.EGG, "food-OCAL_42"},
        {FoodType.BOILED_EGG, "food-OCAL_43"},
        {FoodType.FRIED_EGG, "food-OCAL_44"},
        {FoodType.TURKEY, "food-OCAL_45"},
        {FoodType.DOUBLE_CHICKEN, "food-OCAL_46"},
        {FoodType.SUSHI_ROLL_1, "food-OCAL_47"},
        {FoodType.SUSHI_ROLL_2, "food-OCAL_48"},
        {FoodType.SUSHI_SHRIMP, "food-OCAL_49"},
        {FoodType.SUSHI_TUNA, "food-OCAL_50"},
        {FoodType.SUSHI_SALMON, "food-OCAL_51"},
        {FoodType.SUSHI_EGG, "food-OCAL_52"},
        {FoodType.SUSHI_SHELL, "food-OCAL_53"},
        {FoodType.SUSHI_EEL, "food-OCAL_54"},
        {FoodType.CUPCAKE, "food-OCAL_55"},
        {FoodType.DONUT, "food-OCAL_56"},
    };
}