using System.Linq;

public class User
{
  public int UserID { get; set; }
  public List<string> UserName { get; set; }
  public int GroupID { get; set; }
}

List<User> userList = new List<User>();
userList.Add( new User { UserID = 1, UserName = new List<string> { "UserOne", "UserTwo" }, GroupID = 1 } );
userList.Add( new User { UserID = 7, UserName = new List<string> { "UserOne" }, GroupID = 7 } );
userList.Add( new User { UserID = 2, UserName = new List<string> { "UserTwo" }, GroupID = 1 } );
userList.Add( new User { UserID = 3, UserName = new List<string> { "UserThree" }, GroupID = 1 } );
userList.Add( new User { UserID = 4, UserName = new List<string> { "UserFour" }, GroupID = 1 } );
userList.Add( new User { UserID = 5, UserName = new List<string> { "UserOne" }, GroupID = 3 } );
userList.Add( new User { UserID = 6, UserName = new List<string> { "UserSix" }, GroupID = 3 } );

//var groupedCustomerList = userList.GroupBy( u => u.UserName ).ToList().Dump();
Dictionary<string, List<User>> groupedList = new Dictionary<string, List<User>>();

var names = userList.SelectMany(x => x.UserName).Distinct().ToList();
names.Dump();

foreach (var name in names)
{
    groupedList.Add(name, userList.Where(user => user.UserName.Contains(name)).ToList());  
}

groupedList.Dump();
