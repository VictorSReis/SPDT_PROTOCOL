namespace SPDTSdk;

public readonly struct NotifyErrorObject
{
    public SPDTError Error { get; }
    public UInt32 StreamIDGeneratedError { get; }

    public NotifyErrorObject(SPDTError pError, UInt32 pStreamIDGeneratedError)
    {
        Error = pError;
        StreamIDGeneratedError = pStreamIDGeneratedError;
    }
}