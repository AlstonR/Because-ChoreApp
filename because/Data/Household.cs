using System;
using SQLite;
namespace because.Data
{
    public class HouseHold
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
		public string Name { get; set; }
		public string ParentName { get; set; }
		public string Username { get; set; }
        public string Password { get; set; }
		public byte[] ProfilePicture { get; set; }
	}
}

