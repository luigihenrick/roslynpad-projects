public class Foo
{
	public int Id { get; set; }
	public Bar Bar { get; set; }
}

public class Bar
{
	public int Id { get; set; }
	public string Name { get; set; }
}

new Foo
{
	Id = 22,
	Bar = new Bar
	{
		Id = 12454,
		Name = "Some Info"
	}
}.Dump();