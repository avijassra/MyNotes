namespace MyNotes.Backend.Service.Contracts
{
    using System;
    using System.ServiceModel;
    using MyNotes.Backend.Dtos;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        LoggedUserInfoDto Authenticate(string username, string password);

        [OperationContract]
        UserDto GetSingleUser(Guid id);

        [OperationContract]
        IList<UserDto> GetAllUsers(Guid groupId, bool isSysAccount);

        [OperationContract]
        MessageResultDto AddUser(string firstname, string lastname, string nickname, string username, Guid groupId);

        [OperationContract]
        MessageResultDto UpdateUser(Guid id, string firstname, string lastname, string nickname, string username, Guid groupId);

        [OperationContract]
        MessageResultDto DeleteUser(Guid id);

        [OperationContract]
        MessageResultDto ResetPassword(Guid id);

        [OperationContract]
        MessageResultDto UserLockStatus(Guid id, bool isLocked);
    }
}