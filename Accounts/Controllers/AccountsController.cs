using Accounts.Models;
using Accounts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountsRepository _repository;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            // TODO: refactor using Service Layer.
            return await _repository.ReadAccounts();
        }


        [HttpPost]
        public async Task<Account> CreateAccoint(NewAccount request)
        {
            // TODO: refactor using Service Layer.
            // validate

            var account = new Account
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = (int)AccountStatus.New,
                Created = DateTime.Now
            };

            await _repository.CreateAccount(account);
            return account;
        }

    }
}