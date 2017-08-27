using System;
using SQLite;

namespace Eval.Models
{
    /*
     * Sample to show how to convert a model to table
     * and manipulate it easily with SQLite
     */
	public class Contact
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; } //Auto increment
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool DoNotContact { get; set; }
	}
}
