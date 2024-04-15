public static class Utils
{
    public static string GetProfitObjText()
    {
        return $"Profit: {DataManager.GetTodayProfit()}/{DataManager.GetProfitObjective()}";
    }
    public static string GetTalkedObjText()
    {
        return $"People talked: {DataManager.GetTodayTalked()}/{DataManager.GetTalkedObjective()}";
    }
    public static string GetServedObjText()
    {
        return $"Dish served: {DataManager.GetTodayServed()}/{DataManager.GetServedObjective()}";
    }
}