using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringMeti
{
    class Company: IUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // public string Db { get; set; }
    }
}
