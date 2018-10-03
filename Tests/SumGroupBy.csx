public class CoeAssetBestSeller
{
    public string PermanenceTime { get; set; }
    public int StructureId { get; set; }
    public string UnderlyingAssetsBasket { get; set; }
    public int OrdersCount { get; set; }
    public int CoeAssetId { get; set; }
    
    public int[] UnderlyingAssetIds => UnderlyingAssetsBasket.Split(',').Select(Int32.Parse).ToArray();
}

var coeAssetsBestSellers = new List<CoeAssetBestSeller> 
{
    new CoeAssetBestSeller { PermanenceTime = "48 mes(es)", StructureId = 1, UnderlyingAssetsBasket = "1,2,3,4", CoeAssetId = 1, OrdersCount = 100},
    new CoeAssetBestSeller { PermanenceTime = "24 mes(es)", StructureId = 2, UnderlyingAssetsBasket = "5,6",     CoeAssetId = 2, OrdersCount = 200},
    new CoeAssetBestSeller { PermanenceTime = "48 mes(es)", StructureId = 1, UnderlyingAssetsBasket = "1,2,3,4", CoeAssetId = 4, OrdersCount = 300},
    new CoeAssetBestSeller { PermanenceTime = "24 mes(es)", StructureId = 2, UnderlyingAssetsBasket = "5,6",     CoeAssetId = 3, OrdersCount = 400},
};

var groupAssets = coeAssetsBestSellers
    .GroupBy(a => new { a.StructureId, a.UnderlyingAssetsBasket, a.PermanenceTime })
    .SelectMany(group => group.Select(asset => new { asset.CoeAssetId, asset.OrdersCount, GroupOrdersCount = group.Sum(g => g.OrdersCount) }))
    .OrderByDescending(item => item.GroupOrdersCount).ThenByDescending(item => item.OrdersCount)
    //.Select(item => item.CoeAssetId)
    .Dump();