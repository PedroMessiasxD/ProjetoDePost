namespace ProjetoDePost.Exceptions
{
    public class EmpresaCriacaoException : Exception
    {
        public EmpresaCriacaoException(string message) : base(message)
        {

        }

        public EmpresaCriacaoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
