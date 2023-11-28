using because.Shared.Commands;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Offline;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using System;
using System.Xml.Linq;

namespace because.Services
{
	public class AuthService
	{
		private readonly FirebaseAuthClient _authClient = new FirebaseAuthClient(new FirebaseAuthConfig()
		{
			ApiKey = "AIzaSyCcEhwQU4BhoLl_mYVHDkwUPT2JmShLMag",
			AuthDomain = "because-f3c54.firebaseapp.com",
			Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
			{
				new EmailProvider()
			}
		});
		public static CurrentUser _currentUser;
		public static FirestoreDb Database;
		public AuthService()
		{
		}
		public async Task<MethodResult> SigninAsync(SigninModel signinModel)
		{
			try
			{
				UserCredential userCredential = await _authClient.SignInWithEmailAndPasswordAsync(
					signinModel.Email,
					signinModel.Password);

				await FirestoreService.InitializeFirestoreAsync();

				Database = FirestoreDb.Create("because-f3c54");

				_currentUser = new CurrentUser()
				{
					Email = signinModel.Email,
					HouseHoldName = await GetHouseHoldNameByEmail(signinModel.Email),
					Name = await GetNameByEmail(signinModel.Email),
				};
				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again");
			}
		}
		public async Task<MethodResult> SignUpAsync(SignupModel signupModel)
		{
			try
			{
				await _authClient.CreateUserWithEmailAndPasswordAsync(signupModel.Email, signupModel.Password, signupModel.HouseHoldName);

				_currentUser = new CurrentUser()
				{
					Email = signupModel.Email,
					HouseHoldName = signupModel.HouseHoldName,
					Role = "P",
				};

				await FirestoreService.InitializeFirestoreAsync();

				Database = FirestoreDb.Create("because-f3c54");

				CollectionReference collection = Database.Collection("Users");
				Dictionary<string, object> user = new Dictionary<string, object>()
				{
					{"Email", signupModel.Email},
					{"HouseHoldName", signupModel.HouseHoldName },
				};
				await collection.AddAsync(user);

				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again.");
			}
		}
		public async Task<string> GetHouseHoldNameByEmail(string userEmail)
		{
			try
			{


				CollectionReference collection = Database.Collection("Users");

				QuerySnapshot querySnapshot = await collection
					.WhereEqualTo("Email", userEmail)
					.Limit(1)
					.GetSnapshotAsync();

				if (querySnapshot.Documents.Count > 0)
				{
					DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

					if (documentSnapshot.TryGetValue("HouseHoldName", out object houseHoldNameValue))
					{
						string houseHoldName = houseHoldNameValue?.ToString();
						return houseHoldName;
					}
				}

				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving user data: {ex.Message}");
				return null;
			}
		}
		public async Task<string> GetNameByEmail(string userEmail)
		{
			try
			{


				CollectionReference collection = Database.Collection("Users");

				QuerySnapshot querySnapshot = await collection
					.WhereEqualTo("Email", userEmail)
					.WhereEqualTo("Role", "P")
					.Limit(1)
					.GetSnapshotAsync();

				if (querySnapshot.Documents.Count > 0)
				{
					DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

					if (documentSnapshot.TryGetValue("Name", out object name))
					{
						return name?.ToString();
					}
				}

				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving user data: {ex.Message}");
				return null;
			}
		}

			public async Task<MethodResult> ParentSetupAsync(SignupModel signupModel)
		{
			try
			{


				CollectionReference collection = Database.Collection("Users");

				QuerySnapshot querySnapshot = await collection
					.WhereEqualTo("Email", _currentUser.Email)
					.GetSnapshotAsync();

				if (querySnapshot.Documents.Count > 0)
				{
					byte[] fileBytes;
					using (var memoryStream = new MemoryStream())
					{
						await signupModel.ProfileImage.OpenReadStream().CopyToAsync(memoryStream);
						fileBytes = memoryStream.ToArray();
					}
					string base64ProfileImage = Convert.ToBase64String(fileBytes);
					base64ProfileImage = $"data:{signupModel.ProfileImage.ContentType};base64,{base64ProfileImage}";

					DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

					var updates = new Dictionary<string, object>	
			{
				{"Name", signupModel.ParentName},
				{"ProfilePicture", base64ProfileImage},
				{"Role", "P"},
			};

					await documentSnapshot.Reference.UpdateAsync(updates);
				}
				else
				{
					Console.WriteLine("No matching user document found.");
				}

				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again.");
			}
		}
	}
}

