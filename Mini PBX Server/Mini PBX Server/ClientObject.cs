using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mini_PBX.Models;
using Mini_PBX_Server.Model;
using Mini_PBX_Server.Service;

namespace Mini_PBX
{
	public class ClientObject
	{
		//private string phone_number;
		private ClientDTO clientDTO = new ClientDTO();
		protected internal NetworkStream Stream { get; private set; }
		TcpClient client;
		ServerObject server; // объект сервера
		Action<string, string> _callback;
		public string GetPhone_number() { return clientDTO.phone_number; }
		public ClientObject(TcpClient tcpClient, ServerObject serverObject)
		{
			client = tcpClient;
			server = serverObject;
			serverObject.AddConnection(this);
		}
		public void Process()
		{
			string message = "";
			string call_numb = "";
			try
			{
				
				UserService userService = new UserService(new ClientRepository(new ClientContext()));
				Stream = client.GetStream();
				// Get phone number
				message = GetMessage();
				for (int i = 0; i < message.Length; i++)
				{
					if (message[i] >= '0' && message[i] <= '9')
						clientDTO.phone_number += message[i];
					else if ((message[i] >= 'a' && message[i] <= 'z') || (message[i] >= 'A' && message[i] <= 'Z'))
						clientDTO.userName += message[i];
				}
				if (clientDTO.phone_number.Length == 3)
				{
					if (!userService.IsUserExist(clientDTO))
					{
						userService.ClientRegister(clientDTO);
						message = "Регистрация прошла успешно";
						Console.WriteLine(message);
						server.BroadcastMessage(String.Format("\n" + message + "\nВаш никнейм {0} \nВаш номер телефона : {1}", clientDTO.userName, clientDTO.phone_number), this);
					}
					else
					{
						message = "Авторизация прошла успешно";
						server.BroadcastMessage(String.Format("\n" + message + "\nВаш никнейм {0} \nВаш номер телефона : {1}", clientDTO.userName, clientDTO.phone_number), this);
					}
					message = clientDTO.phone_number + ": На линии ";
					Console.WriteLine(message);

					message = GetMessage();
					message = clientDTO.phone_number + ": " + message;
					for (int i = 5; i < message.Length; i++)
					{
						if (message[i] >= '0' && message[i] <= '9')
							call_numb += message[i];
					}
					server.CheckAndConectAsync(call_numb, this);
					Console.WriteLine(message);
					bool check = true;
					while (check)
					{
						try
						{
							message = GetMessage();
							message = String.Format("{0}: {1}", clientDTO.phone_number, message);
							Console.WriteLine(message);
							server.BroadcastMessageAsync(message, this.clientDTO.phone_number);
						}
						catch
						{
							message = String.Format("{0}: Сбросил", clientDTO.phone_number);
							Console.WriteLine(message);
							server.BroadcastMessageAsync(message, this.clientDTO.phone_number);
							server.RemoveCall(this);
							check = false;
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				// if exit from cycle
				server.RemoveConnection(this.clientDTO.phone_number);
				Close();
			}
		}
		public Task ProcessAsync()
		{
			return Task.Run(() =>
			{
				Process();
			});
		}
		public string GetMessage()
		{
			byte[] data = new byte[64];
			StringBuilder builder = new StringBuilder();
			int bytes = 0;
			 bytes = Stream.Read(data, 0, data.Length);
			builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			return builder.ToString();
		}
	    
		public void Close()
		{
			if (Stream != null)
				Stream.Close();
			if (client != null)
				client.Close();
		}
	}
}
