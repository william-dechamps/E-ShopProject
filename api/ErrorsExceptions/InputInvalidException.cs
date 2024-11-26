[Serializable]
class InputInvalidException : Exception
{
    public InputInvalidException() { }

    public InputInvalidException(string message)
        : base(message) { }
}
