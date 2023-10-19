using Telegram.Bot.Types.ReplyMarkups;
internal class Buttons
{
    internal static InlineKeyboardMarkup HrButtons = new
    (
        new[]
        {
            /*new[]
            {
                InlineKeyboardButton.WithCallbackData("Разослать уведомление сотрудникам сообщение", callbackData:"SendAttention")
            },*/
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Список сотрудников", callbackData:"Workers")
            }
        }
    );
    internal static InlineKeyboardMarkup WhoAreYou = new
    (
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HR-специалист", callbackData:"IsHr")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Новый сотрудник", callbackData:"IsNotHr")
            }
        }
    );
    internal static InlineKeyboardMarkup NameIsCorrectButtons = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Да, имя верно!", callbackData:"NameIsCorrect"),
            InlineKeyboardButton.WithCallbackData("Нет, введу снова!", callbackData:"NameIsNotCorrect")
        }
    );
    internal static InlineKeyboardMarkup NumberIsCorrectButtons = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Да, номер корректен!", callbackData:"NumberIsCorrect"),
            InlineKeyboardButton.WithCallbackData("Нет, введу снова!", callbackData:"NumberIsNotCorrect")
        }
    );
    internal static InlineKeyboardMarkup CityIsCorrectButtons = new
    (
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Да, город указан верно!", callbackData:"CityIsCorrect"),
            InlineKeyboardButton.WithCallbackData("Нет, введу снова!", callbackData:"CityIsNotCorrect")
        }
    );
    internal static InlineKeyboardMarkup PostIsCorrectButtons = new
    (
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Да, должность указана верно!", callbackData:"PostIsCorrect")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Нет, введу снова!", callbackData:"PostIsNotCorrect")
            }
        }
    );
    internal static InlineKeyboardMarkup Parts = new
    (
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел IT", callbackData:"IT/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел СПЕЦИАЛИСТОВ-ТЕХНОЛОГОВ", callbackData:"СПЕЦИАЛИСТОВ ТЕХНОЛОГОВ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел ОЦЕНКИ КАЧЕСТВА ПРОДУКЦИИ", callbackData:"ОЦЕНКИ КАЧЕСТВА ПРОДУКЦИИ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел HR", callbackData:"HR/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел УПРАВЛЕНИЯ И КОНТРОЛЯ", callbackData:"УПРАВЛЕНИЯ И КОНТРОЛЯ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел ФИНАНСОВ", callbackData:"ФИНАНСОВ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел ПРОДАЖ", callbackData:"ПРОДАЖ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел АНАЛИТИКИ", callbackData:"АНАЛИТИКИ/p")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отдел ЮРИСПРУДЕНЦИИ", callbackData:"ЮРИСПРУДЕНЦИИ/p")
            }
        }
    );

    internal static InlineKeyboardMarkup MainQuestions = new
    (
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Время начала и конца рабочего дня", callbackData:"q0")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Адрес места работы", callbackData:"q1")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Где находится моё рабочее место?", callbackData:"q2")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Перерывы и обеденное время", callbackData:"q3")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Правила оформления рабочего места", callbackData:"q4")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Положения внутреннего регламента компании", callbackData:"q5")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Структура компании, мои руководители и коллеги", callbackData:"q6")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Сотрудники, доступные для связи только удалённо", callbackData:"q7")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Дресс-код компании", callbackData:"q8")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Особенности этикета и поведения на рабочем месте", callbackData:"q9")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Доступ к компьютеру и другому оборудованию", callbackData:"q10")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Куда обращаться, в случае технических проблем?", callbackData:"q11")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Программы и системы использующиеся в работе?", callbackData:"q12")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Процедуры и стандарты безопасности", callbackData:"q13")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Мои первоочередные задачи", callbackData:"q14")
            },  
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Мой план работы", callbackData:"q15")
            }
        }
    );
}