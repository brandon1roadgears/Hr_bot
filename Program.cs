using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

using static Buttons;
using static FIleReader;

namespace MainProgramms
{
    class MainFunctions
    {
        private static TelegramBotClient bot = new TelegramBotClient("6070896245:AAFdsxzXWK8PacUMmGSp5susStyI1Gmu_vs");
        private static void Main()
        {
            bot.StartReceiving(updateHandler: Update, pollingErrorHandler: Error);
            Console.ReadLine();
        }

        async private static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            switch(update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    switch(update.Message.Text)
                    {
                        case "/start":
                            
                            if(!IsUserExist(update.Message.Chat.Id.ToString()))
                            {
                                SignUpUser(update.Message.Chat.Id.ToString());
                            }
                            else
                            {
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            SaveStep(update.Message.Chat.Id.ToString(), -1);
                            SaveMessage(update.Message.Chat.Id.ToString(), update.Message.MessageId);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.Message.Chat.Id,
                                text: "Прежде чем мы начнём, укажте свою роль.",
                                replyMarkup: WhoAreYou
                            );
                        break;
                        case "/restart":
                            if(!IsUserExist(update.Message.Chat.Id.ToString()))
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Для возможности использования данной команды необходимо начать процедуру авторизации!"
                                );
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            SaveStep(update.Message.Chat.Id.ToString(), -1);
                            int start = LoadMessage(update.Message.Chat.Id.ToString());
                            for(int i = update.Message.MessageId; i >= start; --i)
                            {
                                try
                                {
                                    await DeleteMessage(client, update.Message.Chat.Id, i);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                            DeleteUserData(update.Message.Chat.Id.ToString());
                            await client.SendTextMessageAsync
                            (
                                chatId: update.Message.Chat.Id,
                                text: "Ваши данные были успешно удалены\nНапишите [/start] для продолжения работы с ботом."
                            );
                        break;

                        case "/questions":
                            if(!IsUserExist(update.Message.Chat.Id.ToString()) || LoadStep(update.Message.Chat.Id.ToString()) == -1)
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Для возможности использования данной команды, вам нужно пройти процесс авторизации!"
                                );
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            if(LoadRole(update.Message.Chat.Id.ToString()) == "hr")
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Использовать данную команду могут только новые сотрудники!"
                                );
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            await client.SendTextMessageAsync
                            (
                                chatId: update.Message.Chat.Id,
                                text: "Отвечу на все часто задаваемые вопросы.",
                                replyMarkup: MainQuestions
                            );
                        break;
                            
                        case "/hr_list":
                            if(!IsUserExist(update.Message.Chat.Id.ToString()) || LoadStep(update.Message.Chat.Id.ToString()) == -1)
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Для возможности использования данной команды, вам нужно пройти процесс авторизации!"
                                );
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            if(LoadRole(update.Message.Chat.Id.ToString()) == "new")
                            {
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Использовать данную команду может только HR-персонал!"
                                );
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                                return;
                            }
                            await client.SendTextMessageAsync
                            (
                                chatId: update.Message.Chat.Id,
                                text: "Вот список доступных вам функций.",
                                replyMarkup: HrButtons
                            );
                        break;

                        case string userMessage when userMessage != null:
                            if(!IsUserExist(update.Message.Chat.Id.ToString()))
                            {
                               await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                               return;
                            }
                            int step = LoadStep(update.Message.Chat.Id.ToString());
                            Console.WriteLine(step);
                            if(step == 1)
                            { 
                                SaveStep(update.Message.Chat.Id.ToString(), 0);
                                SaveName(update.Message.Chat.Id.ToString(), userMessage);
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Введённые вами данные корректны?",
                                    replyMarkup: NameIsCorrectButtons
                                );
                            }
                            else if(step == 2)
                            {
                                SaveStep(update.Message.Chat.Id.ToString(), 0);
                                SaveCity(update.Message.Chat.Id.ToString(), userMessage);
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Введённые вами данные корректны?",
                                    replyMarkup: CityIsCorrectButtons
                                );
                            }
                            else if(step == 3)
                            {
                                SaveStep(update.Message.Chat.Id.ToString(), 0);
                                SavePost(update.Message.Chat.Id.ToString(), userMessage);
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Введённые вами данные корректны?",
                                    replyMarkup: PostIsCorrectButtons
                                );
                            }
                            else if(step == 4)
                            {   SaveStep(update.Message.Chat.Id.ToString(), 0);
                                SaveNumber(update.Message.Chat.Id.ToString(), userMessage);
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.Message.Chat.Id,
                                    text: "Введённые вами данные корректны?",
                                    replyMarkup: NumberIsCorrectButtons
                                );
                            }
                            else if(step == 0 || step == -1)
                            {
                                await DeleteMessage(client, update.Message.Chat.Id, update.Message.MessageId);
                            }
                       break;
                    }
                break;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:             
                    await DeleteMessage(client, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                    await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                    switch(update.CallbackQuery.Data)
                    {
                        case "IsHr":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 0);
                            SaveRole(update.CallbackQuery.Message.Chat.Id.ToString(), "hr");
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Всё, понял, не буду мешать!!!",
                                replyMarkup: HrButtons
                            );
                        break;
                        case "Workers":
                            foreach(var i in GetListOfWorkers())
                            {
                                string _i = i.Substring(27);
                                if(LoadRole(_i) == "hr" || !IsUersRegistrated(_i)) continue;
                                await client.SendTextMessageAsync
                                (
                                    chatId: update.CallbackQuery.Message.Chat.Id,
                                    text: LoadName(_i) + "\n" + LoadCity(_i) + "\n" + LoadPart(_i) + "\n" + LoadPost(_i) + "\n" + LoadNumber(_i)
                                );                                    
                            }
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Выберите действие",
                                replyMarkup: HrButtons
                            );
                        break;
                        case "SendAttention":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 100);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Напишите сообщение и оно будет переслано всем сотрудникам"
                            );
                        break;

                        case "IsNotHr":
                            SaveRole(update.CallbackQuery.Message.Chat.Id.ToString(), "new");
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 1);
                            await client.SendStickerAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                sticker: InputFile.FromUri("https://chpic.su/_data/stickers/o/ofice_vk/ofice_vk_001.webp")

                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Привет! Я твой виртуальный-наставник.\n"+
                                      "Я помогу тебе освоиться в первые дни на работе и познакомлю тебя с твоими коллегами.\n\n"+
                                      "Не будем терять время, и приступим к нашим первым шагам!"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Для начала, нам стоит познакомиться)\n" +
                                      "Как тебя зовут?"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2,
                                text: "Напиши *имя и фамилию* в одной строке"
                            );
                            await client.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id);
                        break;

                        case "NameIsCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 2);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text: "Приятно познакомиться, " + LoadName(update.CallbackQuery.Message.Chat.Id.ToString()) + "!\n\n" +
                                      "Теперь в одной строке укажи в каком городе находтся твой офис." 
                            );
                        break;
                        case "NameIsNotCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 1);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Хорошо, введи имя и фамилию ещё раз."
                            );
                        break; 
 
                        case "CityIsCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 0);
                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: LoadCity(update.CallbackQuery.Message.Chat.Id.ToString()) + " прекрасный город!"
                            );
                            await client.SendTextMessageAsync
                            (   
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Теперь выбери отдел, в котором ты будешь работать.",
                                replyMarkup: Parts
                            );
                        break;
                        case "CityIsNotCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 2);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Хорошо, введи свой город ещё раз."
                            );
                        break;

                        case string teg when teg.Contains("/p"):
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 3);
                            SavePart(update.CallbackQuery.Message.Chat.Id.ToString(), teg.TrimEnd('/', 'p'));
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Замечательно!\n" + 
                                      "Осталось ввести свою должность и мы продолжим! Вводи в одной строке!"
                            );
                        break;

                        case "PostIsCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 4);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "И последнее, укажи свой номер телефона, чтобы сотрудники HR отдела смогли с тобой связаться."
                            );
                            
                        break;

                        case "PostIsNotCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 3);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Хорошо, введи свою должность ещё раз."
                            );
                        break;

                        case "NumberIsCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 0);
                            await client.SendStickerAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                sticker: InputFile.FromUri("https://chpic.su/_data/stickers/o/ofice_vk/ofice_vk_003.webp")

                            );
                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Прекрасно!!!\n"+
                                      "Мы заполнили всю необходимую информацию!!!\n\n"+
                                      "Теперь приступим к самому интересному: знакомству с нашей компанией!!!"
                            );
                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text: "Небольшая история нашей компании!!!\n" +
                                      "Рекомендую ознакомиться, чтобы быть ещё ближе к тому, чтобы стать достойной частью достойной комапании!!!\n\n" +
                                      "[История Компании](https://www.xn--80adabaqgmgqe4aegegbby51a.xn--p1ai/company/istoriya-kompanii/)\n"
                            );
                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text: "[Интересные факты о компании](https://www.xn--80adabaqgmgqe4aegegbby51a.xn--p1ai/company/factsabout/)\n"
                            );
                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text: "[Политика качества продукции](https://www.xn--80adabaqgmgqe4aegegbby51a.xn--p1ai/company/quality/)\n"
                            );

                            await client.SendTextMessageAsync
                            ( 
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Но не будем останавливаться. На этом сайте есть ещё много интересной информации о нашей компании.\n"+
                                "Рекомендую ознакомиться с ней на досуге)"
                            );
                            await client.SendStickerAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                sticker: InputFile.FromUri("https://chpic.su/_data/stickers/o/ofice_vk/ofice_vk_013.webp")
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text: "Следующим важным шагом будет знакомство с твоим наставником!!!\n" + 
                                      "В каждом отделе имеется сотрудник, ответственный за адапатцию новичков.\n" + 
                                      "За работу с новыми сотрудниками в отделе " + LoadPart(update.CallbackQuery.Message.Chat.Id.ToString()) + " отвечает " + 
                                      "[Данил](https://vk.com/danila_dmtr)"
                            );
                            await client.SendTextMessageAsync
                            (
                                   chatId: update.CallbackQuery.Message.Chat.Id,
                                   text: "Тебе небходимо написать своему наставнику и договориться о встрече.\n" +
                                         "В назначенный день, этот человек проведёт небольшую экскурсию и объяснит принцип устройства самых сложных процессов в нашей компании."
                            );  
                            await client.SendTextMessageAsync
                            (   
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                text:"Также я обязан сообщить, что у твоего отдела имеется специальный [ЧАТ](https://t.me/+iRlJbX4KA4o1Yzc6)\n" +
                                     "Здесь ты можешь общаться со своими коллегами и также обсуждать самые актуальные вопросы!"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Ну, а пока ты ожидаешь ответа от наставника, я уже готов отвтетить тебе на часто задавемые вопросы.\n" +
                                "Просто набери команду /questions"
                            );
                                                       
                        break;
                        case "NumberIsNotCorrect":
                            SaveStep(update.CallbackQuery.Message.Chat.Id.ToString(), 4);
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Хорошо, введи номер телефона ещё раз."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;

                        case "q0":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Рабочий день начинается обычно в 9:00 утра и заканчивается в 18:00 вечера, с понедельника по пятницу."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q1":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "636160, Россия, Томская область,\nс. Кожевниково, пер. Дзержинского, 19 стр.1"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q2":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Ваше рабочее место находится в офисе по адресу:\n" +
                                      "636160, Россия, Томская область,\nс. Кожевниково, пер. Дзержинского, 19 стр.1"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q3":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Обеденное время с 12:00 до 13:00.\n" + 
                                      "Кроме обеда, у вас есть право на короткие перерывы в течение дня для отдыха и перекуса."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q4":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Необходимо поддерживать чистоту и порядок на рабочем месте.\n" + 
                                      "Персональные вещи должны быть аккуратно убраны."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q5":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Пожалуйста, ознакомьтесь с внутренним регламентом компании, который будет предоставлен вам при приеме на работу.\n" +
                                      "Электронная версия регламента [Ссылка на какой-нибудь догумент]."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q6":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "[Описание структуры компании, руководителей и подразделений]"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q7":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "В нашей компании есть коллеги, которые работают удаленно. Вы можете связаться с ними через электронную почту или внутренние коммуникационные инструменты."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q8":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "У нас нет формы одежды, но мы ожидаем, что вы будете одеваться профессионально и соответствующе рабочей обстановке."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q9":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Ожидается вежливое и профессиональное поведение на рабочем месте."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q10":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Для получения доступа к компьютеру и офисному оборудованию обратитесь в IT-отдел компании.\n [Ссылка для свзяи с отделом]"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q11":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "При возникновении технических проблем свяжитесь с IT-поддержкой [Ссылка для свзяи с IT-отделом]."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q12":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Мы используем программы [список программ] для работы. Вам будет предоставлено обучение по их использованию."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q13":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Пожалуйста, соблюдайте стандарты безопасности при работе с данными и информацией компании.\n[Ссылка с рекомендациями по безопасной эксплуатации ресурсов компании]"
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q14":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Ваши первоочередные задачи включают [перечень задач]."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                        case "q15":
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Подробности вашего плана работы обсудят с вами ваш руководитель."
                            );
                            await client.SendTextMessageAsync
                            (
                                chatId: update.CallbackQuery.Message.Chat.Id,
                                text: "Список всех вопроов доступен по команде /questions"
                            );
                        break;
                    }
                break;
            }
        }
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        async private static Task DeleteMessage(ITelegramBotClient client, long chatId, int messageId)
        {
            await client.DeleteMessageAsync
            (
                chatId : chatId,
                messageId : messageId
            );
        }
    }
}
