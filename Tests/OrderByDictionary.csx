public enum Category
{
    PrefixedIncome = 1,
    PosfixedIncome = 2,
    Inflation = 3,
    MultiMarket = 4,
    VariableIncome = 5
}

public class Portfolio
{
    public double PreFixedIncome { get; set; }
    public double PosFixedIncome { get; set; }
    public double MultiMarket { get; set; }
    public double VariableIncome { get; set; }
    public double Alternatives { get; set; }
    public double Inflation { get; set; }
}

var customerPortifolio = new
{ 
    CurrentPortfolio = new Portfolio 
    {
        PreFixedIncome = 10,
        PosFixedIncome = 12,
        Alternatives = 0,
        Inflation = 0,
        MultiMarket = 0,
        VariableIncome = 0
    },
    SuggestedPortfolio = new Portfolio 
    {
        PreFixedIncome = 10,
        PosFixedIncome = 12,
        Alternatives = 13,
        Inflation = 15,
        MultiMarket = 20,
        VariableIncome = 30
    }
};

var categoriesHarmonization = new Dictionary<Category, double>();

categoriesHarmonization.Add(Category.PrefixedIncome, customerPortifolio.CurrentPortfolio.PreFixedIncome - customerPortifolio.SuggestedPortfolio.PreFixedIncome);
categoriesHarmonization.Add(Category.PosfixedIncome, customerPortifolio.CurrentPortfolio.PosFixedIncome - customerPortifolio.SuggestedPortfolio.PosFixedIncome);
categoriesHarmonization.Add(Category.VariableIncome, customerPortifolio.CurrentPortfolio.VariableIncome - customerPortifolio.SuggestedPortfolio.VariableIncome);
categoriesHarmonization.Add(Category.MultiMarket, customerPortifolio.CurrentPortfolio.MultiMarket - customerPortifolio.SuggestedPortfolio.MultiMarket);
categoriesHarmonization.Add(Category.Inflation, customerPortifolio.CurrentPortfolio.Inflation - customerPortifolio.SuggestedPortfolio.Inflation);

categoriesHarmonization.Dump();