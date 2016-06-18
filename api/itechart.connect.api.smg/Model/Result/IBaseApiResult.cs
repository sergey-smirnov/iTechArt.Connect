namespace itechart.connect.api.smg.Model.Result
{
    public interface IBaseApiResult<in TOutput>
    {
        void InitializeFromOutput(TOutput output);
    }
}