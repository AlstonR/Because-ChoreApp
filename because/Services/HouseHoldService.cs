using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class HouseHoldService
	{
		private readonly FirestoreDb _db = FirestoreDb.Create("because-f3c54");

		public HouseHoldService()
		{
			_db = FirestoreDb.Create("because-f3c54");
		}
		public async Task AddChildAsync(string childName)
		{
			var collection = _db.Collection("Users");

			string email = AuthService._currentUser.Email;


			var childData = new Dictionary<string, object>()
		{
			{ "Email", email },
			{ "Name", childName },
			{ "Role", "C" },
			{ "Coins", 0 }
		};

			await collection.AddAsync(childData);
		}
	}
}
