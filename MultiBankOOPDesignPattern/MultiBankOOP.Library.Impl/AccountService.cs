using MultiBankOOP.Library.Contracts;
using MultiBankOOP.Library.Contracts.DTOs;
using MultiBankOOP.Domain.Models;
using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.XCutting.Enums;
using MultiBankOOP.Library.Contracts.DTOs;
using MultiBank.XCutting.Enums;

namespace MultiBankOOP.Library.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository? _accountRepository;
        private readonly IMovementsRepository? _movementsRepository;
        public string UserNumber { get; set; }

        public AccountService(IAccountRepository accountRepository, IMovementsRepository? movementsRepository)
        {
            _accountRepository = accountRepository;
            _movementsRepository = movementsRepository;
        }

        public void SetUserNumber (string number)
        {
            UserNumber = number;
        }
        public LoginResultDto Login(string number, int pin)
        {
            LoginResultDto loginResult = new()
            {
                ResultHasErrors = false,
                Error = null
            };

            AccountModel accountModel = new();
            AccountEntity accountEntity = _accountRepository?.GetAccountInfo(number);

            if (accountEntity == null)
            {
                loginResult.ResultHasErrors = true;
                loginResult.Error = LoginErrorEnum.NotPresent;

            }
            else
            {
                accountModel.Number = accountEntity.number;
                accountModel.Pin = accountEntity.pin;

                if (!accountModel.Login(pin))
                    loginResult.Error = LoginErrorEnum.IncorrectPin;

            }

            return loginResult;

        }
        public IncomeResultDto AddMoney(decimal income)
        {
            IncomeResultDto result = new()
            {
                ResultHasErrors = false,
                Error = null
            };

            AccountModel accountModel = new();
            if (accountModel.ValidIncome(income))
            {
                AccountEntity? accountEntity = _accountRepository?.GetAccountInfo(UserNumber);
                List<MovementEntity>? movementsEntityList = _movementsRepository?.GetMovements(UserNumber);
                if (accountEntity != null && movementsEntityList != null)
                {
                    // map entity to domain model...
                    accountModel.Money = accountEntity.money;
                    accountModel.Movements = movementsEntityList.Select(x => new MovementModel
                    {
                        Value = x.value,
                        Timestamp = x.timestamp,
                    }).ToList();

                    // ... in order to apply business logic in domain model
                    accountModel.AddIncome(income);

                    // map domain model result to entity...
                    accountEntity.money = accountModel.Money;
                    MovementEntity movementToAdd = new()
                    {
                        value = accountModel.Movements.Last().Value,
                        timestamp = accountModel.Movements.Last().Timestamp
                    };

                    // ... in order to save entity with the last changes done
                    _accountRepository?.UpdateAccount(UserNumber, accountEntity);
                    _movementsRepository?.AddMovement(UserNumber, movementToAdd);

                    // map domain model to Dto in order to add needed information to presentation layer
                    result.totalMoney = accountModel.Money;
                }
            }
            else
            {
                result.ResultHasErrors = true;

                if (accountModel.incomeNegative)
                {
                    result.Error = IncomeErrorEnum.Negative;
                }

                if (accountModel.incomeOverMaxValue)
                {
                    result.Error = IncomeErrorEnum.OverMaxValue;
                    result.maxIncomeAllowed = AccountModel.maxIncome;
                }
            }

            return result;
        }

        public OutcomeResultDto RetireMoney(decimal outcome)
        {
            OutcomeResultDto result = new()
            {
                ResultHasErrors = false,
                Error = null
            };

            AccountModel accountModel = new();
            if (accountModel.ValidOutcome(outcome))
            {
                AccountEntity? accountEntity = _accountRepository?.GetAccountInfo(UserNumber);
                List<MovementEntity>? movementsEntityList = _movementsRepository?.GetMovements(UserNumber);
                if (accountEntity != null && movementsEntityList != null)
                {
                    accountModel.Money = accountEntity.money;
                    accountModel.Movements = movementsEntityList.Select(x => new MovementModel
                    {
                        Value = x.value,
                        Timestamp = x.timestamp,
                    }).ToList();

                    accountModel.AddOutcome(outcome);

                    accountEntity.money = accountModel.Money;
                    MovementEntity movementToAdd = new()
                    {
                        value = accountModel.Movements.Last().Value,
                        timestamp = accountModel.Movements.Last().Timestamp
                    };

                    _accountRepository?.UpdateAccount(UserNumber, accountEntity);
                    _movementsRepository?.AddMovement(UserNumber, movementToAdd);

                    result.totalMoney = accountModel.Money;
                }
            }
            else
            {
                result.ResultHasErrors = true;

                if (accountModel.outcomeNegative)
                {
                    result.Error = OutcomeErrorEnum.Negative;
                }

                if (accountModel.outcomeOverMaxValue)
                {
                    result.Error = OutcomeErrorEnum.OverMaxValue;
                    result.maxOutcomeAllowed = AccountModel.maxOutcome;
                }

                if (accountModel.outcomeLeavesAccountOverMaxAllowedDebt)
                {
                    result.Error = OutcomeErrorEnum.MaxAllowedDebtSurpassed;
                    result.maxDebtAllowed = AccountModel.maxDebtAllowed;
                }
            }

            return result;
        }

        public MovementListDto GetMovements()
        {
            List<MovementEntity> movementsEntityList = _movementsRepository?.GetMovements(UserNumber)!;

            return new()
            {
                movements = movementsEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = movementsEntityList.Sum(x => x.value)
            };
        }

        public MovementListDto GetIncomes()
        {
            List<MovementEntity> incomesEntityList = _movementsRepository!.GetMovements(UserNumber).Where(x => x.value > 0).ToList();

            return new()
            {
                movements = incomesEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = incomesEntityList.Sum(x => x.value)
            };
        }

        public MovementListDto GetOutcomes()
        {
            List<MovementEntity> outcomesEntityList = _movementsRepository!.GetMovements(UserNumber).Where(x => x.value < 0).ToList();

            return new()
            {
                movements = outcomesEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = outcomesEntityList.Sum(x => x.value)
            };
        }

        public decimal? GetMoney()
        {
            AccountEntity? entity = _accountRepository?.GetAccountInfo(UserNumber);
            if (entity == null)
            {
                throw new Exception();
            }
            AccountModel accountModel = new()
            {
                Number = entity.number,
                Money = entity.money
            };

            return accountModel?.Money;
        }
    }
}
