namespace MyNotes.Backend.Service.Contracts
{
    using System;
    using System.ServiceModel;
    using MyNotes.Backend.Dtos;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        AccountDto GetSingleAccount(Guid id);

        [OperationContract]
        IList<AccountDto> GetAllAccountsInGroup(Guid groupId);

        [OperationContract]
        IList<AccountDto> GetAllUserAccounts(Guid groupId, Guid userId);

        [OperationContract]
        MessageResultDto AddAccount(string name, Guid userId);

        [OperationContract]
        MessageResultDto UpdateAccount(Guid id, string name, Guid userId);

        [OperationContract]
        MessageResultDto DeleteAccount(Guid id);
    }
}
