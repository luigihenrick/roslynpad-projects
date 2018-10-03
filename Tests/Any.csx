
var oldItems = new List<TbPrateleira>
{
    new TbPrateleira{ IdAtivoCoe = 1, IdPrateleira = 1},
    new TbPrateleira{ IdAtivoCoe = 2, IdPrateleira = 1},
    new TbPrateleira{ IdAtivoCoe = 3, IdPrateleira = 2},
    new TbPrateleira{ IdAtivoCoe = 4, IdPrateleira = 2}
};

var newItems = new List<TbPrateleira>
{
    new TbPrateleira{ IdAtivoCoe = 3, IdPrateleira = 2},
    new TbPrateleira{ IdAtivoCoe = 4, IdPrateleira = 2},
    new TbPrateleira{ IdAtivoCoe = 5, IdPrateleira = 3},
    new TbPrateleira{ IdAtivoCoe = 6, IdPrateleira = 3}
};


var oldsToDelete = oldItems
                .Where(o => !newItems.Any(n => n.IdPrateleira == o.IdPrateleira && n.IdAtivoCoe == o.IdAtivoCoe))
                .ToList()
                .Dump();
                
var oldsToUpdate = oldItems
                .Where(o => newItems.Any(n => n.IdPrateleira == o.IdPrateleira && n.IdAtivoCoe == o.IdAtivoCoe))
                .ToList()
                .Dump();
                
var newToInsert = newItems
                .Where(n => !oldItems.Any(o => o.IdPrateleira == n.IdPrateleira && o.IdAtivoCoe == n.IdAtivoCoe))
                .ToList()
                .Dump();

class TbPrateleira 
{
    public int IdAtivoCoe { get; set; }
    public int IdPrateleira { get; set; }
}