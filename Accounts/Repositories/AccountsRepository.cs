using Accounts.Models;
using Accounts.Repositories.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Repositories
{
    // TODO: put interface in a separate file
    public interface IAccountsRepository
    {
        public Task<IEnumerable<Account>> ReadAccounts();
        public Task CreateAccount(Account account);
    }


    public class AccountsRepository : IAccountsRepository
    {
        private static readonly AsyncLock _asyncLock = new AsyncLock();

        private readonly ILogger<AccountsRepository> _logger;
        private readonly string _dataFilePath;

        public AccountsRepository(
            ILogger<AccountsRepository> logger,
            IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _dataFilePath = Path.Combine(
                hostingEnvironment.WebRootPath,
                "data",
                "Invoices.csv");
            _logger.LogInformation($"Accounts Repository data file: {_dataFilePath}");
        }

        public async Task<IEnumerable<Account>> ReadAccounts()
        {
            using(await _asyncLock.LockAsync())
            {
                return await GetAllAccounts();
            }
        }

        public async Task CreateAccount(Account account)
        {
            using(await _asyncLock.LockAsync())
            {
                // check if accoutn already exists
                var existingAccounts =  await GetAllAccounts();
                if(existingAccounts.Any(x => x.AccountNumber == account.AccountNumber))
                {
                    throw new Exception($"Failed adding new account. Account number {account.AccountNumber} already exiss");
                }

                // append to file
                var newLine = AccountCsvHelper.FromAccount(account);
                await File.AppendAllLinesAsync(_dataFilePath, new []{newLine});
            }
        }

        private async Task<IEnumerable<Account>> GetAllAccounts()
        {
            var lines = await File.ReadAllLinesAsync(_dataFilePath);
            return lines.Select(x => AccountCsvHelper.ToAccount(x));
        }
    }
}