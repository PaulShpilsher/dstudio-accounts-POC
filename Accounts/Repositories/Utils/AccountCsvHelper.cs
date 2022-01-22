using System.Globalization;
using System.Text;
using Accounts.Models;

namespace Accounts.Repositories.Utils
{
    // Счет:
    // • Дата и время создания счета;
    // • Дата и время изменения счета;
    // • Номер счета;
    // • Статус обработки счета (при создании устанавливается в значение «Новый»);
    // • Сумма счета;
    // • Способ оплаты счета.

    // By the way the provided CSV file structured in copmarison to the spec above
    // I am making an assumption that updated date and time field is optional
    // File:
    // 2020-03-11 08:11:20;0001;3;10.00;1
    // 2020-03-12 08:04:12;0002;2;120.00;3
    // 2020-03-11 22:01:39;0003;3;30.00;2
    // 2020-04-08 12:37:08;0004;1;1.00;3
    // 2020-03-16 10:41:39;0005;1;10.12;3
    // 2020-05-14 10:59:17;0006;2;5.00;1
    // 2020-04-09 18:20:32;0007;3;1230.00;1
    // 2020-03-16 11:13:38;0008;2;132210.00;2
    // 2020-04-13 11:37:09;0009;3;10.00;1
    // 2020-05-14 10:53:19;0010;1;133.00;3
    //
    // With the above assumption this is how the file will look 
    // with an updated accounts:
    //
    // 2020-03-11 08:11:20;2020-03-11 08:11:24;0001;3;10.00;1
    // 2020-03-12 08:04:12;0002;2;120.00;3
    // 2020-03-11 22:01:39;0003;3;30.00;2
    // 
    // the factory methods serialize/deserialize
    // Account data model to/from CSV string handling the optiona
    // updated date and time field  

    public static class AccountCsvHelper
    {

        private const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string csvSeparator = ";";

        public static Account ToAccount(string s)
        {
            var account = new Account();
            var fields = s.Split(csvSeparator);

            account.Created = DateTime.ParseExact(fields[0], dateTimeFormat, CultureInfo.InvariantCulture);

            int index = 1;

            // opttional field
            DateTime updated;
            if(DateTime.TryParseExact(fields[index], dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out updated))
            {
                account.Updated = updated;
                ++index;
            }

            account.AccountNumber = fields[index];
            account.Status = (int)Enum.Parse<AccountStatus>(fields[index+1]);
            account.Amount = decimal.Parse(fields[index+2]);
            account.PaymentMethod = (int)Enum.Parse<PaymentMethod>(fields[index+3]);
            
            return account;
        }


        public static string FromAccount(Account account)
        {
            var builder = new StringBuilder();
            builder
                .Append(account.Created.ToString(dateTimeFormat,CultureInfo.InvariantCulture))
                .Append(csvSeparator);
            if(account.Updated.HasValue)
            {
                builder
                    .Append(account.Updated.Value.ToString(dateTimeFormat,CultureInfo.InvariantCulture))
                    .Append(csvSeparator);
            }

            builder
                .Append(account.AccountNumber).Append(csvSeparator)
                .Append(account.Status).Append(csvSeparator)
                .AppendFormat("{0:#.00}", account.Amount).Append(csvSeparator)
                .Append(account.PaymentMethod);

            return builder.ToString();
        }

    }
}
