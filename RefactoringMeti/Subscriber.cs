using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringMeti
{
    class Subscriber: IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Db { get; set; }
    }
}
