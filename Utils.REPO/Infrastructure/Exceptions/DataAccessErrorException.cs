namespace Utils.REPO.Infrastructure.Exceptions
{
    public class DataAccessErrorException : Exception
    {
        #region Fields and Properties
        public string ErrorDescription { get; private set; }
        public override string Message => $"An error occurd while processing your request, ErrorDescription= {ErrorDescription}.";
        #endregion

        #region Constructors
        public DataAccessErrorException(string errorDescription) : base(errorDescription)
        {
            ErrorDescription = errorDescription;
        }
        #endregion
    }
}
