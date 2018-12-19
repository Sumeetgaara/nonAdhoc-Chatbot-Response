using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using AdaptiveCards;

namespace NonAdhoc.Dialogs
{
	[Serializable]
	public class RootDialog : IDialog<object>
	{
		public Task StartAsync(IDialogContext context)
		{
			context.Wait(MessageReceivedAsync);

			return Task.CompletedTask;
		}

		private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
		{
			var activity = await result as Activity;

			// calculate something for us to return
			int length = (activity.Text ?? string.Empty).Length;

			// return our reply to the user
			//await context.PostAsync($"You sent {activity.Text} which was {length} characters");

			if(activity.Text == "Query1")
			{
				await context.PostAsync("Response1");
			}

			if (activity.Text == "Query5")
			{
				await context.PostAsync("Response5");
			}

			if (activity.Text == "Query3")
			{

				await Task.Delay(10000);
				Activity replyToConversation = (Activity)context.MakeMessage();
				replyToConversation.Attachments = new List<Attachment>();

				List<CardAction> cardButtons = new List<CardAction>();

				CardAction plButton = new CardAction()
				{
					Value = 1.ToString(),
					Title = "Your Query: Query3",
					Type = "imBack"

				};

				cardButtons.Add(plButton);
				CardAction plButton1 = new CardAction()
				{
					Value = 2.ToString(),
					Title = "Response: Response3",
					Type = "imBack"

				};
				cardButtons.Add(plButton1);
				HeroCard plCard = new HeroCard()
				{
					Title = "Long Response Message",
					Buttons = cardButtons
				};

				Attachment plAttachment = plCard.ToAttachment();
				replyToConversation.Attachments.Add(plAttachment);
				await context.PostAsync(replyToConversation);
			}

			context.Wait(MessageReceivedAsync);
		}
	}
}