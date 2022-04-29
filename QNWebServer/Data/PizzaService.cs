namespace QNWebServer.Data;
public class PizzaService
{
    public async Task<Pizza[]> GetPizzasAsync()
    {
        await Task.Delay(0);
        return new Pizza[]{new Pizza{Name="haoci",Description="haochide",Price=10.00M,Vegetarian=true,Vegan=false}};
    }
}