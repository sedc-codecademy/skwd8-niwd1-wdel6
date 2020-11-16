using System;
using System.Collections.Generic;
using NineDemos;

// Dictionary<Dictionary<string, string>, Func<int, int, bool>> dictionary = new Dictionary<Dictionary<string, string>, Func<int, int, bool>>();
var dictionary = new Dictionary<Dictionary<string, string>, Func<int, int, bool>>();


Person weko = new ()
{
    FirstName = "Wekoslav",
    LastName = "Stefanovski"
};

//Person weko = new Person();
//weko.FirstName = "Wekoslav";
//weko.LastName = "Stefanovski";

Console.WriteLine(weko.FirstName);



Person weko2 = new Person("Wekoslav", "Stefanovski");

Console.WriteLine(weko2.FirstName);

Person weko3 = new()
{
    FirstName = "Weko",
    LastName = weko.LastName
};

Console.WriteLine(weko3.FirstName);

Person weko4 = weko with { FirstName = "Weko"};
Console.WriteLine(weko4.FirstName);



//weko.FirstName = "Weko";
//Console.WriteLine(weko.FirstName);
