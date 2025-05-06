namespace DevCopilot2.Domain.Enums.User
{
    public enum ChangeUserResult
    {
        NotFound = 1,
        Success,
        PassportNumberExists,
        NationalCodeExists,
        PhoneNumberExists,
        InvalidNationalCode,
        InvalidPhoneNubmer,
        NullNationalCodeAndPassportNumber,
        NullYektaCode
    }

    public enum LoginUserResult
    {
        Success,
        NotFound,
        IncorrectCode,
        CodeExpired
    }

}
