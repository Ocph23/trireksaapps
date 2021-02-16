using Microsoft.Extensions.Options;
using System;
using WebApi.Models;

namespace WebApi.Services
{
    public class TelegramService:Telegram.Bot.TelegramBotClient
    {
        
        public TelegramService(IOptionsMonitor<TelegramConfig> option) : base(option.CurrentValue.ApiToken) {
            this.OnUpdate += TelegramService_OnUpdate;
            this.OnInlineResultChosen += TelegramService_OnInlineResultChosen;
            this.OnCallbackQuery += TelegramService_OnCallbackQuery; 
            this.OnMessage += TelegramService_OnMessage;
            this.OnMessageEdited += TelegramService_OnMessageEdited; 
            this.OnReceiveError += TelegramService_OnReceiveError;
            this.OnReceiveGeneralError += TelegramService_OnReceiveGeneralError;
        }

        private void TelegramService_OnReceiveGeneralError(object sender, Telegram.Bot.Args.ReceiveGeneralErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnMessageEdited(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnInlineResultChosen(object sender, Telegram.Bot.Args.ChosenInlineResultEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void TelegramService_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
           try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



    }
}
