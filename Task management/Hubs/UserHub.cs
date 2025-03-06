using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class UserHub : Hub
{
	private readonly IIUserInformation iUserInformation;
	private readonly UserManager<ApplicationUser> _userManager;

	private static ConcurrentDictionary<string, ApplicationUser> ConnectedUsers = new();

	// تهيئة iUserInformation و UserManager في المُنشئ
	public UserHub(IIUserInformation iUserInformation, UserManager<ApplicationUser> userManager)
	{
		this.iUserInformation = iUserInformation;
		_userManager = userManager;
	}

	public override async Task OnConnectedAsync()
	{
		string connectionId = Context.ConnectionId;
		string userName = Context.User?.Identity?.Name ?? "Guest_" + connectionId;

		try
		{
			if (iUserInformation == null)
			{
				await Clients.Caller.SendAsync("Error", "خطأ في تهيئة iUserInformation.");
				return;
			}

			// استرجاع بيانات المستخدم
			var userInfo1 = await iUserInformation.GetAllByNameAsync(userName);
			var userInfo = await _userManager.Users
				.Where(x => x.UserName == userName && x.ActiveUser == true)
				.FirstOrDefaultAsync();

			if (userInfo == null)
			{
				await Clients.Caller.SendAsync("Error", "لم يتم العثور على بيانات المستخدم.");
				return;
			}

			// إضافة المستخدم إلى قائمة المتصلين
			ConnectedUsers[connectionId] = userInfo;

			// إرسال بيانات المستخدمين المتصلين إلى جميع العملاء
			await Clients.All.SendAsync("UserConnected", ConnectedUsers.Values.Select(u => new
			{
				u.UserName,
				u.Email,
				u.PhoneNumber,
				u.ImageUser // تأكد من أن هذه الخاصية موجودة في ApplicationUser
			}));
		}
		catch (Exception ex)
		{
			await Clients.Caller.SendAsync("Error", $"حدث خطأ أثناء جلب البيانات: {ex.Message}");
		}

		await base.OnConnectedAsync();
	}

	public override Task OnDisconnectedAsync(Exception? exception)
	{
		string connectionId = Context.ConnectionId;

		// إزالة المستخدم من قائمة المتصلين
		ConnectedUsers.TryRemove(connectionId, out _);

		// إرسال بيانات المستخدمين المتصلين بعد قطع الاتصال
		Clients.All.SendAsync("UserDisconnected", ConnectedUsers.Values.Select(u => u.UserName));
		return base.OnDisconnectedAsync(exception);
	}

	public static IEnumerable<ApplicationUser> GetOnlineUsers()
	{
		return ConnectedUsers.Values;
	}
}
